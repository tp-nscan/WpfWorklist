using System;
using System.Collections.Generic;
using Utils.FileUtils.FileParts;

namespace Utils.FileUtils.FileIO
{
    public abstract class ExcelFileLoader<TR> : IDisposable where TR : FileRecordBase
    {
        protected ExcelFileLoader(RecordFileBase<TR> recordFile)
        {
            _recordFile = recordFile;
        }

        private readonly RecordFileBase<TR> _recordFile;
        protected RecordFileBase<TR> RecordFile
        {
            get { return _recordFile; }
        }

        public abstract IEnumerable<TR> ReadFile { get; }


        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            //_streamReader.Dispose();
            //_streamReader = null;
        }

        #endregion
    }
}
