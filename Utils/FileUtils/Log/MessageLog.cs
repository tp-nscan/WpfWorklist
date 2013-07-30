using System;
using System.Collections.Generic;

namespace Utils.FileUtils.Log
{
    public class MessageLog
    {
        public event EventHandler MessagesChanged;

        public void OnMessagesChanged(EventArgs e)
        {
            EventHandler handler = MessagesChanged;
            if (handler != null) handler(this, e);
        }

        public MessageLog(string name)
        {
            _name = name;
        }

        private readonly string _name;
        public string Name
        {
            get { return _name; }
        }
        readonly List<string> _messages = new List<string>();
        public void AddWithName(string msg)
        {
            if (string.IsNullOrEmpty(Name))
            {
                _messages.Add(msg);
                return;
            }
            _messages.Add(Name + ": " + msg);
            OnMessagesChanged(EventArgs.Empty);
        }

        public void AddMessages(IEnumerable<string> msgs)
        {
            _messages.AddRange(msgs);
        }

        public void ClearMessages()
        {
            _messages.Clear();
        }

        public bool HasMessages
        {
            get { return _messages.Count > 0; }
        }

        public IEnumerable<string> Messages
        {
            get { return _messages; }
        }
    }
}
