// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;


using SIGEM.Client.Adaptor.DataFormats;

namespace SIGEM.Client.Adaptor
{
	#region NavigationFiltering
	/// <summary>
	/// In an scenario when an Oid could be selected and is allowed
	/// any navigation along the model associated with the selected Oid,
	/// performs other scenario where appears information filtered or related by this Oid.
	/// This kind of navigations are called Navigational Filtering.
	/// This class stores the information related to a Navigational Filtering.
	/// </summary>
	public partial class NavigationalFiltering
	{
		#region Constructors
		/// <summary>
		/// Initializes a new empty instance of NavigationalFiltering.
		/// </summary>
		public NavigationalFiltering()
		{
		}
		/// <summary>
		/// Initializes a new instance of NavigationalFiltering when applied to a filter variable
		/// </summary>
		/// <param name="argument"></param>
		public NavigationalFiltering(FilterVariableNavigationFiltering filterVariable)
		{
			FilterVariable = filterVariable;
		}
		/// <summary>
		/// Initializes a new instance of NavigationalFiltering class when it is applied in a service argument.
		/// </summary>
		/// <param name="argument"></param>
		public NavigationalFiltering(ArgumentNavigationFiltering argument)
		{
			Argument = argument;
		}
		/// <summary>
		/// Initializes a new instance of NavigationalFiltering class when it is applied in a selected object.
		/// </summary>
		/// <param name="selectedObject"></param>
		public NavigationalFiltering(SelectedObjectNavigationFiltering selectedObject)
		{
			SelectedObject = selectedObject;
		}
		/// <summary>
		/// Initializes a new instance of NavigationalFiltering class when it is applied to a conditional navigation from a ServiceIU.
		/// </summary>
		/// <param name="serviceIU"></param>
		public NavigationalFiltering(ServiceIUNavigationFiltering serviceIU)
		{
			ServiceIU = serviceIU;
		}
		#endregion Constructors

		#region ArgumentNavigationFiltering
		private ArgumentNavigationFiltering mArgument = null;
		/// <summary>
		/// Gets or sets the argument of the filter used by the navigation.
		/// </summary>
		public ArgumentNavigationFiltering Argument
		{
			get
			{
				return mArgument;
			}
			set
			{
				if (value != null)
				{
					FilterVariable = null;
					SelectedObject = null;
					ServiceIU = null;
				}
				mArgument = value;
			}
		}
		#endregion ArgumentNavigationFiltering

		#region ArgumentNavigationFiltering
		private FilterVariableNavigationFiltering mFilterVariable = null;
		/// <summary>
		/// Gets or sets the argument of the filter used by the navigation.
		/// </summary>
		public FilterVariableNavigationFiltering FilterVariable
		{
			get
			{
				return mFilterVariable;
			}
			set
			{
				if (value != null)
				{
					SelectedObject = null;
					ServiceIU = null;
					Argument = null;
				}
				mFilterVariable = value;
			}
		}
		#endregion ArgumentNavigationFiltering

		#region SelectedObject
		private SelectedObjectNavigationFiltering mSelectedObject = null;
		/// <summary>
		/// Gets or sets the SelectedObject navigational filtering.
		/// </summary>
		public SelectedObjectNavigationFiltering SelectedObject
		{
			get
			{
				return mSelectedObject;
			}
			set
			{
				if (value != null)
				{
					FilterVariable = null;
					Argument = null;
					ServiceIU = null;
				}
				mSelectedObject = value;
			}
		}
		#endregion SelectedObject

		#region ServiceIU
		private ServiceIUNavigationFiltering mServiceIU = null;
		/// <summary>
		/// Gets or sets the SelectedObject navigational filtering.
		/// </summary>
		public ServiceIUNavigationFiltering ServiceIU
		{
			get
			{
				return mServiceIU;
			}
			set
			{
				if (value != null)
				{
					FilterVariable = null; 
					Argument = null;
					SelectedObject = null;
				}
				mServiceIU = value;
			}
		}
		#endregion ServiceIU

		#region IsNavigationalArgument
		/// <summary>
		/// Gets if the arguments of the NavigationalFiltering are indicated.
		/// </summary>
		public bool IsNavigationalArgument
		{
			get
			{
				return (Argument == null ? false : true);
			}
		}
		#endregion IsNavigationalArgument

