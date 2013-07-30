//using System;
//using System.Collections.Generic;
//using System.Collections.Immutable;
//using MathUtils.Rand;
//using SortNetwork.KeySets;
//using SortNetwork.Sorters;
//using SortNetwork.Switchables;

//namespace SortNetwork.Results
//{
//    public interface ISwitchableResult
//    {
//        Guid Key { get; }
//        ISwitchableResult Switch(ISwitch @switch);
//        ISwitchable Switchable { get; }
//        IEnumerable<ISwitch> SwitchesUsed { get; }
//    }

//    public static class SwitchableResult
//    {
//        public static ISwitchableResult ToSwitchableResult
//        (
//            this ISwitchable switchable,
//            Guid key
//        )
//        {
//            return new SwitchableResultImpl
//                (
//                    key: key,
//                    switchable: switchable,
//                    initialSwitchable: switchable
//                );
//        }
//    }

//    class SwitchableResultImpl : ISwitchableResult
//    {
//        public SwitchableResultImpl
//            (
//                Guid key, 
//                ISwitchable switchable, 
//                ISwitchable initialSwitchable
//            )
//        {
//            _key = key;
//            _switchable = switchable;
//            _initialSwitchable = initialSwitchable;
//        }

//        private readonly ISwitchable _initialSwitchable;
//        public ISwitchable InitialSwitchable
//        {
//            get { return _initialSwitchable; }
//        }

//        private readonly ISwitchable _switchable;
//        public ISwitchable Switchable
//        {
//            get { return _switchable; }
//        }

//        public bool IsSorted { get { return Switchable.IsSorted; } }

//        public IEnumerable<ISwitchable> CreateMutatedCopiesOf(IRando randomGen, int copyNumber)
//        {
//            throw new NotImplementedException();
//        }

//        public ISwitchable Switch(IKeyPair keyPair)
//        {
//            return Switchable.Switch(keyPair);
//        }

//        public int KeyCount { get { return Switchable.KeyCount; } }

//        public SwitchableType SwitchableType { get { return Switchable.SwitchableType; } }

//        private readonly Guid _key;
//        public Guid Key
//        {
//            get { return _key; }
//        }

//        public ISwitchableResult Switch(ISwitch @switch)
//        {
//            var curSwitchable = Switchable.Switch(@switch.KeyPair);
//            if (Equals(curSwitchable, Switchable))
//            {
//                return this;
//            }

//            var retVal = new SwitchableResultImpl
//                (
//                    key:  Key,
//                    switchable: curSwitchable,
//                    initialSwitchable: InitialSwitchable
//                );

//            retVal._switchesUsed = retVal._switchesUsed.Push(@switch);
//            return retVal;
//        }

//        private ImmutableStack<ISwitch> _switchesUsed = ImmutableStack<ISwitch>.Empty;
//        public IEnumerable<ISwitch> SwitchesUsed { get { return _switchesUsed; } } 
//    }
//}
