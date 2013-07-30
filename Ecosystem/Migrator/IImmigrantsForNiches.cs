using System;

namespace Ecosystem.Migrator
{
    public interface IImmigrantsForNiches
    {
        INicheImmigrants NicheImmigrantsByNicheId(Guid guid);
    }
}
