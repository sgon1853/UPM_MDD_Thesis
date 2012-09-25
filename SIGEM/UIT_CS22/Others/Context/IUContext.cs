// v3.8.4.5.b
using System;
using System.Collections.Generic;

using SIGEM.Client.Oids;

namespace SIGEM.Client
{
	/// <summary>
	/// Class 'IUContext'.
	/// </summary>
	[Serializable]
	public class IUContext
	{
		#region Members
		/// <summary>
		/// Instance of Previous.
		/// </summary>
		private IUContext mPrevious = null;
		/// <summary>
		/// Context Type.
		/// </summary>
		private ContextType mContextType;
		/// <summary>
		/// RelationType.
		/// </summary>
		private RelationType mRelationType;
		/// <summary>
		/// Instance of Others.
		/// </summary>
		private Dictionary<string, object> mOthers = new Dictionary<string,object>(StringComparer.CurrentCultureIgnoreCase);
		/// <summary>
		/// Interaction unit name.
		/// </summary>
		private string mIuName;
		/// <summary>
		/// Context initialized?.
		/// </summary>
		private bool mInitialized = false;
		/// <summary>
		/// Exchange information.
		/// </summary>
		private ExchangeInfo mExchangeInformation;
		#endregion Members

		#region Properties
		/// <summary>
		/// Gets or sets the agent connected to the application.
		/// </summary>
		public AgentInfo Agent
		{
			get
			{
				return Logics.Logic.Agent;
			}
			set
			{
				Logics.Logic.Agent = value;
			}
		}
		/// <summary>
		/// Gets or sets previous.
		/// </summary>
		public IUContext Previous
		{
			get
			{
				return mPrevious;
			}
		}
		/// <summary>
		/// Gets or sets context type.
		/// </summary>
		public ContextType ContextType
		{
			get
			{
				return mContextType;
			}
			protected set
			{
				mContextType= value;
			}
		}
		/// <summary>
		/// Gets or sets relation type.
		/// </summary>
		public RelationType RelationType
		{
			get
			{
				return mRelationType;
			}
			protected set
			{
				mRelationType = value;
			}
		}
		/// <summary>
		/// Gets or sets IU name.
		/// </summary>
		public string IuName
		{
			get
			{
				return mIuName;
			}
			private set
			{
				mIuName = value;
			}
		}
		/// <summary>
		/// Gets or sets a boolean value indicating whether the context has been initialized or not.
		/// </summary>
		public bool Initialized
		{
			get
			{
				return mInitialized;
			}
			set
			{
				mInitialized = value;
			}
		}
		/// <summary>
		/// Gets or sets exchange information.
		/// </summary>
		public ExchangeInfo ExchangeInformation
		{
			get
			{
				return mExchangeInformation;
			}
			set
			{
				mExchangeInformation = value;
			}
		}
		/// <summary>
		/// Gets the general purpose dictionary (custom purposes).
		/// </summary>
		public Dictionary<string, object> Others
		{
			get
			{
				return mOthers;
			}
		}
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'IUContext' class.
		/// </summary>
		/// <param name="exchangeInfo">Exchange information.</param>
		/// <param name="contextType">Context type.</param>
		/// <param name="iuName">Interaction unit name.</param>
		public IUContext(ExchangeInfo exchangeInfo, ContextType contextType, string iuName)
		{
			ExchangeInformation = exchangeInfo;
			ContextType = contextType;
			mIuName = iuName;
		}
		/// <summary>
		/// Initializes a new instance of the 'IUContext' class.
		/// </summary>
		/// <param name="contextType">Context type.</param>
		/// <param name="relationType">Relation type.</param>
		public IUContext(ContextType contextType, RelationType relationType)
			: this(null, contextType, string.Empty)
		{
			RelationType = relationType;
		}
		/// <summary>
		/// Initializes a new instance of 'IUContext'.
		/// </summary>
		/// <param name="previous">Previous.</param>
		/// <param name="contextType">Context type.</param>
		/// <param name="relationType">Relation type.</param>
		/// <param name="iuName">IU name.</param>
		public IUContext(IUContext previous, ContextType contextType, RelationType relationType, string iuName)
			:this(contextType, relationType, iuName)
		{
			mPrevious = previous;
		}
		/// <summary>
		/// Initializes a new instance of 'IUContext'.
		/// </summary>
		/// <param name="contextType">Context type.</param>
		/// <param name="relationType">Relation type.</param>
		/// <param name="iuName">IU name.</param>
		private IUContext(ContextType contextType, RelationType relationType, string iuName)
		{
			mContextType = contextType;
			mRelationType = relationType;
			mIuName = iuName;
		}
		#endregion Constructors
	}
}
