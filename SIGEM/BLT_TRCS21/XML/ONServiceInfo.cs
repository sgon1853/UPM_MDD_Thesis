// 3.4.4.5

using System;
using System.Xml;
using System.Collections;
using System.Collections.Specialized;
using SIGEM.Business;
using SIGEM.Business.Exceptions;
using SIGEM.Business.XML;
using SIGEM.Business.Types;

namespace SIGEM.Business.XML
{
	/// <summary>
	/// Summary description for ONServiceInfo.
	/// </summary>
	public class ONServiceInfo
	{
		#region Members
		public HybridDictionary mArgumentList = new HybridDictionary(true);
		public HybridDictionary mChangesDetectionList = new HybridDictionary(true);
		private string mIdService;
		private string mAlias;
		private string mIdClass;
		private string mClassAlias;
		#endregion

		#region Properties
		#endregion

		#region Constructor
		public ONServiceInfo(string idService, string alias, string idClass, string classAlias)
		{
			mAlias = alias;
			mIdService = idService;
			mIdClass = idClass;
			mClassAlias = classAlias;
		}
		#endregion

		#region Add arguments
		public void AddDataValuedArgument(string name, bool Null, DataTypeEnumerator type, int maxLength,  string idArgument, string alias)
		{
			ONArgumentInfo lArg = new ONArgumentInfo(name, Null, type, maxLength, "", idArgument, alias);
			mArgumentList.Add(name.ToLower(), lArg);
		}
		public void AddOIDArgument(string name, bool Null, string className, string idArgument, string alias)
		{
			ONArgumentInfo lArg = new ONArgumentInfo(name, Null, DataTypeEnumerator.OID, 0, className, idArgument, alias);
			mArgumentList.Add(name.ToLower(), lArg);
		}
		public void AddAgrArgument(string name, bool Null, string className, string idArgument, string alias)
		{
			ONArgumentInfo lArg = new ONArgumentInfo(name, Null, DataTypeEnumerator.Agr, 0, className, idArgument, alias);
			mArgumentList.Add(name.ToLower(), lArg);
		}
		#endregion
		#region Add Change Detection Items
		public void AddDataValuedChangeDetectionItem(string key, DataTypeEnumerator type)
		{
			ONChangeDetectionInfo lCD = new ONChangeDetectionInfo(key, type, ""); 
			mChangesDetectionList.Add(key.ToLower(), lCD); 
		}
		public void AddOIDChangeDetectionItem(string key, string className)
		{
			ONChangeDetectionInfo lCD = new ONChangeDetectionInfo(key, DataTypeEnumerator.OID, className);
			mChangesDetectionList.Add(key.ToLower(), lCD); 
		}
		#endregion Change Detection Items

		#region Add filter Variables
		public void AddFilterVariable(string name, DataTypeEnumerator type, int maxLength,  string idArgument, string alias)
		{
			ONArgumentInfo lArg = new ONArgumentInfo(name, true, type, maxLength, "", idArgument, alias);
			mArgumentList.Add(name.ToLower(), lArg);
			lArg.Value = ONContext.GetComponent_ONType(type.ToString());
			lArg.Value.Value = null;
		}
		public void AddOIDAFilterVariable(string name, string className, string idArgument, string alias)
		{
			ONArgumentInfo lArg = new ONArgumentInfo(name, true, DataTypeEnumerator.OID, 0, className, idArgument, alias);
			mArgumentList.Add(name.ToLower(), lArg);
			lArg.Value = ONContext.GetComponent_OID(className);
			lArg.Value.Value = null;
		}
		#endregion
		
