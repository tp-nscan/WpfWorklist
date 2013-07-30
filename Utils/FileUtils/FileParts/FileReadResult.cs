namespace Utils.FileUtils.FileParts
{
    public class FileReadResult<T>
    {
        public FileReadResult(T result, bool hasError, string message)
        {
            _hasError = hasError;
            _message = message;
            _result = result;
        }

        private readonly bool _hasError;
        public bool HasError
        {
            get { return _hasError; }
        }

        private readonly string _message;
        public string Message
        {
            get { return _message; }
        }

        private readonly T _result;
        public T Result
        {
            get { return _result; }
        }
    }
}
