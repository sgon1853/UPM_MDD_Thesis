// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using SIGEM.Client.Adaptor;

namespace SIGEM.Client.Oids
{
	/// <summary>
	/// This class mantains information related to 
	/// the communication with the business logic.
	/// </summary>
	[Serializable]
	public abstract class AnonymousAgentInfo : AgentInfo
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'AnonymousAgentInfo' class.
		/// </summary>
		/// <param name="className">Class name.</param>
		/// <param name="alias">Alias.</param>
		/// <param name="idXML">IdXML.</param>
		protected AnonymousAgentInfo(string className, string alias, string idXML)
			: base(className, alias, idXML)
		{
			this.Fields.Add(FieldList.CreateField(string.Empty, ModelType.String));
			base.SetValue(0, "anonymous");
		}
		#endregion Constructors

		#region Methods
		/// <summary>
		/// Clear values.
		/// </summary>
		public override void ClearValues()
		{
		}
		/// <summary>
		/// Clear values.
		/// </summary>
		/// <param name="defaultValue">Default value.</param>
		public override void ClearValues(object defaultValue)
		{
		}
		/// <summary>
		/// Checks whether the anonymous agent object is valid or not.
		/// </summary>
		/// <returns>Returns true if the anomymous agent is valid; Otherwise, returns false.</returns>
		public override bool IsValid()
		{
			return true;
		}
		/// <summary>
		/// Cretate types.
		/// </summary>
		/// <param name="types">Types.</param>
		protected override void CreateTypes(ModelType[] types)
		{
		}
		/// <summary>
		/// Create types.
		/// </summary>
		/// <param name="types">Types.</param>
		protected override void CreateTypes(IList<ModelType> types)
		{
		}
		/// <summary>
		/// Set values.
		/// </summary>
		/// <param name="values">Values.</param>
		public override void SetValues(object[] values)
		{
		}
		/// <summary>
		/// Set values.
		/// </summary>
		/// <param name="values">Values.</param>
		public override void SetValues(IList<object> values)
		{
		}
		/// <summary>
		/// Set value.
		/// </summary>
		/// <param name="index">Index.</param>
		/// <param name="value">Value.</param>
		public override void SetValue(int index, object value)
		{
		}
		#endregion Methods
	}
}

