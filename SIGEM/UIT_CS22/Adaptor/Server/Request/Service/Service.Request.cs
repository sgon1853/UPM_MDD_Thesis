// v3.8.4.5.b

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

using SIGEM.Client.Adaptor.DataFormats;

namespace SIGEM.Client.Adaptor
{
	#region <Service.Request>.
	internal class ServiceRequest
	{
		#region Construtctors.
		public ServiceRequest() { }
		public ServiceRequest(
		string className,
		string serviceName,
		Arguments arguments)
		{
			this.Class = className;
			this.Name = serviceName;
			this.Arguments = (arguments == null ? new Arguments() : arguments);
		}
		public ServiceRequest(string className, string serviceName)
			: this(className, serviceName, null) { }
		#endregion Construtctors.

		private string mClass = string.Empty;
		public string Class
		{
			get
			{
				return mClass;
			}
			set
			{
				mClass = value;
			}
		}

		private string mName = string.Empty;
		public string Name
		{
			get
			{
				return mName;
			}
			set
			{
				mName = value;
			}
		}

		private Arguments mArguments = null;
		public Arguments Arguments
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

		private Dictionary<string,KeyValuePair<ModelType,object>> mGArguments = null;
		public Dictionary<string,KeyValuePair<ModelType,object>> GArguments
		{
			get
			{
				return mGArguments ;
			}
			set
			{
				mGArguments  = value;
			}
		}

		#region Change Detection Items
		private ChangeDetectionItems mItems = new ChangeDetectionItems();
		/// <summary>
		/// Gets the change detection items.
		/// </summary>
		/// <value>The change detection items.</value>
		
		//public IList<ChangeDetectionItem> ChangeDetectionItems

		public ChangeDetectionItems ChangeDetectionItems
		{
			get { return mItems; }
			set { mItems = value; }
		}	   

		#endregion  Change Detection Items
	}
	#endregion <Service.Request>.
}

