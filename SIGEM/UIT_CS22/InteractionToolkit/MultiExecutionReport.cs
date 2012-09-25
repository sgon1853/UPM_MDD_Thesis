// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SIGEM.Client.InteractionToolkit
{
	public partial class MultiExecutionReport : Form
	{
		public MultiExecutionReport(String serviceAlias)
		{
			InitializeComponent();
			this.Text = CultureManager.TranslateStringWithParams(LanguageConstantKeys.L_MULTIEXE_EXECUTION_REPORT, LanguageConstantValues.L_MULTIEXE_EXECUTION_REPORT, serviceAlias);

			// Apply MultiLanguage.
			this.Ok.Text = CultureManager.TranslateString(LanguageConstantKeys.L_CLOSE, LanguageConstantValues.L_CLOSE, this.Ok.Text);
		}
		public void SetFormat(DataTable report, Dictionary<string, KeyValuePair<string, string>> inboundArgument, Dictionary<string, KeyValuePair<string, string>> outboundArgument)
		{
			foreach (DataColumn column in report.Columns)
			{
				DataGridViewColumn lColumn = new DataGridViewTextBoxColumn();
				lColumn.DataPropertyName = column.ColumnName;
				lColumn.Name = column.ColumnName;
				//Alias for the inboundArguments.
                if (inboundArgument != null && inboundArgument.ContainsKey(column.ColumnName))
				{
					KeyValuePair<String, String> argument = inboundArgument[column.ColumnName];
					lColumn.HeaderText = CultureManager.TranslateString(argument.Key, argument.Value);
				}
				//Alias for the outboundArguments.
				else
				{
                    if (outboundArgument != null && outboundArgument.ContainsKey(column.ColumnName))
					{
						KeyValuePair<String, String> argument = outboundArgument[column.ColumnName];
						lColumn.HeaderText = CultureManager.TranslateString(argument.Key, argument.Value);
					}
					else
					{
						lColumn.HeaderText = column.ColumnName;
					}
				}
				dataGridView1.Columns.Add(lColumn);
			}
		}
		public void ShowReport(DataTable report)
		{
			this.dataGridView1.DataSource = report;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
