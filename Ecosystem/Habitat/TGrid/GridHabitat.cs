using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Ecosystem.Migrator;
using Ecosystem.Niche;
using MathUtils.Collections;

namespace Ecosystem.Habitat.TGrid
{
    public class GridHabitat : ReadOnlyDictionary<Guid, IGridNiche>, IGridHabitat
    {
        public GridHabitat(int gridSize, IEnumerable<IGridNiche> nicheRepo, IHabitatMigrationRules habitatMigrationRules)
            : base(nicheRepo.ToDictionary(n=>n.Guid))
        {
            _gridSize = gridSize;
            _habitatMigrationRules = habitatMigrationRules;
        }

        public GridHabitat(int gridSize, IReadOnlyCollection<IOrganisim> organisims, IReadOnlyCollection<IOrganisim> migrants,
            int organisimCount, int migrantCount,
            Func<GridHabitat, IHabitatMigrationRules> migrationRulesMaker)
            : base(Doink())
        {
            //_gridSize = gridSize;

            //var organisimsPcs1 = organisims.Split(GridSize).ToList();
            //var migrantsPcs1 = migrants.Split(GridSize).ToList();

            //for (var x = 0; x < GridSize; x++)
            //{
            //    var organisimsPcs2 = organisimsPcs1[x].Split(GridSize).ToList();
            //    var migrantsPcs2 = migrantsPcs1[x].Split(GridSize).ToList();
            //    for (var y = 0; y < GridSize; y++)
            //    {
            //        var newNiche = new GridNiche
            //            (
            //                Guid.NewGuid(), new TorusPoint(x, y, gridSize, gridSize),
            //                organisimsPcs2[y].Items,
            //                migrantsPcs2[y].Items,
            //                organisimCount,
            //                migrantCount
            //            );

            //        _gridNichesByLocation.Add(newNiche.Location, newNiche);
            //        _gridNichesById.Add(newNiche.Guid, newNiche);
            //    }
            //}

            //_habitatMigrationRules = migrationRulesMaker(this);
        }

        public static IDictionary<Guid, IGridNiche> Doink()
        {
            return new Dictionary<Guid, IGridNiche>();
        }

        private readonly int _gridSize;
        public int GridSize
        {
            get { return _gridSize; }
        }
        private readonly Dictionary<Guid, IGridNiche> _gridNichesById = new Dictionary<Guid, IGridNiche>();
        private readonly Dictionary<TorusPoint, IGridNiche> _gridNichesByLocation = new Dictionary<TorusPoint, IGridNiche>();

        private readonly IHabitatMigrationRules _habitatMigrationRules;
        public IHabitatMigrationRules HabitatMigrationRules
        {
            get { return _habitatMigrationRules; }
        }

        public INiche NicheAtLocation(TorusPoint location)
        {
            return _gridNichesByLocation.ContainsKey(location) ? _gridNichesByLocation[location] : null;
        }

        IEnumerable<IGridNiche> IGridHabitat.Niches { get { return _gridNichesByLocation.Values; } }


        public IEnumerable<INiche> Items { get { return _gridNichesByLocation.Values; } }

        public int Size
        {
            get { return _gridNichesById.Count; }
        }

        public INiche this[int index]
        {
            get { throw new NotImplementedException(); }
        }

        public IReadOnlyCollection<INiche> SubRepo(int start, int count)
        {
            throw new NotImplementedException();
        }

        public INiche GetValue(Guid key)
        {
            return _gridNichesById.ContainsKey(key) ? _gridNichesById[key] : null;
        }

        public IEnumerator<KeyValuePair<Guid, INiche>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(Guid key, out INiche value)
        {
            throw new NotImplementedException();
        }

        INiche IReadOnlyDictionary<Guid, INiche>.this[Guid key]
        {
            get { throw new NotImplementedException(); }
        }

        private IEnumerable<Guid> _keys;
        private IEnumerable<INiche> _values;
        public IEnumerable<Guid> Keys
        {
            get { return _keys; }
        }

        public IEnumerable<INiche> Values
        {
            get { return _values; }
        }

        //public new IEnumerable<Guid> Keys
        //{
        //    get { return Keys; }
        //}

        //IEnumerable<INiche> Values
        //{
        //    get { return Values; }
        //}
    }
}
