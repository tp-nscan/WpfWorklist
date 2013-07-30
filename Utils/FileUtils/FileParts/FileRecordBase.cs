using Utils.FileUtils.Log;

namespace Utils.FileUtils.FileParts
{
    public abstract class FileRecordBase : IFileRecordBase
    {
        protected FileRecordBase(string fileType)
        {
            _fileType = fileType;
            _messageLog = new MessageLog(FileType);
        }

        private readonly string _fileType;
        public string FileType
        {
            get { return _fileType; }
        }

        public abstract string Key { get; }

        public abstract void LoadFromTextRecord(string textRecord, object helper);

        private readonly MessageLog _messageLog;
        public MessageLog MessageLog
        {
            get { return _messageLog; }
        }

        public abstract string ToRecord();
    }
}
