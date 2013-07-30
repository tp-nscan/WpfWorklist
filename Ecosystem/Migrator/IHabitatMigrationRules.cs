using Ecosystem.Niche;

namespace Ecosystem.Migrator
{
    public interface IHabitatMigrationRules
    {
        INicheMigrationRule NicheMigrationRule(INiche niche);
    }
}
