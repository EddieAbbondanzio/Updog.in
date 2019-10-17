using FluentMigrator;
using Updog.Domain;

namespace Updog.Persistance {
    [Migration(2019_10_16_6, "Add vote table.")]
    public class AddSubscriptionTableMigrate : Migration {
        public override void Up() {
            Create.Table("Subscription")
                .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("UserId").AsInt32().NotNullable()
                .WithColumn("SpaceId").AsInt32().NotNullable();

            Create.ForeignKey().FromTable("Subscription").ForeignColumn("UserId").ToTable("\"User\"").PrimaryColumn("Id");
            Create.ForeignKey().FromTable("Subscription").ForeignColumn("SpaceId").ToTable("Space").PrimaryColumn("Id");
        }

        public override void Down() {
            Delete.Table("Subscription");
        }
    }
}
