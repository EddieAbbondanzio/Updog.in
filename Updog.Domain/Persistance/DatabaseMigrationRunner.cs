namespace Updog.Domain {
    public abstract class DatabaseMigrationRunner {
        public abstract void MigrateUp();
        public abstract void MigrateDown(long version);
    }
}