using System;
using System.Collections.Generic;
using System.IO;
using Utils.FileUtils.FileParts;

namespace Utils.FileUtils.FileIO
{
    public abstract class TextFileLoader<TR> : IDisposable where TR : IFileRecordBase
    {
        protected TextFileLoader(RecordFileBase<TR> recordFileBase)
        {
            _recordFileBase = recordFileBase;
        }

        private readonly RecordFileBase<TR> _recordFileBase;
        protected RecordFileBase<TR> RecordFileBase
        {
            get { return _recordFileBase; }
        }

        public abstract IEnumerable<TR> ReadFile { get; }

        private StreamReader _streamReader;
        protected StreamReader StreamReader
        {
            get
            {
                if (_streamReader==null)
                {
                    try
                    {
                        _streamReader = new StreamReader(RecordFileBase.FileNameAndPath);
                    }
                    catch (Exception ex)
                    {
                        RecordFileBase.MessageLog.AddWithName(
                            String.Format("Error opening file{0}: {1}", RecordFileBase.FileNameAndPath, ex.Message));
                        RecordFileBase.FileReadStatus = FileReadStatus.ReadWithErrors;

                        if (_streamReader != null) _streamReader.Dispose();
                        return null;
                    }

                }
                return _streamReader;
            }
        }

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            _streamReader.Dispose();
            _streamReader = null;
        }

        #endregion
    }
}
