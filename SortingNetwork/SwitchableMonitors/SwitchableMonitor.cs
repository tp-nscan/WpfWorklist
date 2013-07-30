using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using MathUtils.Rand;
using SortingNetwork.KeyPair;
using SortingNetwork.Switchables;
using SortingNetwork.Switches;

namespace SortingNetwork.SwitchableMonitors
{
    public interface ISwitchableMonitor : ISwitchable
    {
        Guid Key { get; }
        ISwitchableMonitor Switch(ISwitch @switch);
        ISwitchable Switchable { get; }
        IEnumerable<ISwitch> SwitchesUsed { get; }
    }

    public static class SwitchableMonitor
    {
        public static ISwitchableMonitor ToSwitchableMonitor
        (
            this ISwitchable switchable,
            Guid key
        )
        {
            return new SwitchableMonitorImpl
                (
                    key: key,
                    switchable: switchable,
                    initialSwitchable: switchable
                );
        }
    }

    class SwitchableMonitorImpl : ISwitchableMonitor
    {
        public SwitchableMonitorImpl
            (
                Guid key, 
                ISwitchable switchable, 
                ISwitchable initialSwitchable
            )
        {
            _key = key;
            _switchable = switchable;
            _initialSwitchable = initialSwitchable;
        }

        private readonly ISwitchable _initialSwitchable;
        public ISwitchable InitialSwitchable
        {
            get { return _initialSwitchable; }
        }

        private readonly ISwitchable _switchable;
        public ISwitchable Switchable
        {
            get { return _switchable; }
        }

        public bool IsSorted { get { return Switchable.IsSorted; } }

        public IEnumerable<ISwitchable> CreateMutatedCopiesOf(IRando randomGen, int copyNumber)
        {
            throw new NotImplementedException();
        }

        public ISwitchable Switch(IKeyPair keyPair)
        {
            return Switchable.Switch(keyPair);
        }

        public int KeyCount { get { return Switchable.KeyCount; } }

        public SwitchableType SwitchableType { get { return Switchable.SwitchableType; } }

        private readonly Guid _key;
        public Guid Key
        {
            get { return _key; }
        }

        public ISwitchableMonitor Switch(ISwitch @switch)
        {
            var curSwitchable = Switchable.Switch(@switch.KeyPair);
            if (Equals(curSwitchable, Switchable))
            {
                return this;
            }

            var retVal = new SwitchableMonitorImpl
                (
                    key:  Key, 
                    switchable:  Switchable,
                    initialSwitchable: InitialSwitchable
                );

            retVal._switchesUsed = retVal._switchesUsed.Push(@switch);
            return retVal;
        }

        private ImmutableStack<ISwitch> _switchesUsed = ImmutableStack<ISwitch>.Empty;
        public IEnumerable<ISwitch> SwitchesUsed { get { return _switchesUsed; } } 
    }
}