		#region XML2ON - ON2XML Filter Variables
		public void XML2ONFilterVariables(XmlReader xmlReader, double dtdVersion)
		{
			// Check the filter variables of the request
			try
			{
				if (!xmlReader.IsStartElement(ONXml.XMLTAG_FILTERVARIABLES))
					throw new ONXMLStructureException(null, ONXml.XMLTAG_FILTERVARIABLES);
			}
			catch
			{
				throw new ONXMLStructureException(null, ONXml.XMLTAG_FILTERVARIABLES);
			}

			if (xmlReader.IsEmptyElement) // Filter dont have Variables
			{
				xmlReader.ReadElementString();
				return;
			}

			xmlReader.ReadStartElement(ONXml.XMLTAG_FILTERVARIABLES);

			// While there are filters to solve ...
			string lName;
			while(xmlReader.IsStartElement(ONXml.XMLTAG_FILTERVARIABLE))
			{
				lName = xmlReader.GetAttribute(ONXml.XMLATT_NAME);
				string lType = xmlReader.GetAttribute(ONXml.XMLATT_TYPE);
				xmlReader.ReadStartElement(ONXml.XMLTAG_FILTERVARIABLE);

				try
				{
					ReadArgument(xmlReader, dtdVersion, lName, lType);
				}
				catch(Exception e)
				{
					throw new ONArgumentException(e, lName);
					
				}
				xmlReader.ReadEndElement(); // Filter.Variable
			}

			xmlReader.ReadEndElement(); // Filter.Variables

			// Comprobations over the filters
			foreach(DictionaryEntry lElem in mArgumentList)
			{
				ONArgumentInfo lArg = (ONArgumentInfo) lElem.Value;
					
				// Check if it is all the filters
				if (lArg.Value == null)
					throw new ONMissingArgumentException(null, lArg.IdArgument, lArg.Alias);

				if (lArg.Value.Value == null && lArg.Null == false)
					throw new ONNotNullArgumentException(null, mIdService, mIdClass, lArg.IdArgument, mAlias, mClassAlias, lArg.Alias);

				if (lArg.MaxLength > 0 )
				{
					ONString lString = lArg.Value as ONString;
                    //MARTA DEFECT 3766
					//ONText lText = lArg.Value as ONText;
					if (((object) lString != null) && (lString.TypedValue != null) && (lString.TypedValue.Length > lArg.MaxLength))
						throw new ONMaxLenghtArgumentException(null, lArg.IdArgument, lArg.Alias, lArg.MaxLength.ToString());
					//MARTA DEFECT 3766
                    //if (((object) lText != null) && (lText.TypedValue != null) && (lText.TypedValue.Length > lArg.MaxLength))
					//	throw new ONMaxLenghtArgumentException(null, lArg.IdArgument, lArg.Alias, lArg.MaxLength.ToString());
				}
			}
		}
		#endregion

		#region XML2ON - ON2XML

