// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.Data;

using SIGEM.Client.Adaptor.Exceptions;
using SIGEM.Client.Presentation;

namespace SIGEM.Client
{
	/// <summary>
	/// Class that manages the internationalization of the application (multilaguage feature).
	/// </summary>
	public class CultureManager
	{
		#region Members
		/// <summary>
		/// List of supported languages of the application.
		/// </summary>
		private static Dictionary<string, CultureInfo> mSupportedLanguages = null;
		#endregion Members

		#region Properties
		/// <summary>
		/// Gets or sets the current culture of the application.
		/// </summary>
		public static CultureInfo Culture
		{
			get
			{
				return System.Threading.Thread.CurrentThread.CurrentCulture;
			}
			set
			{
				try
				{
					System.Windows.Forms.Application.CurrentCulture = value;
					System.Threading.Thread.CurrentThread.CurrentCulture = value;
					System.Threading.Thread.CurrentThread.CurrentUICulture = value;

					// Initialize the custom culture formats for the application.
					InitializeCultureFormats();
				}
				catch(Exception exception)
				{
					ScenarioManager.LaunchErrorScenario(exception);
				}
			}
		}
		/// <summary>
		/// Gets the list of supported languages of the application. Requires manual changes
		/// if the user needs to support more languages (multilanguage feature).
		/// </summary>
		public static Dictionary<string, CultureInfo> SupportedLanguages
		{
			get
			{
				if (mSupportedLanguages == null)
				{
					/*
					 IMPORTANT: MANUAL CHANGE FOR MULTILANGUAGE FEATURE:
					 * If your language is included below as example, just uncomment
					 * the code line. If not, add the languages that the application needs
					 * usign the ISO standard representation for languages.
					 * Do not forget alseo to add the language resource files
					 * (for example, 'Labels.en-US.resx') for tthe needed languages.
					 */
					mSupportedLanguages = new Dictionary<string, CultureInfo>();
					mSupportedLanguages.Add("en-US", new CultureInfo("en-US"));
					// For adding new Languages
					//mSupportedLanguages.Add("de-DE", new CultureInfo("de-DE"));
				}

				return mSupportedLanguages;
			}
		}
		#endregion Properties

