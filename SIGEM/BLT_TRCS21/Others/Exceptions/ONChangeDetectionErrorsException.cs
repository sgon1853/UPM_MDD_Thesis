// 3.4.4.5
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using SIGEM.Business.Types;

namespace SIGEM.Business.Exceptions
{
	public class ONChangeDetectionErrorsException : ONException
	{
		public HybridDictionary stateDifferences = new HybridDictionary(true);

		public ONChangeDetectionErrorsException(Exception innerException, Dictionary<String, ONChangeDetectionInfo> differences)
			: base(innerException, 48)
		{
			mMessage = ONErrorText.ChangeDetectionFailure;
			foreach (KeyValuePair<string, ONChangeDetectionInfo> lChangeDetection in differences)
			{
				ONChangeDetectionInfo lChange = lChangeDetection.Value;
				stateDifferences.Add(lChange.Key, lChange);
			}
		}
	}
}

