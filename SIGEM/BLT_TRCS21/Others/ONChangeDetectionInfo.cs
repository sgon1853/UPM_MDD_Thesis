// 3.4.4.5
using System;
using System.Collections.Generic;
using System.Text;
using SIGEM.Business.Types;

namespace SIGEM.Business
{
    public class ONChangeDetectionInfo
    {
        private string mKey;
        private DataTypeEnumerator mType;
        private string mClassName;
        private IONType mOldValue;
        private IONType mNewValue;

        #region Properties
        public IONType OldValue
        {
            get
            {
                return mOldValue;
            }
            set
            {
                mOldValue = value;
            }
        }

        public IONType NewValue
        {
            get
            {
                return mNewValue;
            }
            set
            {
                mNewValue = value;
            }
        }

        public DataTypeEnumerator Type
        {
            get
            {
                return mType;
            }
        }

        public string Key
        {
            get
            {
                return mKey;
            }
        }

        public string ClassName
        {
            get
            {
                return mClassName;
            }
        }

        #endregion Properties

        #region Constructor
        public ONChangeDetectionInfo(string key, DataTypeEnumerator type, string className)
        {
            mKey = key;
            mType = type;
            mClassName = className;
        }
        #endregion

        #region Operations
        public ONBool CheckValues()
        {
            if (!OldValue.Equals(NewValue))
                return new ONBool(false);
            else
                return new ONBool(true);
        }
        #endregion
    }
}
