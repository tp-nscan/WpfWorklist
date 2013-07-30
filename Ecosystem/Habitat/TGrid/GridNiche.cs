using System;
using System.Collections.Generic;
using MathUtils.Collections;

namespace Ecosystem.Habitat.TGrid
{
    public class GridNiche : IGridNiche
    {
        private readonly Guid _guid;

        public GridNiche(Guid guid, TorusPoint location, IEnumerable<IOrganisim> organisims, IEnumerable<IOrganisim> migrants, int organisimCount, int migrantCount)
        {
            _guid = guid;
            _location = location;
            _organisimCount = organisimCount;
            _migrantCount = migrantCount;
            _organisims.AddRange(organisims);
            _migrants.AddRange(migrants);
        }

        public Guid Guid
        {
            get { return _guid; }
        }

        private readonly List<IOrganisim> _organisims = new List<IOrganisim>();
        public IEnumerable<IOrganisim> Organisims { get { return _organisims; } }

        private readonly List<IOrganisim> _migrants = new List<IOrganisim>();
        
        public IEnumerable<IOrganisim> Migrants
        {
            get { return _migrants; }
        }

        private readonly int _organisimCount;
        public int OrganisimCount
        {
            get { return _organisimCount; }
        }

        private readonly int _migrantCount;
        public int MigrantCount
        {
            get { return _migrantCount; }
        }

        readonly TorusPoint _location;
        public TorusPoint Location
        {
            get { return _location; }
        }
    }
}