		#region IsNavigationalFilterVariable
		/// <summary>
		/// Gets if the navigational filtering applies to a conditional navigation from a filter interaction unit
		/// </summary>
		public bool IsNavigationalFilterVariable
		{
			get
			{
				return (FilterVariable == null ? false : true);
			}
		}
		#endregion IsNavigationalFilterVariable

		#region IsNavigationalSelectObject
		/// <summary>
		/// Gets if the selected Oid of the NavigationalFiltering is indicated.
		/// </summary>
		public bool IsNavigationalSelectObject
		{
			get
			{
				return (SelectedObject == null ? false : true);
			}
		}
		#endregion IsNavigationalSelectObject

		#region IsNavigationalServiceIU
		/// <summary>
		/// Gets if the navigational filtering applies to a conditional navigation from a service interaction unit
		/// </summary>
		public bool IsNavigationalServiceIU
		{
			get
			{
				return (ServiceIU == null ? false : true);
			}
		}
		#endregion IsNavigationalServiceIU
	}
	#endregion NavigationFiltering

	#region NavigationFilteringArgument
	/// <summary>
	/// Stores the information related to the arguments of a NavigationalFiltering.
	/// </summary>
	public class ArgumentNavigationFiltering: NavigationFilteringDataColumns
	{
		#region Constructors
		/// <summary>
		/// Initializes a new empty instance of ArgumentNavigationFiltering.
		/// </summary>
		public ArgumentNavigationFiltering()
		{ }
		/// <summary>
		/// Initializes a new instance of ArgumentNavigationFiltering.
		/// </summary>
		/// <param name="className">Class that initiates the navigation.</param>
		/// <param name="serviceName">Service invoked in the navigation.</param>
		/// <param name="argumentName">Indicates the filter argument group.</param>
		/// <param name="arguments">Argument list.</param>
		public ArgumentNavigationFiltering(
			string className,
			string serviceName,
			string argumentName,
			ArgumentsList arguments)
			:base(className, serviceName, argumentName, arguments){}
		#endregion Constructors

		#region ServiceName
		private string mServiceName = string.Empty;
		/// <summary>
		/// Gets or sets the service invoked in the navigation.
		/// </summary>
		public string ServiceName
		{
			get
			{
				return base.InteractionUnitName;
			}
			set
			{
				base.InteractionUnitName = value;
			}
		}
		#endregion ServiceName
	}
	#endregion NavigationFilteringArgument
	
	#region NavigationFilteringFilterVariable
	/// <summary>
	/// Stores the information related to the arguments of a NavigationalFiltering.
	/// </summary>
	public class FilterVariableNavigationFiltering :
		NavigationFilteringDataColumns
	{
		#region Constructors
		/// <summary>
		/// Initializes a new empty instance of ArgumentNavigationFiltering.
		/// </summary>
		public FilterVariableNavigationFiltering()
		{ }
		/// <summary>
		/// Initializes a new instance of ArgumentNavigationFiltering.
		/// </summary>
		/// <param name="className">Class that initiates the navigation.</param>
		/// <param name="serviceName">Service invoked in the navigation.</param>
		/// <param name="argumentName">Indicates the filter argument group.</param>
		/// <param name="arguments">Argument list.</param>
		public FilterVariableNavigationFiltering(
			string className,
			string filterName,
			string argumentName,
			ArgumentsList arguments)
			:base(className, filterName, argumentName, arguments){}
		#endregion Constructors

		#region FilterName
		/// <summary>
		/// Gets or sets the filter invoked in the navigation.
		/// </summary>
		public string FilterName
		{
			get
			{
				return base.InteractionUnitName;
			}
			set
			{
				base.InteractionUnitName = value;
			}
		}
		#endregion FilterName
	}
	#endregion NavigationFilteringFilterVariable

	#region Abstract --> NavigationFilteringDataColumns
	public abstract class NavigationFilteringDataColumns
	{
		#region Constructors
		/// <summary>
		/// Initializes a new empty instance of ArgumentNavigationFiltering.
		/// </summary>
		public NavigationFilteringDataColumns(){}
		public NavigationFilteringDataColumns(
			string className,
			string interactionUnitName,
			string argumentName,
			ArgumentsList arguments)
		{
			ClassName = className;
			InteractionUnitName = interactionUnitName;
			ArgumentName = argumentName;
			Arguments = arguments;
		}
		#endregion Constructors

