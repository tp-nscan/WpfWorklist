using System;

namespace Ecosystem
{
    public interface IOrganisim
    {
        Guid Guid { get; }
        double ReproductionRate { get; }
    }
}
