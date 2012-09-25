// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

using SIGEM.Client.Oids;

namespace SIGEM.Client.Adaptor
{
	#region ArgumentsList
	/// <summary>
	/// Stores and manages arguments in an ordered list.
	/// </summary>
	public partial class ArgumentsList
	{
		#region Static Methods
		#region GetArgumentsFromContext
		/// <summary>
		/// Obtains the ArgumentsList contained in a IUInputFieldsContext (either an IUServiceContext or an IUFilterContext).
		/// </summary>
		/// <param name="context">Context the list is obtained from.</param>
		/// <returns>ArgumentsList contained in the context.</returns>
		public static ArgumentsList GetArgumentsFromContext(IUInputFieldsContext context)
		{
			if (context != null)
			{
				Dictionary<string, ModelType> largumentTypes = null;

				// Context is of IUFilterContext type
				if (context.ContextType == ContextType.Service)
				{
					// Get types of service arguments
					return GetArgumentListFromArgumentInfoContext((IUServiceContext)context);
				}

				// Context is of IUFilterContext type
				if (context.ContextType == ContextType.Filter)
				{
					// Get types of filter variables
					largumentTypes = Logics.Logic.GetVariablesTypes(context.ClassName, context.ContainerName);
					if (largumentTypes != null)
					{
						return GetArgumentListFromArgumentInfoContext(context.InputFields, largumentTypes);
					}
				}
			}
			return null;
		}
		#endregion GetArgumentsFromContext

		#region GetArgumentListFromArgumentInfoContext
		/// <summary>
		/// Obtains the ArgumentsList contained in a IUServiceContext.
		/// </summary>
		/// <param name="context">Context the list is obtained from.</param>
		/// <returns>ArgumentsList contained in the context.</returns>
		private static ArgumentsList GetArgumentListFromArgumentInfoContext(IUServiceContext context)
		{
			Dictionary<string, ModelType> largumentTypes = null;
			ArgumentsList lResult = null;

			largumentTypes = Logics.Logic.GetInboundArgumentTypes(context.ClassName, context.ContainerName);
			if (largumentTypes != null)
			{
				lResult = GetArgumentListFromArgumentInfoContext(context.InputFields, largumentTypes);
			}

			largumentTypes = Logics.Logic.GetOutboundArgumentTypes(context.ClassName, context.ContainerName);
			if (largumentTypes != null)
			{
				if (context.OutputFields != null && context.OutputFields.Count > 0)
				{
					foreach (KeyValuePair<string, ModelType> largumentType in largumentTypes)
					{
						object argumentInfo = context.OutputFields[largumentType.Key];

						if (argumentInfo != null)
						{
							if (largumentType.Value == ModelType.Oid)
							{
								List<Oid> lOids = argumentInfo as List<Oid>;
								if (lOids != null && lOids.Count == 1)
								{
									lResult.Add(largumentType.Key, largumentType.Value, lOids[0], lOids[0].ClassName);
								}
							}
							else
							{
								if (argumentInfo != null)
								{
								lResult.Add(largumentType.Key, largumentType.Value, argumentInfo, string.Empty);
								}
							}
						}
					}
				}
			}

			if (lResult != null)
			{
				return lResult;
			}

			return null;
		}

		/// <summary>
		/// Obtains the ArgumentsList contained in a IUServiceContext.
		/// </summary>
		/// <param name="arguments">Context Arguments.</param>
		/// <param name="argumentTypes">Arguments Types.</param>
		/// <returns>ArgumentsList contained in the context.</returns>
		private static ArgumentsList GetArgumentListFromArgumentInfoContext(
			Dictionary<string, IUContextArgumentInfo> arguments,
			Dictionary<string, ModelType> argumentTypes)
		{
			ArgumentsList lResult = null;
			if ((arguments != null) && (argumentTypes != null))
			{
				lResult = new ArgumentsList();
				foreach (KeyValuePair<string, ModelType> largumentType in argumentTypes)
				{
					IUContextArgumentInfo argumentInfo = arguments[largumentType.Key];

					if (argumentInfo != null)
					{
						if (largumentType.Value == ModelType.Oid)
						{
							List<Oid> lOids = argumentInfo.Value as List<Oid>;
							if (lOids != null && lOids.Count == 1)
							{
								lResult.Add(largumentType.Key, largumentType.Value, lOids[0], lOids[0].ClassName);
							}
						}
						else
						{
							if (argumentInfo.Value != null)
							{
								lResult.Add(largumentType.Key, largumentType.Value, argumentInfo.Value, string.Empty);
							}
						}
					}
				}
			}
			return lResult;
		}
		#endregion GetArgumentListFromArgumentInfoContext

		#endregion Static Methods
	}
	#endregion ArgumentsList

}
