// v3.8.4.5.b
using System;

namespace SIGEM.Client
{
	/// <summary>
	/// Class that defines the Introduction Pattern structure.
	/// </summary>
	public class IntroductionPattern
	{
		#region Members
		/// <summary>
		/// Data type.
		/// </summary>
		private ModelType mDataType = ModelType.String;
		/// <summary>
		/// Introduction pattern 'Minimum value' property.
		/// </summary>
		private decimal? mMinValue = null;
		/// <summary>
		/// Introduction pattern 'Maximum value' property.
		/// </summary>
		private decimal? mMaxValue = null;
		/// <summary>
		/// Introduction pattern 'Maximum integer digits' property.
		/// </summary>
		private int? mMaxIntegerDigits = null;
		/// <summary>
		/// Introduction pattern 'Minimum decimal digits' property.
		/// </summary>
		private int mMinDecimalDigits = 0;
		/// <summary>
		/// Introduction pattern 'Maximum decimal digits' property.
		/// </summary>
		private int mMaxDecimalDigits = 0;
		/// <summary>
		/// Introduction pattern 'Mask' property.
		/// </summary>
		private string mMask = null;
		/// <summary>
		/// Introduction pattern 'Validation message' property.
		/// </summary>
		private string mIPValidationMessage = null;
		#endregion Members

		#region Constructors
		/// <summary>
		/// Creates a new instance of the 'IntroductionPattern' class for numeric values (Int or Nat).
		/// </summary>
		/// <param name="dataType">Data type of the introduction pattern.</param>
		/// <param name="minValue">Minimum value of the introduction pattern.</param>
		/// <param name="maxValue">Maximum value of the introduction pattern.</param>
		/// <param name="maxIntegerDigits">Maximum integer digits of the introduction pattern.</param>
		/// <param name="ipValidationMessage">Validation message of the introduction pattern.</param>
		public IntroductionPattern(
			ModelType dataType,
			decimal? minValue,
			decimal? maxValue,
			string ipValidationMessage)
		{
			DataType = dataType;
			MinValue = minValue;
			MaxValue = maxValue;

			if (minValue == null || maxValue == null)
			{
				// If one of the two limits is not specified, it can not be restricted the max number of digits.
				mMaxIntegerDigits = null;
			}
			else
			{
				// Calculate the maximum number of integer positions, based on the max value and the min value.
				int? lMaxIntegerDigitsFromMaxValue = null;
				int? lMaxIntegerDigitsFromMinValue = null;
				if (maxValue != null)
				{
					lMaxIntegerDigitsFromMaxValue = 1;
					if (maxValue.Value != 0)
					{
						maxValue = Math.Abs(maxValue.Value); // Get the unsigned value, because Log10 returns NaN for negative values.
						double lMaxValue = Math.Truncate(Math.Abs(Math.Log10((double)maxValue.Value))) + 1;
						lMaxIntegerDigitsFromMaxValue = Convert.ToInt32(lMaxValue);
					}
				}
				if (minValue != null)
				{
					lMaxIntegerDigitsFromMinValue = 1;
					if (minValue.Value != 0)
					{
						minValue = Math.Abs(minValue.Value); // Get the unsigned value, because Log10 returns NaN for negative values.
						double lMinValue = Math.Truncate(Math.Abs(Math.Log10((double)minValue.Value))) + 1;
						lMaxIntegerDigitsFromMinValue = Convert.ToInt32(lMinValue);
					}
				}
				if (lMaxIntegerDigitsFromMaxValue != null && lMaxIntegerDigitsFromMinValue != null)
				{
					// Get the maximum value between the max number of digits for maxValue and the max number of digits for minValue.
					mMaxIntegerDigits = Math.Max(lMaxIntegerDigitsFromMaxValue.Value, lMaxIntegerDigitsFromMinValue.Value);
				}
			}

			IPValidationMessage = ipValidationMessage;
		}
		/// <summary>
		/// Creates a new instance of the 'IntroductionPattern' class for numeric values (Real).
		/// </summary>
		/// <param name="dataType">Data type of the introduction pattern.</param>
		/// <param name="minValue">Minimum value of the introduction pattern.</param>
		/// <param name="maxValue">Maximum value of the introduction pattern.</param>
		/// <param name="maxIntegerDigits">Maximum integer digits of the introduction pattern.</param>
		/// <param name="minDecimalDigits">Minimum decimal digits of the introduction pattern.</param>
		/// <param name="maxDecimalDigits">Maximum decimal digits of the introduction pattern.</param>
		/// <param name="ipValidationMessage">Validation message of the introduction pattern.</param>
		public IntroductionPattern(
			ModelType dataType,
			decimal? minValue,
			decimal? maxValue,
			int minDecimalDigits,
			int maxDecimalDigits,
			string ipValidationMessage)
			: this(dataType, minValue, maxValue, ipValidationMessage)
		{
			MinDecimalDigits = minDecimalDigits;
			MaxDecimalDigits = maxDecimalDigits;
		}
		/// <summary>
		/// Creates a new instance of the 'IntroductionPattern' class for masked values.
		/// </summary>
		/// <param name="dataType">Data type of the introduction pattern.</param>
		/// <param name="mask">Mask of the introduction pattern.</param>
		/// <param name="ipValidationMessage">Validation message of the introduction pattern.</param>
		public IntroductionPattern(
			ModelType dataType,
			string mask,
			string ipValidationMessage)
		{
			DataType = dataType;
			Mask = mask;
			IPValidationMessage = ipValidationMessage;
		}
		#endregion Constructors

		#region Properties
		/// <summary>
		/// Gets the data type of the introduction pattern.
		/// </summary>
		public ModelType DataType
		{
			get
			{
				return mDataType;
			}
			protected set
			{
				mDataType = value;
			}
		}
		/// <summary>
		/// Gets the 'minimum value' of the introduction pattern (numeric).
		/// </summary>
		public decimal? MinValue
		{
			get
			{
				return mMinValue;
			}
			protected set
			{
				mMinValue = value;
			}
		}
		/// <summary>
		/// Gets the 'maximum value' of the introduction pattern (numeric).
		/// </summary>
		public decimal? MaxValue
		{
			get
			{
				return mMaxValue;
			}
			protected set
			{
				mMaxValue = value;
			}
		}
		/// <summary>
		/// Gets the 'maximum integer digits' of the introduction pattern (numeric).
		/// </summary>
		public int? MaxIntegerDigits
		{
			get
			{
				return mMaxIntegerDigits;
			}
			protected set
			{
				mMaxIntegerDigits = value;
			}
		}
		/// <summary>
		/// Gets the 'minimum decimal digits' of the introduction pattern (numeric).
		/// </summary>
		public int MinDecimalDigits
		{
			get
			{
				return mMinDecimalDigits;
			}
			protected set
			{
				mMinDecimalDigits = value;
			}
		}
		/// <summary>
		/// Gets the introduction pattern 'Maximum decimal digits' property.
		/// </summary>
		public int MaxDecimalDigits
		{
			get
			{
				return mMaxDecimalDigits;
			}
			protected set
			{
				mMaxDecimalDigits = value;
			}
		}
		/// <summary>
		/// Gets the introduction pattern 'Mask' property.
		/// </summary>
		public string Mask
		{
			get
			{
				return mMask;
			}
			protected set
			{
				mMask = value;
			}
		}
		/// <summary>
		/// Gets the introduction pattern 'Validation message' property.
		/// </summary>
		public string IPValidationMessage
		{
			get
			{
				return mIPValidationMessage;
			}
			protected set
			{
				mIPValidationMessage = value;
			}
		}
		#endregion Properties
	}
}