		#region Methods
		/// <summary>
		/// Translates strings depending on the current culture.
		/// </summary>
		/// <param name="idXML">Unique XML resource identifier.</param>
		/// <param name="defaultString">Default string returned if the resource is not found.</param>
		/// <returns>Translated string.</returns>
		public static string TranslateString(string idXML, string defaultString)
		{
			return TranslateString(idXML, defaultString, defaultString);
		}
		/// <summary>
		/// Translates strings depending on the current culture. It also adds parameters to compose the translated string.
		/// </summary>
		/// <param name="idXML">Unique XML resource identifier.</param>
		/// <param name="defaultString">Default string returned if the resource is not found.</param>
		/// <param name="pListArgs">Parameters to compose the translated string.</param>
		/// <returns>Translated string.</returns>
		public static string TranslateStringWithParams(string idXML, string defaultString, params Object[] parameters)
		{
			string lTranslatedString = string.Empty;

			lTranslatedString = TranslateString(idXML, defaultString, defaultString);

			return string.Format(lTranslatedString, parameters);
		}
		/// <summary>
		/// Translates strings modeled by the user depending on the current culture.
		/// </summary>
		/// <param name="idXML">Unique XML resource identifier.</param>
		/// <param name="modeledAlias">Alias modeled by the user.</param>
		/// <param name="interactionToolkitAlias">Current alias of the interactiontoolkit layer.</param>
		/// <returns>Translated string.</returns>
		public static string TranslateString(string idXML, string modeledAlias, string interactionToolkitAlias)
		{
			string lTranslatedString = string.Empty;

			// Checking.
			if ((modeledAlias == null) || (interactionToolkitAlias == null))
			{
				return string.Empty;
			}

			// If multilaguage feature is not used (only the default language).
			if ((SupportedLanguages != null) && (SupportedLanguages.Count <= 1))
			{
				if (interactionToolkitAlias != string.Empty)
				{
					lTranslatedString = interactionToolkitAlias;
				}
				else
				{
					lTranslatedString = modeledAlias;
				}
			}
			// If multilaguage feature is used.
			else
			{
				if (idXML == null)
				{
					lTranslatedString = modeledAlias;
				}
				else
				{
					ResourceManager lrm = null;
					try
					{
						if ((string.Compare(modeledAlias, interactionToolkitAlias, false) != 0))
						{
							lTranslatedString = interactionToolkitAlias;
						}
						else
						{
							if (Culture != null)
							{
								// Go to the resource files depending on the current culture.
								lrm = new ResourceManager("SIGEM.Client.Resources.Language.Labels", Assembly.GetExecutingAssembly());
								lTranslatedString = lrm.GetString(idXML, Culture);
								if (string.IsNullOrEmpty(lTranslatedString))
								{
									lTranslatedString = modeledAlias;
								}
							}
						}
					}
					catch
					{
						lTranslatedString = modeledAlias;
					}
				}
			}

			return lTranslatedString;
		}
		/// <summary>
		/// Translate strings from the business logic layer depending on the current culture.
		/// </summary>
		/// <param name="exception">Business logic exception.</param>
		/// <returns>Translated string.</returns>
		public static string TranslateBusinessLogicException(ResponseException exception)
		{
			string lTranslatedString = string.Empty;

			if (exception == null)
			{
				return lTranslatedString;
			}

			// If there is not multilanguage, return the business logic exception message directly.
			if (SupportedLanguages.Count <= 1)
			{
				return exception.Message;
			}
			// If there is multilanguage, translate the business logic exception message depending on the culture.
			else
			{
				lTranslatedString = TranslateString("e" + exception.Number, exception.Message);
				if (exception.Parameters != null)
				{
					string lSearchedKey = string.Empty;
					for (int lKeyIndex = 0; lKeyIndex < exception.Parameters.Count; lKeyIndex++)
					{
						lSearchedKey = "{" + exception.Parameters[lKeyIndex].Key + "}";

						// Keyed value.
						if (exception.Parameters[lKeyIndex].Type == ErrorParamType.Key)
						{
							// The text to replace must be taken from language resources.
							string lTranslatedKey = TranslateString(exception.Parameters[lKeyIndex].Text, string.Empty);
							lTranslatedString = lTranslatedString.Replace(lSearchedKey, lTranslatedKey);
						}
						// Literal value.
						else if (exception.Parameters[lKeyIndex].Type == ErrorParamType.Literal)
						{
							// The text to replace must be taken directly from the exception.
							lTranslatedString = lTranslatedString.Replace(lSearchedKey, exception.Parameters[lKeyIndex].Text);
						}
					}
				}
			}

			return lTranslatedString;
		}
		/// <summary>
		/// Translate strings from the business logic layer depending on the current culture.
		/// </summary>
		/// <param name="trace">Bussiness Logic exception trace element.</param>
		/// <returns>Translated string.</returns>
		public static string TranslateBusinessLogicException(Trace trace)
		{
			string lTranslatedString = string.Empty;

			if (trace == null)
			{
				return lTranslatedString;
			}

			// If there is not multilanguage, return the business logic exception message directly.
			if (SupportedLanguages.Count <= 1)
			{
				return trace.Message;
			}
			// If there is multilanguage, translate the business logic exception message depending on the culture.
			else
			{
				lTranslatedString = TranslateString("e" + trace.Number, trace.Message);
				if (trace.Parameters != null)
				{
					string lSearchedKey = string.Empty;
					for (int lKeyIndex = 0; lKeyIndex < trace.Parameters.Count; lKeyIndex++)
					{
						lSearchedKey = "{" + trace.Parameters[lKeyIndex].Key + "}";

						// Keyed value.
						if (trace.Parameters[lKeyIndex].Type == ErrorParamType.Key)
						{
							// The text to replace must be taken from language resources.
							string lTranslatedKey = TranslateString(trace.Parameters[lKeyIndex].Text, string.Empty);
							lTranslatedString = lTranslatedString.Replace(lSearchedKey, lTranslatedKey);
						}
						// Literal value.
						else if (trace.Parameters[lKeyIndex].Type == ErrorParamType.Literal)
						{
							// The text to replace must be taken directly from the exception.
							lTranslatedString = lTranslatedString.Replace(lSearchedKey, trace.Parameters[lKeyIndex].Text);
						}
					}
				}
			}

			return lTranslatedString;
		}
		/// <summary>
		/// Initialize the custom culture formats used for the application depending on the user needs.
		/// </summary>
		public static void InitializeCultureFormats()
		{
			// Specify your desired format for DateTime types.
			//Culture.DateTimeFormat.DateSeparator = "/";
			//Culture.DateTimeFormat.TimeSeparator = ":";
			//Culture.DateTimeFormat.FullDateTimePattern = "dd/MM/yyyy";
			//Culture.DateTimeFormat.ShortDatePattern = "dd/MM/yy";
			//Culture.DateTimeFormat.ShortTimePattern = "HH:mm:ss";

			// Specify your desired format for Number types.
			//Culture.NumberFormat.NegativeSign = "-";
			//Culture.NumberFormat.PositiveSign = "+";

			//Culture.NumberFormat.CurrencyDecimalDigits = 2;
			//Culture.NumberFormat.CurrencyDecimalSeparator = ",";
			//Culture.NumberFormat.CurrencyGroupSeparator = ".";
			//Culture.NumberFormat.CurrencySymbol = "";

			//Culture.NumberFormat.NumberDecimalDigits = 2;
			//Culture.NumberFormat.NumberDecimalSeparator = ",";
			//Culture.NumberFormat.NumberGroupSeparator = ".";

			//Culture.NumberFormat.PercentDecimalDigits = 2;
			//Culture.NumberFormat.PercentDecimalSeparator = ",";
			//Culture.NumberFormat.PercentGroupSeparator = ".";
			//Culture.NumberFormat.PercentSymbol = "%";
			//Culture.NumberFormat.PerMilleSymbol = "%";

			//Culture.NumberFormat.PositiveSign = "";
		}
		/// <summary>
		/// Get the custom mask for Date format.
		/// </summary>
		/// <returns>Formated string.</returns>
		public static string GetMaskForDate()
		{
			string lMask = "";
			return lMask;
		}
		/// <summary>
		/// Get the custom mask for DateTime format.
		/// </summary>
		/// <returns>Formated string.</returns>
		public static string GetMaskForDateTime()
		{
			string lMask = "";
			return lMask;
		}
		/// <summary>
		/// Get the custom mask for Time format.
		/// </summary>
		/// <returns>Formated string.</returns>
		public static string GetMaskForTime()
		{
			string lMask = "";
			return lMask;
		}
		/// <summary>
		/// Get the custom mask for Real format.
		/// </summary>
		/// <returns>Formated string.</returns>
		public static string GetMaskForReal()
		{
			string lMask = "";
			return lMask;
		}
		/// <summary>
		/// Retrieves a DataTable containig the translated alias of the application agents.
		/// </summary>
		/// <returns>DataTable containig the translated alias.</returns>
		[Obsolete("Since version 3.5.4.3")]
		public static DataTable GetAgentsAlias()
		{
			DataTable lDataTable = new DataTable();
			lDataTable.Columns.Add(new DataColumn("_name", Type.GetType("System.String")));
			lDataTable.Columns.Add(new DataColumn("_transalatedString", Type.GetType("System.String")));

			DataRow lRow;

			foreach (Logics.LogInAgent lAgent in Logics.Agents.GetLogInAgents())
			{
				lRow = lDataTable.NewRow();
				lRow["_name"] = lAgent.Name;
				lRow["_transalatedString"] = TranslateString(lAgent.IdXML, lAgent.Alias);
				lDataTable.Rows.Add(lRow);
			}

			return lDataTable;
		}
		#endregion Methods
	}
}
