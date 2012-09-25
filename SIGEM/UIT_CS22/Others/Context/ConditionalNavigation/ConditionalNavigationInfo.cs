// v3.8.4.5.b
using System;
using System.Collections.Generic;
using System.Text;

namespace SIGEM.Client
{
	#region enum ConditionalNavigationInfoType
	/// <summary>
	/// 'ConditionalNavigationInfoType' enum values.
	/// </summary>
	/// <remarks>Type of the condition satisfied.</remarks>
	[Serializable]
	public enum ConditionalNavigationInfoType
	{
		Success,
		Error
	}
	#endregion enum ConditionalNavigationInfoType

	/// <summary>
	/// Class 'ConditionalNavigationInfo'.
	/// </summary>
	[Serializable]
	public class ConditionalNavigationInfo : List<DestinationInfo>
	{
		#region Members
		/// <summary>
		/// Type of the conditional navigation information.
		/// </summary>
		private ConditionalNavigationInfoType mConditionalNavigationInfoType = ConditionalNavigationInfoType.Success;
		/// <summary>
		/// Question message.
		/// </summary>
		private string mQuestion = string.Empty;
		#endregion Members

		#region Properties
		/// <summary>
		/// Gets or sets the type of the conditional navigation information.
		/// </summary>
		public ConditionalNavigationInfoType ConditionalNavigationInfoType
		{
			get
			{
				return mConditionalNavigationInfoType;
			}
			protected set
			{
				mConditionalNavigationInfoType = value;
			}
		}
		/// <summary>
		/// Gets or sets the question message.
		/// </summary>
		public string Question
		{
			get
			{
				return mQuestion;
			}
			protected set
			{
				mQuestion = value;
			}
		}
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the 'ConditionalNavigationInfo' class.
		/// </summary>
		/// <param name="question">Question message.</param>
		/// <param name="contidionalType">Type of the conditional navigation information.</param>
		public ConditionalNavigationInfo(
			string question,
			ConditionalNavigationInfoType contidionalType)
		{
			Question = question;
			ConditionalNavigationInfoType = contidionalType;
		}
		#endregion Constructors
	}

}

