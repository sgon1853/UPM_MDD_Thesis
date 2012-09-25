// 3.4.4.5

using System;
using System.IO;
using System.Text;
using System.Xml;
using SIGEM.Business.OID;
using SIGEM.Business.Types;
using SIGEM.Business.XML;
using SIGEM.Business.Exceptions;

namespace SIGEM.Business
{
	/// <summary>
	/// Descripcion breve de SecureControl.
	/// </summary>
	internal abstract class ONSecureControl
	{
		#region Constants
		private const string mServerName = "Backend"; 
		private const int mValidity = 0; // 0 value means no TimeStamp validation
		private const string mKey = "OlivaNovaModelExecutionSystem";
		private static string mValidLetters = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
		internal const bool SecureServer = true;
		#endregion

		#region Password
		public static string CipherPassword(string password)
		{
			int i;
			string lStrBase = "";
			string lDateString = "18sep1999000000sat";
			string lStrSal = "";
			byte[] lCode = new byte[1];

			for (i = 0; i < 9; i++)
				lStrBase += lDateString.Substring(i, 1) + lDateString.Substring(i + 9, 1);

			for (i = 0; i < password.Length; i++)
			{
				int j = (i + 1) % lStrBase.Length;

				int lCar1 = System.Convert.ToInt32(password.Substring(i, 1)[0]);
				int lCar2 = System.Convert.ToInt32(lStrBase.Substring(j, 1)[0]);

				lCode[0] = System.Convert.ToByte(lCar1 * lCar2 % 64 + 33);
				lStrSal += System.Convert.ToChar(lCode[0]);
			}
			
			return lStrSal;
		}
		#endregion

