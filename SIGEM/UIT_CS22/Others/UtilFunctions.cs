// v3.8.4.5.b
using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Globalization;
using System.Data;
using System.Xml;
using SIGEM.Client.Oids;


namespace SIGEM.Client
{
	public static class UtilFunctions
	{
		#region Methods
		/// <summary>
		/// Returns a protected string for & characters
		/// </summary>
		/// <param name="pString"></param>
		/// <returns></returns>
		public static string ProtectAmpersandChars(string pString)
		{
			string lAuxString = pString;
			if (lAuxString.Contains("&"))
			{
				lAuxString = lAuxString.Replace("&", "&&");
			}
			return lAuxString;
		}
		/// Returns true if the list contains a pair with the key
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		public static bool ExistKeyInPairList(List<KeyValuePair<object, string>> list, object keyToBeFound)
		{
			foreach (KeyValuePair<object, string> pair in list)
			{
				if (keyToBeFound == null)
				{
					if (pair.Key == null)
						return true;
				}
				else
				{
					if (pair.Key != null)
					{
						if (keyToBeFound.Equals(pair.Key))
							return true;
					}
				}
			}
			return false;
		}
		/// <summary>
		/// Returns true if the Oid in the list belong to the class.
		/// </summary>
		/// <param name="oidList">Oid list.</param>
		/// <param name="className">Class name.</param>
		/// <returns>bool.</returns>
		public static bool OidsBelongToClass(object oidList, string className)
		{
			List<Oid> lOids = oidList as List<Oid>;
			if (lOids == null)
			{
				return false;
			}

			if (lOids.Count == 0)
			{
				return false;
			}

			if (lOids[0].ClassName.Equals(className, StringComparison.CurrentCultureIgnoreCase))
			{
				return true;
			}
			return false;
		}
		/// <summary>
		/// Compare the value of two objects.
		/// </summary>
		/// <param name="obj1">Obj1.</param>
		/// <param name="obj2">Obj2.</param>
		/// <returns>bool.</returns>
		public static bool ObjectsEquals(object obj1, object obj2)
		{
			// Both are null
			if (obj1 == null && obj2 == null)
			{
				return true;
			}
			// Only one is null
			if (obj1 == null ^ obj2 == null)
			{
				return false;
			}

			if ((obj1.GetType() == typeof(System.Byte[])) || (obj2.GetType() == typeof(System.Byte[])))
			{
				if (obj1.Equals(obj2))
				{

					return true;
				}
			}
			else
			{
				// The second condition is to handle diferent data types but with the same value
				if (obj1.Equals(obj2) || obj1.ToString() == obj2.ToString())
				{
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// Check whether an Oid object is not null and is a valid Oid (all its fields are not null).
		/// </summary>
		/// <param name="oid"></param>
		/// <returns>True if the Oid is not null and valid. False in other case.</returns>
		public static bool IsOidNotNullAndValid(Oid oid)
		{
			return ((oid != null) && oid.IsValid());
		}
		/// <summary>
		/// Determines whether the two Oid lists passed as parameters are equal.
		/// </summary>
		/// <param name="list1">List1.</param>
		/// <param name="list2">List2.</param>
		/// <returns>Returns true if the two list are equal; Otherwise, return false.</returns>
		public static bool OidListEquals(List<Oid> list1, List<Oid> list2)
		{
			return OidListEquals(list1, list2, false);
		}
		/// <summary>
		/// Determines whether the two Oid lists passed as parameters are equal.
		/// </summary>
		/// <param name="list1">List1.</param>
		/// <param name="list2">List2.</param>
		/// <param name="checkOrder">If true, the order of the lists is also checked; If false, the order of the list are ignored.</param>
		/// <returns>Returns true if the two list are equal; Otherwise, return false.</returns>
		public static bool OidListEquals(List<Oid> list1, List<Oid> list2, bool checkOrder)
		{
			if (list1 == null && list2 == null)
			{
				// If both are null, are equals.
				return true;
			}
			else
			{
				if (list1 == null ^ list2 == null)
				{
					// If only one is null, are differents.
					return false;
				}
				else
				{
					// If has different number of element are different.
					if (list1.Count != list2.Count)
					{
						return false;
					}
					else
					{
						// If the lists oder must be checked.
						if (checkOrder)
						{
							// Search all one's elements in other.
							foreach (Oid lOid in list1)
							{
								if (!((list2.Contains(lOid)) && (list1.IndexOf(lOid) == list2.IndexOf(lOid))))
								{
									return false;
								}
							}
						}
						else
						{
							// Search all one's elements in other.
							foreach (Oid lOid in list1)
							{
								if (!list2.Contains(lOid))
								{
									return false;
								}
							}
						}
					}
				}
			}
			return true;
		}
		/// <summary>
		/// Convert a Oid object into a Oid list.
		/// </summary>
		/// <param name="oid">Oid object to convert.</param>
		/// <returns>List of Oids.</returns>
		public static List<Oid> OidToOidList(Oid oid)
		{
			if (oid != null)
			{
				List<Oid> lResultOidList = new List<Oid>();
				lResultOidList.Add(oid);
				return lResultOidList;
			}
			return null;
		}

		#region OidFieldsToString
		/// <summary>
		/// Converts the Oid object fields into a string.
		/// </summary>
		/// <param name="oid">Oid object to convert.</param>
		/// <param name="separator">Separator character.</param>
		/// <returns>Oid object fields converted into a string.</returns>
		public static string OidFieldsToString(Oid oid, char separator)
		{
			StringBuilder lStringBuilder = new StringBuilder();

			if (oid != null)
			{
				// Get the Oid values.
				foreach (object lValue in oid.GetValues())
				{
					lStringBuilder.Append(lValue.ToString());
					lStringBuilder.Append(separator);
				}

				// Remove the last separator.
				if (lStringBuilder.Length > 0)
				{
					lStringBuilder.Length--;
				}
			}

			// Oid converted.
			return lStringBuilder.ToString();
		}
		#endregion OidFieldsToString

		/// <summary>
		/// Process a list of outbound arguments values in order to convert Oid values to Oid lists.
		/// </summary>
		/// <param name="arguments">List of arguments values.</param>
		/// <returns>List of arguments values.</returns>
		public static Dictionary<string, object> ProcessOutboundArgsList(Dictionary<string, object> arguments)
		{
			Dictionary<string, object> lAux = new Dictionary<string, object>();

			foreach (KeyValuePair<string, object> lKeyValuePair in arguments)
			{
				if ((lKeyValuePair.Value as Oid) != null)
				{
					lAux.Add(lKeyValuePair.Key, OidToOidList(lKeyValuePair.Value as Oid));
				}
				else
				{
					lAux.Add(lKeyValuePair.Key, lKeyValuePair.Value);
				}
			}

			return lAux;
		}
		/// <summary>
		/// Verify if both lists are equals.
		/// </summary>
		/// <param name="l1">l1.</param>
		/// <param name="l2">l2.</param>
		/// <returns>bool.</returns>
		public static bool EqualList(IList l1, IList l2)
		{
			if ((l1 == null) && (l2 == null))
			{
				return true;
			}
			else if ((l1 == null) || (l2 == null))
			{
				return false;
			}
			else if (l1.Count != l2.Count)
			{
				return false;
			}
			else
			{
				for (int i = 0; i < l1.Count; i++)
				{
					if (!(l1[i].Equals(l2[i])))
					{
						return false;
					}
				}
			}
			return true;
		}
		/// <summary>
		/// Returns the attribute names separates by comma that doesn't appear in the DataTable
		/// </summary>
		/// <param name="dataTable"></param>
		/// <param name="attributes"></param>
		/// <returns></returns>
		public static string ReturnMissingAttributes(DataTable dataTable, string attributes)
		{
			string newAttributes = "";
			string[] attributeList = attributes.Split(',');
			foreach (string attribute in attributeList)
			{
				string atr = attribute.Trim();
				if (dataTable == null || dataTable.Columns[atr] == null)
				{
					if (!newAttributes.Equals(""))
					{
						newAttributes += ",";
					}

					newAttributes += atr;
				}
			}
			return newAttributes;
		}
		/// <summary>
		/// Returns the extra text to be added to the title in the target scenario
		/// Text will identify to the selected instance
		/// </summary>
		/// <param name="alias">Class or role alias</param>
		/// <param name="oid">Selected intance Oid</param>
		/// <param name="displaySet">Attributes to be shown</param>
		/// <returns></returns>
		public static string GetText2Title(string scenarioAlias, string instanceAlias, Oid oid, string displaySet)
		{
			// Retuned text will have next structure:
			//	  scenarioAlias. instanceAlias: oidValues - displaySetValues

			string title = "";

			if (!scenarioAlias.Equals(""))
			{
				title = scenarioAlias;
			}

			if (!instanceAlias.Equals(""))
			{
				if (!title.Equals(""))
					title += ". ";

				title += instanceAlias + ": ";
			}

			try
			{
				if (oid != null)
				{
					string oidString = "";
					foreach (object value in oid.GetValues())
					{
						if (!oidString.Equals(""))
							oidString += " ";

						oidString += value.ToString();
					}

					title += oidString;

					// Get the information from the selected instance
					if (!displaySet.Equals(""))
					{
						string missingDisplaySet = ReturnMissingAttributes(oid.ExtraInfo, displaySet);

						string displaySetText = "";
						string[] attributes = displaySet.Split(',');
						try
						{
							DataTable dataTable = null;
							if (!missingDisplaySet.Equals(""))
							{
								dataTable = Logics.Logic.ExecuteQueryInstance(Logics.Logic.Agent, oid, missingDisplaySet);
								if (dataTable != null)
								{
									if (oid.ExtraInfo != null)
										oid.ExtraInfo.Merge(dataTable);
									else
										oid.ExtraInfo = dataTable;
								}
							}
							if (oid.ExtraInfo != null && oid.ExtraInfo.Rows.Count > 0)
							{
								DataRow row = oid.ExtraInfo.Rows[0];
								foreach (string attribute in attributes)
								{
									if (!displaySetText.Equals(""))
										displaySetText += " ";

									if (row[attribute].GetType() != typeof(System.DBNull))
									{
										if (row[attribute].GetType() == typeof(System.DateTime))
										{
											DateTime dateTime = (DateTime)row[attribute];
											if (dateTime.Hour == 0 && dateTime.Minute == 0 && dateTime.Second == 0)
											{
												displaySetText += dateTime.ToShortDateString();
											}
											else
											{
												displaySetText += dateTime.ToString();
											}
										}
										else
										{
											displaySetText += row[attribute].ToString();
										}
									}
								}
							}
						}
						catch
						{
						}
						if (!displaySetText.Equals(""))
							title += " - " + displaySetText;
					}
				}
			}
			catch
			{
			}

			return title;
		}
		/// <summary>
		/// Oposite to the Split method
		/// </summary>
		/// <param name="stringList"></param>
		/// <param name="separator"></param>
		/// <returns></returns>
		public static string StringList2String(List<string> stringList, string separator)
		{
			string aux = "";

			foreach (string value in stringList)
			{
				if (!aux.Equals(""))
					aux += separator;

				aux += value;
			}

			return aux;
		}

		#region Operator Like for string
		/// <summary>
		/// Calculates the Like string operation over strParam1 and strParam2.
		/// </summary>
		/// <param name="strParam1"></param>
		/// <param name="strParam2"></param>
		/// <returns>Returns true if strParam1 is like strParam2</returns>

		public static bool Like(string strParam1, string strParam2)
		{
			if (strParam1 == null && strParam2 == null)
				return true;

			if (strParam1 == null || strParam2 == null)
				return false;

			if (strParam2.IndexOf("%") < 0)
			{
				return strParam1.ToUpper().StartsWith(strParam2.ToUpper());
			}

			// Prepare strParam2 protecting the wildcard characters
			string protectedParam2 = strParam2.Replace(".", "\\.");
			protectedParam2 = protectedParam2.Replace("*", "\\*");
			// Change the wildcard Caracters
			protectedParam2 = protectedParam2.Replace("%", ".*");
			protectedParam2 = "\\A" + protectedParam2;
			
			return Regex.IsMatch(strParam1, protectedParam2, RegexOptions.IgnoreCase);
		}
		#endregion

		/// <summary>
		/// Predicate to validate agent connected name
		/// </summary>
		/// <param name="agentName"></param>
		/// <returns></returns>
		public static bool IsAgentConnected(string agentName)
		{
			if (string.Compare(agentName, Logics.Logic.Agent.ClassName, true) == 0)
			{
				return true;
			}
			return false;
		}
		/// <summary>
		/// Checks if the columnList names are included in columns names.
		/// </summary>
		/// <param name="columnList">column list names</param>
		/// <param name="columns">DataColumn collection</param>
		/// <returns>True if all are included, otherwise false</returns>
		public static bool ExistColumn(string[] columnList, DataColumnCollection columns)
		{
			foreach (string column in columnList)
			{
				if (!columns.Contains(column))
				{
					return false;
				}
			}
			return true;
		}

		#region Resources
		/// <summary>
		/// Create an icon from a bitmap.
		/// </summary>
		/// <param name="resourceBitmap">Bitmap to be transformed.</param>
		/// <returns>An Icon.</returns>
		public static System.Drawing.Icon BitmapToIcon(System.Drawing.Bitmap resourceBitmap)
		{
			IntPtr Hicon = resourceBitmap.GetHicon();

			return System.Drawing.Icon.FromHandle(Hicon);
		}
		#endregion Resources


		/// <summary>
		/// Returns the value of the attribute of a node in a XML file, throwing an exception if the attribut is not found.
		/// </summary>
		/// <param name="documentName">Name of the file where the XML document is.</param>
		/// <param name="node">XMLNode to search the attribute in.</param>
		/// <param name="attName">Name of the attribute.</param>
		/// <returns>A string with the expected value.</returns>
		public static string GetProtectedXmlNodeValue(string documentName, XmlNode node, string attName)
		{
			if (node != null &&
				node.Attributes.Count > 0 &&
				node.Attributes[attName] != null)
			{
				return node.Attributes[attName].Value;
			}
			else
			{
				object[] lArgs = new object[3];
				lArgs[0] = attName;
				lArgs[1] = node.Name;
				lArgs[2] = documentName;
				
				throw new Exception(CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_ERROR_LOADING_ATTR_REPORTSXML, LanguageConstantValues.L_ERROR_LOADING_ATTR_REPORTSXML, lArgs));
			}
		}
		#endregion Methods
	}
}

