//------------------------------------------------------------------------------
// <auto-generated>
//	 This code was generated by a tool.
//	 Runtime Version:2.0.50727.42
//
//	 Changes to this file may cause incorrect behavior and will be lost if
//	 the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SIGEM.Client.Properties
{
	[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
	[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "8.0.0.0")]
	internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase
	{
		private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));

		public static Settings Default
		{
			get
			{
				return defaultInstance;
			}
		}

		[global::System.Configuration.ApplicationScopedSettingAttribute()]
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
		[global::System.Configuration.DefaultSettingValueAttribute("100")]
		public int DependencyRulesCounter
		{
			get
			{
				return ((int)(this["DependencyRulesCounter"]));
			}
		}
		[global::System.Configuration.ApplicationScopedSettingAttribute()]
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
		[global::System.Configuration.DefaultSettingValueAttribute("Reports.config.xml")]
		public string ConfigurationOfReports {
			get {
				return ((string)(this["ConfigurationOfReports"]));
			}
			set
			{
				this["ConfigurationOfReports"] = value;
			}
		}
		[global::System.Configuration.ApplicationScopedSettingAttribute()]
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
		[global::System.Configuration.DefaultSettingValueAttribute("net://localhost:SIGEM")]
		public string ConnectionString {
			get
			{
				return ((string)(this["ConnectionString"]));
			}
		}
		[global::System.Configuration.ApplicationScopedSettingAttribute()]
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
		[global::System.Configuration.DefaultSettingValueAttribute("file://UserPrefs.xml")]
		public string ConnectionStringUserPreferences {
			get
			{
				return ((string)(this["ConnectionStringUserPreferences"]));
			}
			set
			{
				this["ConnectionStringUserPreferences"] = value;
			}
		}
		[global::System.Configuration.ApplicationScopedSettingAttribute()]
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
		[global::System.Configuration.DefaultSettingValueAttribute("AND")]
		public string ConjunctionPolicy {
			get
			{
				return ((string)(this["ConjunctionPolicy"]));
			}
		}
		[global::System.Configuration.ApplicationScopedSettingAttribute()]
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
		[global::System.Configuration.DefaultSettingValueAttribute("False")]
		public bool UserPreferencesByUser {
			get {
				return ((bool)(this["UserPreferencesByUser"]));
			}
		}
		[global::System.Configuration.ApplicationScopedSettingAttribute()]
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
		[global::System.Configuration.DefaultSettingValueAttribute("MainMenuReports.xml")]
		public string MainMenuReports {
			get {
				return ((string)(this["MainMenuReports"]));
			}
		}
	}
}