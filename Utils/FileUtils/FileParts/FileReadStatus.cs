namespace Utils.FileUtils.FileParts
{
    public enum FileReadStatus
    {
        BeingRead,
        NotFound,
        NotRead,
        Read,
        ReadWithErrors
    }

    public static class FileReadStatusExt
    {
        public static bool IsOk(this FileReadStatus inputFileStatus)
        {
            switch (inputFileStatus)
            {
                case FileReadStatus.BeingRead:
                case FileReadStatus.NotRead:
                case FileReadStatus.Read:
                    return true;
                case FileReadStatus.ReadWithErrors:
                case FileReadStatus.NotFound:
                    return false;
                default:
                    return false;
            }
        }
    }
}
