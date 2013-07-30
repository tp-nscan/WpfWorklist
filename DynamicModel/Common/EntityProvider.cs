using System.Collections.Generic;
using DynamicModel.Model;

namespace DynamicModel.Common
{
    public interface IEntityProvider
    {
        IEnumerable<IEntity> Entities { get; }
        void AddEntity(IEntity entity);
    }

    public static class EntityProvider
    {
        public static IEntityProvider Make()
        {
            return new TestEntityProvider();
        }
    }

    class TestEntityProvider : IEntityProvider
    {
        public void AddEntity(IEntity entity)
        {
            _entities.Add(entity);
        }

        private readonly List<IEntity> _entities = new List<IEntity>();
        public IEnumerable<IEntity> Entities
        {
            get { return _entities; }
        }
    }
}
