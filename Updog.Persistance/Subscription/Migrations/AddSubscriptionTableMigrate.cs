using FluentMigrator;
using Updog.Domain;

namespace Updog.Persistance {
    [Migration(2019_10_16_6, "Add vote table.")]
    public class AddSubscriptionTableMigrate : Migration {
        public override void Up() {
            Create.Table("subscription")
                .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("user_id").AsInt32().NotNullable()
                .WithColumn("space_id").AsInt32().NotNullable();

            Create.ForeignKey().FromTable("subscription").ForeignColumn("user_id").ToTable("\"user\"").PrimaryColumn("id");
            Create.ForeignKey().FromTable("subscription").ForeignColumn("space_id").ToTable("space").PrimaryColumn("id");
        }

        public override void Down() {
            Delete.Table("subscription");
        }
    }
}