		#region ClassName
		private string mClassName = string.Empty;
		/// <summary>
		/// Gets or sets the class that initiates the navigation.
		/// </summary>
		public string ClassName
		{
			get
			{
				return mClassName;
			}
			set
			{
				mClassName = value == null ? string.Empty : value;
			}
		}
		#endregion ClassName

		#region InteractionUnitName
		private string mInteractionUnitName = string.Empty;
		/// <summary>
		/// Gets or sets the Interaction Unit invoked in the navigation.
		/// </summary>
		public string InteractionUnitName
		{
			get
			{
				return mInteractionUnitName;
			}
			set
			{
				mInteractionUnitName = value == null ? string.Empty : value;
			}
		}
		#endregion InteractionUnitName

		#region ArgumentName
		private string mArgumentName = string.Empty;
		/// <summary>
		/// Gets or sets the filter argument group.
		/// </summary>
		public string ArgumentName
		{
			get
			{
				return mArgumentName;
			}
			set
			{
				mArgumentName = value == null ? string.Empty : value;
			}
		}
		#endregion ArgumentName

		#region Arguments
		private ArgumentsList mArguments = null;
		/// <summary>
		/// Gets or sets the argument list.
		/// </summary>
		public ArgumentsList Arguments
		{
			get
			{
				return mArguments;
			}
			set
			{
				mArguments = value;
			}
		}
		#endregion Arguments
	}
	#endregion Abstract --> NavigationFilteringDataColumns

	#region SelectedObjectNavigationFiltering
	public class SelectedObjectNavigationFiltering
	{
		#region Constructors
		/// <summary>
		/// Initializes a new empty instance of ArgumentNavigationFiltering.
		/// </summary>
		public SelectedObjectNavigationFiltering()
		{ }

		/// <summary>
		/// Initializes a new empty instance of ArgumentNavigationFiltering.
		/// <param name="navigationalFilterID">ID of the filter to apply</param>
		/// <param name="selectedObjectOID">Oid of the source object of the navigation</param>
		/// </summary>
		public SelectedObjectNavigationFiltering(string navigationalFilterID, Oids.Oid selectedObjectOID)
		{
			NavigationalFilterID = navigationalFilterID;
			SelectedObjectOID = selectedObjectOID;
		}
		#endregion Constructors

		#region NavigationalFilterID
		private string mNavigationalFilterID = string.Empty;
		/// <summary>
		/// Gets or sets the ID of the navigational filter to apply.
		/// </summary>
		public string NavigationalFilterID
		{
			get
			{
				return mNavigationalFilterID;
			}
			set
			{
				mNavigationalFilterID = value == null ? string.Empty : value;
			}
		}
		#endregion NavigationalFilterID

		#region SelectedObjectOID
		private Oids.Oid mSelectedObjectOID = null;
		/// <summary>
		/// Gets or sets the oid of the source object of the navigation.
		/// </summary>
		public Oids.Oid SelectedObjectOID
		{
			get
			{
				return mSelectedObjectOID;
			}
			set
			{
				mSelectedObjectOID = value;
			}
		}
		#endregion SelectedObjectOID
	}
	#endregion SelectedObjectNavigationFiltering

	#region ServiceIUNavigationFiltering
	public class ServiceIUNavigationFiltering
	{
		#region Constructors
		public ServiceIUNavigationFiltering()
		{ }

		public ServiceIUNavigationFiltering(string navigationalFilterID, ArgumentsList arguments)
		{
			NavigationalFilterID = navigationalFilterID;
			Arguments = arguments;
		}
		#endregion Constructors

		#region NavigationalFilterID
		private string mNavigationalFilterID = string.Empty;
		/// <summary>
		/// Gets or sets the ID of the navigational filter to apply.
		/// </summary>
		public string NavigationalFilterID
		{
			get
			{
				return mNavigationalFilterID;
			}
			set
			{
				mNavigationalFilterID = value == null ? string.Empty : value;
			}
		}
		#endregion NavigationalFilterID

		#region Arguments
		private ArgumentsList mArguments = null;
		/// <summary>
		/// Gets or sets the argument list.
		/// </summary>
		public ArgumentsList Arguments
		{
			get
			{
				return mArguments;
			}
			set
			{
				mArguments = value;
			}
		}
		#endregion Arguments
	}
	#endregion ServiceIUNavigationFiltering
}

