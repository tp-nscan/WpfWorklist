using Utils.FileUtils.Log;

namespace Utils.FileUtils.FileParts
{
    public interface IFileRecordBase
    {
        string FileType { get; }
        string Key { get; }
        void LoadFromTextRecord(string textRecord, object helper);
        MessageLog MessageLog { get; }
        string ToRecord();
    }
}