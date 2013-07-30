using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Utils.FileUtils.Log;

namespace Utils.FileUtils.FileParts
{
    public abstract class RecordFileBase<TR> where TR : IFileRecordBase
    {
        protected RecordFileBase()
        {
            FileNameAndPath = string.Empty;
        }

        protected RecordFileBase(string fileNameAndPath)
        {
            FileNameAndPath = fileNameAndPath;
        }

        public FileReadStatus FileReadStatus { get; set; }

        private readonly MessageLog _messageLog = new MessageLog("File of " + typeof(TR));
        public MessageLog MessageLog
        {
            get { return _messageLog; }
        }

        public string FileNameAndPath { get; set; }

        public bool FileExists
        {
            get { return System.IO.File.Exists(FileNameAndPath); }
        }

        public string FileName
        {
            get {
                try { return System.IO.Path.GetFileName(FileNameAndPath); }
                catch { return string.Empty; }
            }
        }

        public string FilePath
        {
            get {
                try { return System.IO.Path.GetDirectoryName(FileNameAndPath); }
                catch { return string.Empty; }
            }
        }

        protected readonly Subject<TR> _onRecordAdded = new Subject<TR>();
        public IObservable<TR> OnRecordAdded { get { return _onRecordAdded.AsObservable(); } }

        public virtual void AddRecord(TR record)
        {
            _records.Add(record.Key, record);
            _onRecordAdded.OnNext(record);
        }

        public void AddRecords(IEnumerable<TR> records)
        {
            foreach (var record in records)
            {
                AddRecord(record);
            }
        }

        public TR GetRecord(string key)
        {
            if(_records.ContainsKey(key))
            {
                return _records[key];
            }
            return default(TR);
        }

        readonly Dictionary<string,TR> _records = new Dictionary<string,TR>();
        public IEnumerable<TR> Records { get { return _records.Values; } }

        public abstract void WriteFile();
    }
}
