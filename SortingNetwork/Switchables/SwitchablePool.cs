using System;
using System.Collections.Generic;
using System.Linq;

namespace SortingNetwork.Switchables
{
    public static class SwitchablePool
    {
        public static ISwitchablePool ToSwitchablePool(this IEnumerable<ISwitchable> switchables, Guid guid, string comment)
        {
            return new SwitchablePoolImpl(guid, switchables, comment);
        }
    }

    public class SwitchablePoolImpl : ISwitchablePool
    {
        public SwitchablePoolImpl(Guid guid, IEnumerable<ISwitchable> switchables, string comment)
        {
            _guid = guid;
            _comment = comment;
            _switchables = (switchables==null) ? new List<ISwitchable>() : switchables.ToList();
        }

        private readonly List<ISwitchable> _switchables;
        public IEnumerable<ISwitchable> Switchables
        {
            get { return _switchables; }
        }

        private readonly Guid _guid;
        public Guid Guid
        {
            get { return _guid; }
        }

        private readonly string _comment;
        public string Comment
        {
            get { return _comment; }
        }
    }
}
