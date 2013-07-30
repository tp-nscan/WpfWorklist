using System;
using System.Reactive.Subjects;

namespace DynamicModel.Model
{
    public interface IEntity
    {
        string Description { get; set; }
        Guid Guid { get; }
        bool IsLoaded { get; }
        string Name { get; set; }
        IObservable<IEntity> OnEntityChanged { get; }
        string TypeName { get; }
    }

    public static class Entity
    {
        public static IEntity MakeTest(string name)
        {
            return new EntityTestImpl
                (
                    name: name
                );
        }
    }

    public abstract class EntityImpl : IEntity
    {
        protected EntityImpl(Guid guid, string name, string description)
        {
            _guid = guid;
            _name = name;
            _description = description;
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set 
            { 
                _description = value;
                _entityChanged.OnNext(this);
            }
        }

        private readonly Guid _guid;
        public Guid Guid
        {
            get { return _guid; }
        }

        public abstract bool IsLoaded { get; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                _entityChanged.OnNext(this);
            }
        }

        private readonly Subject<IEntity> _entityChanged = new Subject<IEntity>();
        public IObservable<IEntity> OnEntityChanged
        {
            get { return _entityChanged; }
        }

        public abstract string TypeName { get; }
    }

    class EntityTestImpl : EntityImpl
    {
        private readonly Guid _guid;
        private readonly string _name;

        public EntityTestImpl(string name) : base(Guid.NewGuid(), name, string.Empty)
        {
            _name = name;
            _guid = Guid.NewGuid();
        }

        public override bool IsLoaded
        {
            get { return false; }
        }

        public override string TypeName
        {
            get { return "Test"; }
        }
    }
}
