using FluentMigrator.Runner;
using Updog.Domain;

namespace Updog.Persistance {
    public sealed class FluentMigratorMigrationRunner : DatabaseMigrationRunner {
        #region Fields
        private IMigrationRunner runner;
        #endregion

        #region Constructor(s)
        public FluentMigratorMigrationRunner(IMigrationRunner runner) {
            this.runner = runner;
        }
        #endregion

        #region Publics
        public override void MigrateUp() => runner.MigrateUp();

        public override void MigrateDown(long version) => runner.MigrateDown(version);
        #endregion
    }
}