		public void XML2ON(XmlReader xmlReader, double dtdVersion, bool checkMissingArguments)
		{
			string XMLTAG = "";
			// Check the elements arguments of the request
			try
			{
				if ((!xmlReader.IsStartElement(ONXml.XMLTAG_ARGUMENTS)) && (!xmlReader.IsStartElement(ONXml.XMLTAG_FILTERVARIABLES)))
					throw new ONXMLNavFilterException(null, xmlReader.ToString(), ONXml.XMLTAG_ARGUMENTS, ONXml.XMLTAG_FILTERVARIABLES);
			}
			catch
			{
				throw new ONXMLNavFilterException(null, xmlReader.ToString(), ONXml.XMLTAG_ARGUMENTS, ONXml.XMLTAG_FILTERVARIABLES);
			}
			
			if (xmlReader.IsEmptyElement) // Service dont have arguments
			{
				xmlReader.ReadElementString();
				return;
			}

			if (xmlReader.IsStartElement(ONXml.XMLTAG_ARGUMENTS))
			{
				xmlReader.ReadStartElement(ONXml.XMLTAG_ARGUMENTS);
				XMLTAG = ONXml.XMLTAG_ARGUMENT;
			}
			else if (xmlReader.IsStartElement(ONXml.XMLTAG_FILTERVARIABLES))
			{
				xmlReader.ReadStartElement(ONXml.XMLTAG_FILTERVARIABLES);
				XMLTAG = ONXml.XMLTAG_FILTERVARIABLE;
			}

			// While there are arguments to solve ...
			string lName;
			while(xmlReader.IsStartElement(XMLTAG))
			{
				string lXmlType;
				try
				{
					if (dtdVersion <= 2.0)
						lName = xmlReader.GetAttribute(ONXml.XMLATT_NAME_DTD20);
					else
						lName = xmlReader.GetAttribute(ONXml.XMLATT_NAME);
						
					lXmlType = xmlReader.GetAttribute(ONXml.XMLATT_TYPE);

					if ((mIdClass == "") && (mIdService == ""))
					{
						string lClass = "";
						DataTypeEnumerator lType = new DataTypeEnumerator();
						if (string.Compare(lXmlType, "autonumeric", true) == 0)
							lType = DataTypeEnumerator.Autonumeric;
						else if (string.Compare(lXmlType, "int", true) == 0)
							lType = DataTypeEnumerator.Int;
						else if (string.Compare(lXmlType, "bool", true) == 0)
							lType = DataTypeEnumerator.Bool;
						else if (string.Compare(lXmlType, "blob", true) == 0)
							lType = DataTypeEnumerator.Blob;
						else if (string.Compare(lXmlType, "date", true) == 0)
							lType = DataTypeEnumerator.Date;
						else if (string.Compare(lXmlType, "datetime", true) == 0)
							lType = DataTypeEnumerator.DateTime;
						else if (string.Compare(lXmlType, "nat", true) == 0)
							lType = DataTypeEnumerator.Nat;
						else if (string.Compare(lXmlType, "real", true) == 0)
							lType = DataTypeEnumerator.Real;
						else if (string.Compare(lXmlType, "password", true) == 0)
							lType = DataTypeEnumerator.Password;
						else if (string.Compare(lXmlType, "string", true) == 0)
							lType = DataTypeEnumerator.String;
						else if (string.Compare(lXmlType, "text", true) == 0)
							lType = DataTypeEnumerator.Text;
						else if (string.Compare(lXmlType, "time", true) == 0)
							lType = DataTypeEnumerator.Time;
						else
							lType = DataTypeEnumerator.OID;
						
						xmlReader.ReadStartElement(XMLTAG);
						if (lType == DataTypeEnumerator.OID)
							lClass = xmlReader.GetAttribute("Class");

						mArgumentList.Add(lName, new ONArgumentInfo(lName, true, lType, 1000, lClass, "", ""));
                    }
					else
						xmlReader.ReadStartElement(XMLTAG);
				}
				catch(Exception e)
				{
					throw new ONXMLStructureException(e, ONXml.XMLATT_NAME);
				}
				
				try
				{
					ReadArgument(xmlReader, dtdVersion, lName, lXmlType);
				}
				catch(Exception e)
				{
					if (e.GetType() == typeof(ONInstanceNotExistException))
						throw;
					else
						throw new ONArgumentException(e, lName);
					
				}
				xmlReader.ReadEndElement(); // Argument
			}

			xmlReader.ReadEndElement(); // Arguments

			// Check the change detection items of the request
			if (xmlReader.IsStartElement(ONXml.XMLTAG_CHANGEDETECTIONITEMS))
			{
				if (xmlReader.IsEmptyElement) // Service dont have change detection items
				{
					xmlReader.ReadElementString();
					return;
				}

				if (xmlReader.IsStartElement(ONXml.XMLTAG_CHANGEDETECTIONITEMS))
				{
					xmlReader.ReadStartElement(ONXml.XMLTAG_CHANGEDETECTIONITEMS);
					XMLTAG = ONXml.XMLTAG_CHANGEDETECTIONITEM;
				}

				// While there are change detection items to solve ...
				while (xmlReader.IsStartElement(XMLTAG))
				{
					try
					{
						lName = xmlReader.GetAttribute(ONXml.XMLATT_NAME);
						xmlReader.ReadStartElement(XMLTAG);
					}
					catch (Exception e)
					{
						throw new ONXMLStructureException(e, ONXml.XMLATT_NAME);
					}

					try
					{
						ReadChangeDetectionItem(xmlReader, dtdVersion, lName);
					}
					catch (Exception e)
					{
						throw new ONArgumentException(e, lName);

					}
					xmlReader.ReadEndElement(); // ChangeDetectionItem
				}

				xmlReader.ReadEndElement(); // ChangeDetectionItems
			}

			// Comprobations over the arguments
			foreach(DictionaryEntry lElem in mArgumentList)
			{
				
				ONArgumentInfo lArg = (ONArgumentInfo) lElem.Value;
					
				// Check if it is all the arguments
		                if (lArg.Value == null)
		                {
		                    if (checkMissingArguments)
		                    {
		                        throw new ONMissingArgumentException(null, lArg.IdArgument, lArg.Alias);
		                    }
		                    else
		                    {
		                        continue;
		                    }
		                }

				if (lArg.Value.Value == null && lArg.Null == false)
					throw new ONNotNullArgumentException(null, mIdService, mIdClass, lArg.IdArgument, mAlias, mClassAlias, lArg.Alias);

				if (lArg.MaxLength > 0 )
				{
					ONString lString = lArg.Value as ONString;
					//MARTA DEFECT 3766
                    //ONText lText = lArg.Value as ONText;
					if (((object) lString != null) && (lString.TypedValue != null) && (lString.TypedValue.Length > lArg.MaxLength))
						throw new ONMaxLenghtArgumentException(null, lArg.IdArgument, lArg.Alias, lArg.MaxLength.ToString());
					//MARTA DEFECT 3766
                    //if (((object) lText != null) && (lText.TypedValue != null) && (lText.TypedValue.Length > lArg.MaxLength))
					//	throw new ONMaxLenghtArgumentException(null, lArg.IdArgument, lArg.Alias, lArg.MaxLength.ToString());
				}
			}

		}

