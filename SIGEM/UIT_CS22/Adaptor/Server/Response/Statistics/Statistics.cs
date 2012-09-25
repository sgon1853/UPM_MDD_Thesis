// v3.8.4.5.b

using System;
using System.Collections.Generic;
using System.Text;

namespace SIGEM.Client.Adaptor
{
	#region <Statistics>.
	internal class Statistics
	{
		public Statistics() { }
		public string StartTime = string.Empty;
		public string EndTime = string.Empty;
		public string ElapsedTime = string.Empty;
	}

	internal partial class StatisticsTime
	{
		public StatisticsTime(Statistics statistics)
		{
			this.Times = statistics;
		}

		private Statistics mstatistics = null;
		public const string StatisticsDateTimeFormat = @"yyyy/MM/dd hh:mm:ss";

		public Statistics Times
		{
			get
			{
				return this.mstatistics;
			}
			set
			{
				this.mstatistics = value;
			}
		}

		public DateTime StartTime
		{
			get
			{
				return DateTime.Parse(this.Times.StartTime);
			}
			set
			{
				this.Times.StartTime = value.ToString(StatisticsDateTimeFormat);
			}
		}

		public DateTime EndTime
		{
			get
			{
				return DateTime.Parse(this.Times.EndTime);
			}
			set
			{
				this.Times.EndTime = value.ToString(StatisticsDateTimeFormat);
			}
		}

		public uint ElapsedTime
		{
			get
			{
				return uint.Parse(this.Times.ElapsedTime);
			}
			set
			{
				this.Times.EndTime = value.ToString();
			}
		}
	}
	#endregion <Statistics>.
}

