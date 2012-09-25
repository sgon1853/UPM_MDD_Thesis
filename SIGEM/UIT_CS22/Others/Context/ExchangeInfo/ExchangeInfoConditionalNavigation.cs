// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;

using SIGEM.Client.Oids;

namespace SIGEM.Client
{
	#region ExchangeInfoConditionalNavigation
	public partial class ExchangeInfoConditionalNavigation :
		ExchangeInfoAction
	{

		private ConditionalNavigationInfo mConditionalNavigationInfo = null;

		private DestinationInfo mDestinationInfo = null;

		public ExchangeInfoConditionalNavigation(
			ConditionalNavigationInfo conditionalNavigationInfo,
			ExchangeInfoAction exchangeInfoAction)
			: this(conditionalNavigationInfo, exchangeInfoAction.ClassName, exchangeInfoAction.IUName, exchangeInfoAction.SelectedOids, exchangeInfoAction.Previous)
		{
			CustomData = exchangeInfoAction.CustomData;
		}

		public ExchangeInfoConditionalNavigation(
			ConditionalNavigationInfo conditionalNavigationInfo,
			string className,
			string iuName,
			List<Oid> selectedOids,
			IUContext previousContext)
			: base(className, iuName, selectedOids, previousContext)
		{
			this.ExchangeType = ExchangeType.ConditionalNavigation;
			ConditionalNavigationInfo = conditionalNavigationInfo;
		}

		public ConditionalNavigationInfo ConditionalNavigationInfo
		{
			get
			{
				return mConditionalNavigationInfo;
			}
			protected set
			{
				mConditionalNavigationInfo = value;
			}
		}

		public void SelectDestinationInfo(int index)
		{
			if (index < ConditionalNavigationInfo.Count)
			{
				DestinationInfo = ConditionalNavigationInfo[index];
			}

		}
		public DestinationInfo DestinationInfo
		{
			get
			{
				return mDestinationInfo;
			}
			protected set
			{
				mDestinationInfo = value;
			}
		}
	}
	#endregion ExchangeInfoConditionalNavigation
}
