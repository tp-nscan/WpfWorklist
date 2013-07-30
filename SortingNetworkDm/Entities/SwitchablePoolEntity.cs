using System;
using DynamicModel.Model;
using SortNetwork.Switchables;

namespace SortingNetworkDm.Entities
{
    public interface ISwitchablePoolEntity : IEntity
    {
        ISwitchableRepo SwitchableRepo { get; }
    }

    public static class SwitchablePoolEntity
    {
        public const string TypeName = "SwitchablePoolEntity";

        public static ISwitchablePoolEntity Make
        (
            Guid guid,
            string name,
            string description,
            ISwitchableRepo switchableRepo
        )
        {
            return new SwitchablePoolEntityImpl(guid, name, description, switchableRepo);
        }
    }

    class SwitchablePoolEntityImpl : EntityImpl, ISwitchablePoolEntity
    {
        public SwitchablePoolEntityImpl(Guid guid, string name, string description, ISwitchableRepo switchableRepo) 
            : base(guid, name, description)
        {
            _switchableRepo = switchableRepo;
        }

        public override bool IsLoaded
        {
            get { return SwitchableRepo != null; }
        }

        public override string TypeName { get { return SwitchablePoolEntity.TypeName; } }

        private readonly ISwitchableRepo _switchableRepo;
        public ISwitchableRepo SwitchableRepo
        {
            get { return _switchableRepo; }
        }
    }
}