		#region Ticket
		public static ONOid ValidateTicket(double dtdVersion, string ticket, string clientName)
		{
			try
			{
				string lServerName = "";
				string lClientName = "";
				ONOid lAgentOid;
				ONDateTime lTimeStamp;
				ONNat lValidity;

				#region Open xml
				string lTicket = UncipherTicket(ticket);
				XmlTextReader lXMLReader = new XmlTextReader(new StringReader(lTicket));
				lXMLReader.WhitespaceHandling = WhitespaceHandling.None;
				lXMLReader.MoveToContent();
				#endregion

				#region Load information
				lXMLReader.ReadStartElement("ONTicket");

				// Server name
				lServerName = lXMLReader.ReadElementString("Server");

				// Client name
				lClientName = lXMLReader.ReadElementString("Client");

				// Agent oid
				object[] lParam = new object[2];
				lParam[0] = lXMLReader;
				lParam[1] = dtdVersion;
				lXMLReader.ReadStartElement("Agent");
				string lAgentClass = lXMLReader.GetAttribute("Class");
				lAgentOid = ONContext.InvoqueMethod(ONContext.GetType_XML(lAgentClass), "XML2ON", lParam) as ONOid;
				lXMLReader.ReadEndElement(); // Agent

				// TimeStamp
				lTimeStamp = ONXmlDateTime.XML2ON(lXMLReader, dtdVersion, "Timestamp");

				// Validity
				lValidity = ONXmlNat.XML2ON(lXMLReader, dtdVersion, "Validity");

				lXMLReader.ReadEndElement(); // ONTicket
				#endregion

				#region Validate information
				// Server name
				if (lServerName != mServerName)
					throw new ONAgentValidationException(null);

				// Client name
				if (lClientName != clientName)
					throw new ONAgentValidationException(null);

				// Agent oid
				if(ONSecureControl.SecureServer)
					if (lAgentOid == null)
						throw new ONAgentValidationException(null);

				// TimeStamp
				if (ONStdFunctions.datetimeAfter(lTimeStamp, ONStdFunctions.systemDateTime()))
					throw new ONAgentValidationException(null);
				if (new ONBool(mValidity > 0) && (ONStdFunctions.datetimeBefore(ONStdFunctions.dateTimeAdd(new ONString("s"), lValidity, lTimeStamp), ONStdFunctions.systemDateTime())))
					throw new ONAgentValidationException(null);

				// Validity
				if (new ONBool(mValidity > 0) && (lValidity != new ONNat(mValidity)))
					throw new ONAgentValidationException(null);
				#endregion

				#region Close xml
				lXMLReader.Close();
				#endregion

				return lAgentOid;
			}
			catch (ONAgentValidationException)
			{
				throw;
			}
			catch
			{
				throw new ONAgentValidationException(null);
			}
		}
		public static string GetNextTicket(double dtdVersion, string clientName, ONOid agentOid)
		{
			#region Open xml
			MemoryStream lXMLMemoryStream = new MemoryStream();
			XmlTextWriter lXMLWriter = new XmlTextWriter(lXMLMemoryStream, new System.Text.UTF8Encoding());
			lXMLWriter.WriteStartDocument();
			#endregion

			#region Save information
			lXMLWriter.WriteStartElement("ONTicket");

			// Server name
			lXMLWriter.WriteElementString("Server", mServerName);

			// Client name
			lXMLWriter.WriteElementString("Client", clientName);

			// Agent oid
			if (agentOid == null)
			{
				lXMLWriter.WriteStartElement("Agent");
				lXMLWriter.WriteStartElement(ONXml.XMLTAG_OID);
				lXMLWriter.WriteAttributeString(ONXml.XMLATT_CLASS, "Agente");
				lXMLWriter.WriteElementString(ONXml.XMLTAG_OIDFIELD, "DEBUG");
				lXMLWriter.WriteEndElement(); // Oid
				lXMLWriter.WriteEndElement(); // Agent
			}
			else
			{
				object[] lParam = new object[4];
				lParam[0] = lXMLWriter;
				lParam[1] = agentOid;
				lParam[2] = dtdVersion;
				lParam[3] = "OID.Field";
				lXMLWriter.WriteStartElement("Agent");
				ONContext.InvoqueMethod(ONContext.GetType_XML(agentOid.ClassName), "ON2XML", lParam);
				lXMLWriter.WriteEndElement(); // Agent
			}

			// TimeStamp
			ONXmlDateTime.ON2XML(lXMLWriter, ONStdFunctions.systemDateTime(), dtdVersion, "Timestamp");

			// Validity
			ONXmlNat.ON2XML(lXMLWriter, new ONNat(mValidity), dtdVersion, "Validity");

			lXMLWriter.WriteEndElement(); // ONTicket
			#endregion

			#region Get xml
			lXMLWriter.Flush();
			lXMLMemoryStream.Flush();
			lXMLMemoryStream.Position = 0;
			StreamReader lXMLStreamReader = new StreamReader(lXMLMemoryStream);
			string lText = lXMLStreamReader.ReadToEnd();
			#endregion

			#region Close xml
			lXMLWriter.Close();
			#endregion

			return CipherTicket(lText);
		}
		private static string CipherTicket(string ticket)
		{
			StringBuilder lText = new StringBuilder();

			for (int i = 0; i < ticket.Length; i++)
			{
				string lChar1 = ticket.Substring(i, 1);
				string lChar2 = mKey.Substring(i % mKey.Length, 1);

				int lNum = atoi(lChar1) + atoi(lChar2);
				int lRem = lNum % mValidLetters.Length;
				int lDiv = Convert.ToInt32(Math.Floor((double) (lNum / mValidLetters.Length)));

				lText.Append(mValidLetters[lDiv]);
				lText.Append(mValidLetters[lRem]);
			}
			return lText.ToString();
		}
		private static string UncipherTicket(string ticket)
		{
			StringBuilder lText = new StringBuilder();
			for (int i = 0; i < (ticket.Length / 2); i++)
			{
				string lChar11 = ticket.Substring(i * 2, 1);
				string lChar12 = ticket.Substring(i * 2 + 1, 1);
				string lChar2 = mKey.Substring(i % mKey.Length, 1);

				int lDiv = mValidLetters.IndexOf(lChar11);
				int lRem = mValidLetters.IndexOf(lChar12);
				int lNum = lDiv * mValidLetters.Length + lRem;

				lText.Append(itoa(lNum - atoi(lChar2)));
			}

			return lText.ToString();
		}
		private static int atoi(string character)
		{
			int i = System.Convert.ToInt32(character[0]);

			return i;
		}
		private static string itoa(int character)
		{
			string lRet = "";
			lRet += System.Convert.ToChar(character % 65536);
			return lRet;
		}
		#endregion
	}
}
