using FluentMigrator;
using Updog.Domain;

namespace Updog.Persistance {
    [Migration(2019_10_16_1, "Add space table.")]
    public class AddSpaceTableMigrate : Migration {
        public override void Up() {
            Create.Table("Space")
                .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("UserId").AsInt32().NotNullable()
                .WithColumn("Name").AsString(24).NotNullable().Unique()
                .WithColumn("CreationDate").AsDateTime().NotNullable()
                .WithColumn("SubscriptionCount").AsInt32().NotNullable().WithDefaultValue(0)
                .WithColumn("IsDefault").AsBoolean().WithDefaultValue(false)
                .WithColumn("Description").AsString(512);

            Create.ForeignKey().FromTable("Space").ForeignColumn("UserId").ToTable("\"User\"").PrimaryColumn("Id");
        }

        public override void Down() {
            Delete.Table("Space");
        }
    }
}
