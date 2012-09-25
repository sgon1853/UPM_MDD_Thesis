// 3.4.4.5

using System;
using System.Collections;

namespace SIGEM.Business.Exceptions
{
    public class ONSystemException : ApplicationException
    {
        #region Members
        public ArrayList mTraceInformation = new ArrayList();
        #endregion Members

        #region Constructors
        public ONSystemException(System.Exception innerException, string traceItem)
            : base(innerException.Message, innerException)
        {
            mTraceInformation.Add(traceItem);
        }
        #endregion Constructors

        public void addTraceInformation(string traceItem)
        {
            mTraceInformation.Add(traceItem);
        }
    }
}
