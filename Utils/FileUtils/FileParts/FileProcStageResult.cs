using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils.FileUtils.FileParts
{
    public class FileProcStageResult<T>
    {
        public FileProcStageResult(int batchCount, int index, T stageResult, FileReadStatus fileReadStatus)
        {
            _batchCount = batchCount;
            _index = index;
            _stageResult = stageResult;
            _fileReadStatus = fileReadStatus;
        }

        private readonly int _batchCount;
        public int BatchCount
        {
            get { return _batchCount; }
        }

        private readonly FileReadStatus _fileReadStatus;
        public FileReadStatus FileReadStatus
        {
            get { return _fileReadStatus; }
        }

        private readonly int _index;
        public int Index
        {
            get { return _index; }
        }

        private readonly T _stageResult;
        public T StageResult
        {
            get { return _stageResult; }
        }
    }
}
