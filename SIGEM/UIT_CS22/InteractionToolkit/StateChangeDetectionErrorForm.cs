// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using SIGEM.Client.Adaptor.Exceptions;
using SIGEM.Client.Controllers;
using SIGEM.Client.Oids;
using SIGEM.Client.Logics;
using SIGEM.Client.Presentation;

namespace SIGEM.Client.InteractionToolkit
{
	/// <summary>
	/// Application Error form.
	/// </summary>
	public partial class StateChangeDetectionErrorForm : Form
	{
		#region Members
		private Dictionary<string, KeyValuePair<Oid, DisplaySetInformation>> mSCDInfo;
		private Dictionary<string, object> mNewValues;
		private bool mShowValues = false;
		private bool mAllowRetry = false;
		#endregion Members
		#region Constructors
		public StateChangeDetectionErrorForm()
		{
			InitializeComponent();

			// Icon assignament.
			this.Icon = UtilFunctions.BitmapToIcon(Properties.Resources.warning);

		}
		#endregion Constructors

		#region Initialize
		/// <summary>
		/// Initializes the 'StateChangeDetectionErrorForm' of the application.
		/// </summary>
		/// <param name="serviceContext">Related Service Context</param>
		/// <param name="showValues">Indicates if the values must be shown</param>
		/// <param name="allowRetry">Indicates if the user may ask for retry</param>
		public void Initialize(Dictionary<string, KeyValuePair<Oid, DisplaySetInformation>> SCDInfo, Dictionary<string, object> newValues, bool showValues, bool allowRetry)
		{
			mSCDInfo = SCDInfo;
			mNewValues = newValues;
			mShowValues = showValues;
			mAllowRetry = allowRetry;

			SetTexts();

			try
			{
				ConfigureScenario();
			}
			catch (Exception e)
			{
				Presentation.ScenarioManager.LaunchErrorScenario(e);
			}
		}

		#endregion Initialize

		#region Methods

		/// <summary>
		/// Assigns the button texts and caption depending on the language.
		/// </summary>
		private void SetTexts()
		{
			this.Text = CultureManager.TranslateString(LanguageConstantKeys.L_SCD_TITLE, LanguageConstantValues.L_SCD_TITLE);
			this.buttonRetry.Text = CultureManager.TranslateString(LanguageConstantKeys.L_SCD_BTN_RETRY, LanguageConstantValues.L_SCD_BTN_RETRY);
			this.buttonClose.Text = CultureManager.TranslateString(LanguageConstantKeys.L_CLOSE, LanguageConstantValues.L_CLOSE);
			this.dataGridViewValues.Columns[0].HeaderText = CultureManager.TranslateString(LanguageConstantKeys.L_SCD_ATR_ALIAS, LanguageConstantValues.L_SCD_ATR_ALIAS);
			this.dataGridViewValues.Columns[1].HeaderText = CultureManager.TranslateString(LanguageConstantKeys.L_SCD_ATR_PREVIOUSVALUE, LanguageConstantValues.L_SCD_ATR_PREVIOUSVALUE);
			this.dataGridViewValues.Columns[2].HeaderText = CultureManager.TranslateString(LanguageConstantKeys.L_SCD_ATR_CURRENTVALUE, LanguageConstantValues.L_SCD_ATR_CURRENTVALUE);
			if (mAllowRetry)
				this.labelErrorDescription.Text = CultureManager.TranslateString(LanguageConstantKeys.L_SCD_MESSAGE_RETRY, LanguageConstantValues.L_SCD_MESSAGE_RETRY);
			else
				this.labelErrorDescription.Text = CultureManager.TranslateString(LanguageConstantKeys.L_SCD_MESSAGE, LanguageConstantValues.L_SCD_MESSAGE);
		}

		private void ConfigureScenario()
		{
			// Show or hide the Retry button.
			buttonRetry.Visible = mAllowRetry;

			// Load values and resize form
			if (mShowValues)
			{
				this.dataGridViewValues.Visible = true;

				// Load rows in the differences Grid.
				foreach (string key in mNewValues.Keys)
				{
					// Format the Description message
					string lArgName = key.Substring(0,key.IndexOf('.'));
					string lExpression = key.Substring(key.IndexOf('.') + 1);
					DisplaySetItem lSCDAttribute = null;
					KeyValuePair<Oid, DisplaySetInformation> lArgInfo = new KeyValuePair<Oid, DisplaySetInformation>(); ;
					if (mSCDInfo.ContainsKey(lArgName))
					{
						lArgInfo = mSCDInfo[lArgName];

						foreach (DisplaySetItem lItem in lArgInfo.Value.DisplaySetItems)
						{
							if (lItem.Name.Equals(lExpression, StringComparison.InvariantCultureIgnoreCase))
							{
								lSCDAttribute = lItem;
								break;
							}
						}

					}
					string lDescription = "";
					string lPreviousValue = "";
					string lNewValue = "";

					// Check SCD Attribute info and previous value info
					if (lSCDAttribute != null && lArgInfo.Key.SCDAttributesValues.ContainsKey(key))
					{
						lDescription = CultureManager.TranslateString(lSCDAttribute.IdXML, lSCDAttribute.Alias);
						if (lSCDAttribute.ModelType != ModelType.Oid)
						{
							// Data valued attributes
							lPreviousValue = Presentation.DefaultFormats.ApplyDisplayFormat(lArgInfo.Key.SCDAttributesValues[key], lSCDAttribute.ModelType);
							lNewValue = Presentation.DefaultFormats.ApplyDisplayFormat(mNewValues[key], lSCDAttribute.ModelType);
						}
						else
						{
							// Object valued attributes
							lPreviousValue = GetInfoFromOid(lArgInfo.Key.SCDAttributesValues[key] as Oid, lSCDAttribute.SupplementaryInfo);
							lNewValue = GetInfoFromOid(mNewValues[key] as Oid, lSCDAttribute.SupplementaryInfo);
						}
					}


					object[] lRowArray = {lDescription,lPreviousValue, lNewValue};
					this.dataTableValues.Rows.Add(lRowArray);
				}
			}
			else
			{
				// Hide Grid and resize form
				this.dataGridViewValues.Visible = false;
				this.Height -= this.dataGridViewValues.Height;
			}
		}
		
		private string GetInfoFromOid(Oid oid, DisplaySetInformation supplementaryInfo)
		{
			if (oid == null)
				return "";

			// If no Supplementary information is requested, return the Oid values
			if (supplementaryInfo == null)
			{
				return UtilFunctions.OidFieldsToString(oid, ' ');
			}

			// Query to obtain the supplementary information values
			string displaySet = supplementaryInfo.DisplaySetItemsAsString();
			DataTable lDataTable = null;
			try
			{
				lDataTable = Logic.ExecuteQueryInstance(Logic.Agent, oid, displaySet);
			}
			catch
			{
				return UtilFunctions.OidFieldsToString(oid, ' '); ;
			}

			// No data, return empty string
			if (lDataTable == null || lDataTable.Rows.Count == 0)
				return UtilFunctions.OidFieldsToString(oid, ' '); ;

			string lResult = "";
			foreach (DisplaySetItem lItem in supplementaryInfo.DisplaySetItems)
			{
				if (!lResult.Equals(""))
					lResult += " ";

				lResult += DefaultFormats.ApplyDisplayFormat(lDataTable.Rows[0][lItem.Name], lItem.ModelType);
			}

			return lResult;
		}
		#endregion Methods
	}
}


