using System;
using DynamicModel.Model;
using WpfUtils;
using WpfUtils.SelectableCollection;

namespace DynamicModel.ViewModel
{
    public interface IEntityVm : ISelectableVm
    {
        string Description { get; set; }
        Guid Guid { get; }
        string Name { get; set; }
        string TypeName { get; }
    }

    public static class EntityVm
    {
        public static IEntityVm MakeTest(string name)
        {
            return new TestEntityVmImpl(name);
        }
    }

    public abstract class EntityVmImpl : ViewModelBase, IEntityVm
    {
        protected EntityVmImpl(IEntity entity)
        {
            _entity = entity;
        }

        private IEntity _entity;
        public IEntity Entity
        {
            get { return _entity; }
            protected set
            {
                _entity = value;
                _entity.OnEntityChanged.Subscribe
                    (
                        p => { 
                                OnPropertyChanged("Name");
                                OnPropertyChanged("Description");
                             }
                   );
            }
        }

        public string Description
        {
            get { return Entity.Description; }
            set { Entity.Description = value; }
        }

        public Guid Guid
        {
            get { return Entity.Guid; }
        }

        public string Name
        {
            get { return Entity.Name; }
            set { Entity.Name = value; }
        }

        public abstract string TypeName { get; }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }
    }

    class TestEntityVmImpl : ViewModelBase, IEntityVm
    {
        private readonly Guid _guid = Guid.NewGuid();
        private string _description = string.Empty;

        public TestEntityVmImpl(string name)
        {
            Name = name;
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public Guid Guid
        {
            get { return _guid; }
        }

        public string Name { get; set; }

        public string TypeName
        {
            get { return "TestEntityVmImpl"; }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }
    }
}