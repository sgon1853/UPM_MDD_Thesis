// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;

namespace SIGEM.Client.Adaptor
{
	public partial class NavigationalFiltering
	{
		/// <summary>
		/// Determines if the IUContext has a NavigationalFiltering.
		/// </summary>
		/// <param name="context">Context checked if has a NavigationalFiltering.</param>
		/// <returns>True or false, if has a NavigationalContext within or not.</returns>
		public static bool HasNavigationalFiltering(IUContext context)
		{
			return (context.ExchangeInformation == null ? false : context.ExchangeInformation.NavigationalFilter);
		}
		/// <summary>
		/// Obtains the NavigationalFiltering contained in a IUContext.
		/// </summary>
		/// <param name="context">Context the NavigationalFiltering is obtained from.</param>
		/// <returns>NavigationalFiltering contained in the context.</returns>
		public static NavigationalFiltering GetNavigationalFiltering(IUContext context)
		{
			NavigationalFiltering lResult = null;
			if (HasNavigationalFiltering(context))
			{
				switch (context.ExchangeInformation.ExchangeType)
				{
					case ExchangeType.Navigation:
						{
							ExchangeInfoNavigation lExchangeInfo = context.ExchangeInformation as ExchangeInfoNavigation;
							lResult = new NavigationalFiltering(
							new SelectedObjectNavigationFiltering(
							lExchangeInfo.NavigationalFilterIdentity,
							lExchangeInfo.SelectedOids[0]));
						}
						break;
					case ExchangeType.Action:
						{
							ExchangeInfoAction lExchangeInfo = context.ExchangeInformation as ExchangeInfoAction;
							if ((lExchangeInfo.SelectedOids == null) || (lExchangeInfo.SelectedOids.Count == 0) || (lExchangeInfo.SelectedOids[0] == null))
							{
								IUServiceContext lServiceContext = context.ExchangeInformation.Previous as IUServiceContext;


								ArgumentsList lArguments = ArgumentsList.GetArgumentsFromContext(lServiceContext);
								lResult = new NavigationalFiltering(
								new ServiceIUNavigationFiltering(
								lExchangeInfo.NavigationalFilterIdentity,
								lArguments));
							}
							else
							{
								lResult = new NavigationalFiltering(
								new SelectedObjectNavigationFiltering(
								lExchangeInfo.NavigationalFilterIdentity,
								lExchangeInfo.SelectedOids[0]));
							}
						}
						break;
					case ExchangeType.SelectionForward:
						{
							ExchangeInfoSelectionForward lExchangeInfo = context.ExchangeInformation as ExchangeInfoSelectionForward;
							ArgumentsList lArguments = null;
							IUServiceContext lServiceContext = null;
							IUPopulationContext lPopulationContext = null;
							string lClassName = string.Empty;

							// context is of IUServiceContext type.
							if (context.ExchangeInformation.Previous.ContextType == ContextType.Service)
							{
								lServiceContext = context.ExchangeInformation.Previous as IUServiceContext;
								lArguments = ArgumentsList.GetArgumentsFromContext(lServiceContext);
								lClassName = lServiceContext.ClassName;
							if (string.Compare(lClassName, "Global", true) == 0)
							{
								lClassName = string.Empty;
							}

							lResult = new NavigationalFiltering(
								new ArgumentNavigationFiltering(
										lClassName,
										lServiceContext.ServiceName,
										lServiceContext.SelectedInputField,
										lArguments));
							}
							// context is of lPopulationContext type.
							if (context.ExchangeInformation.Previous.ContextType == ContextType.Population)
							{
								lPopulationContext = context.ExchangeInformation.Previous as IUPopulationContext;
								lClassName = lPopulationContext.ClassName;
								string lFilterName = lExchangeInfo.ServiceName;
								string lFilterVariableName = lExchangeInfo.ArgumentName;
								lArguments = ArgumentsList.GetArgumentsFromContext(lPopulationContext.Filters[lFilterName]);

								lResult = new NavigationalFiltering(
									new FilterVariableNavigationFiltering(
										lClassName,
										lFilterName,
										lFilterVariableName,
										lArguments));
							}
						}
						break;
					case ExchangeType.SelectionBackward:
						break;
					default:
						break;
				}
			}
			return lResult;
		}
	}
}