		private void ReadArgument(XmlReader xmlReader, double dtdVersion, string Name, string pType)
		{
			if(pType != null)
			{
				ONArgumentInfo lArg;
				try
				{
					lArg = (ONArgumentInfo) mArgumentList[Name.ToLower()];
				}
				catch
				{
					return;
				}

				if((lArg.Type != DataTypeEnumerator.OID) && (lArg.Type != DataTypeEnumerator.Password) && (string.Compare(lArg.Type.ToString(), pType, true) != 0))
					throw new ONXMLFormatException(null, lArg.Type.ToString());

				if ((xmlReader.IsStartElement(ONXml.XMLTAG_ALTERNATEKEY)) && (mAlias != "MVAgentValidationServ"))
					throw new ONXMLFormatException(null, lArg.Type.ToString());

			}
			
			ReadArgument(xmlReader, dtdVersion, Name);
		}
		
		private void ReadArgument(XmlReader xmlReader, double dtdVersion, string Name)
		{
			// Read the arguments and process
			ONArgumentInfo lArg;
			try
			{
				lArg = (ONArgumentInfo) mArgumentList[Name.ToLower()];
			}
			catch
			{
				return;
			}
		
			switch (lArg.Type)
			{
				case DataTypeEnumerator.Autonumeric:
				{
					lArg.Value = ONXmlAutonumeric.XML2ON(xmlReader, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
				}
				case DataTypeEnumerator.Bool:
				{
					lArg.Value = ONXmlBool.XML2ON(xmlReader, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
				}
				case DataTypeEnumerator.Blob:
				{
					lArg.Value = ONXmlBlob.XML2ON(xmlReader, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
				}
				case DataTypeEnumerator.Date:
				{
					lArg.Value = ONXmlDate.XML2ON(xmlReader, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
				}
				case DataTypeEnumerator.DateTime:
				{
					lArg.Value = ONXmlDateTime.XML2ON(xmlReader, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
				}
				case DataTypeEnumerator.Int:
				{
					lArg.Value = ONXmlInt.XML2ON(xmlReader, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
				}
				case DataTypeEnumerator.Nat:
				{
					lArg.Value = ONXmlNat.XML2ON(xmlReader, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
				}
				case DataTypeEnumerator.Password:
				{
					lArg.Value = ONXmlPassword.XML2ON(xmlReader, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
				}
				case DataTypeEnumerator.Real:
				{
					lArg.Value = ONXmlReal.XML2ON(xmlReader, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
				}
				case DataTypeEnumerator.String:
				{
					lArg.Value = ONXmlString.XML2ON(xmlReader, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
				}
				case DataTypeEnumerator.Text:
				{
					lArg.Value = ONXmlText.XML2ON(xmlReader, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
				}
				case DataTypeEnumerator.Time:
				{
					lArg.Value = ONXmlTime.XML2ON(xmlReader, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
				}
				case DataTypeEnumerator.OID:
				{
					object[] lArgs = new object[2];
					lArgs[0] = xmlReader;
					lArgs[1] = dtdVersion;
					lArg.Value = ONContext.InvoqueMethod(ONContext.GetType_XML(lArg.ClassName), "XML2ON", lArgs) as IONType;
					break;
				}
			}
		}

		private void ReadChangeDetectionItem(XmlReader xmlReader, double dtdVersion, string Name)
		{
			// Read the change detection items
			ONChangeDetectionInfo lCD;
			try
			{
				lCD = (ONChangeDetectionInfo)mChangesDetectionList[Name.ToLower()];
				if (lCD == null)
				{
					xmlReader.Skip();
					return;
				}
			}
			catch
			{
				return;
			}

			switch (lCD.Type)
			{
				case DataTypeEnumerator.Autonumeric:
				{
					lCD.OldValue = ONXmlAutonumeric.XML2ON(xmlReader, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
				}
				case DataTypeEnumerator.Bool:
				{
					lCD.OldValue = ONXmlBool.XML2ON(xmlReader, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
				}
				case DataTypeEnumerator.Blob:
				{
					lCD.OldValue = ONXmlBlob.XML2ON(xmlReader, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
				}
				case DataTypeEnumerator.Date:
				{
					lCD.OldValue = ONXmlDate.XML2ON(xmlReader, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
				}
				case DataTypeEnumerator.DateTime:
				{
					lCD.OldValue = ONXmlDateTime.XML2ON(xmlReader, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
				}
				case DataTypeEnumerator.Int:
				{
					lCD.OldValue = ONXmlInt.XML2ON(xmlReader, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
				}
				case DataTypeEnumerator.Nat:
				{
					lCD.OldValue = ONXmlNat.XML2ON(xmlReader, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
				}
				case DataTypeEnumerator.Password:
				{
					lCD.OldValue = ONXmlPassword.XML2ON(xmlReader, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
				}
				case DataTypeEnumerator.Real:
				{
					lCD.OldValue = ONXmlReal.XML2ON(xmlReader, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
				}
				case DataTypeEnumerator.String:
				{
					lCD.OldValue = ONXmlString.XML2ON(xmlReader, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
				}
				case DataTypeEnumerator.Text:
				{
					lCD.OldValue = ONXmlText.XML2ON(xmlReader, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
				}
				case DataTypeEnumerator.Time:
				{
					lCD.OldValue = ONXmlTime.XML2ON(xmlReader, dtdVersion, ONXml.XMLTAG_LITERAL);
					break;
				}
				case DataTypeEnumerator.OID:
				{
					object[] lArgs = new object[2];
					lArgs[0] = xmlReader;
					lArgs[1] = dtdVersion;
					lCD.OldValue = ONContext.InvoqueMethod(ONContext.GetType_XML(lCD.ClassName), "XML2ON", lArgs) as IONType;
					break;
				}
			}
		}

		#endregion
	}
}

