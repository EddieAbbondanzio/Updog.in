using FluentMigrator;
using Updog.Domain;

namespace Updog.Persistance {
    [Migration(2019_10_16_1, "Add space table.")]
    public class AddSpaceTableMigrate : Migration {
        public override void Up() {
            Create.Table("space")
                .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("user_id").AsInt32().NotNullable()
                .WithColumn("name").AsString(24).NotNullable().Unique()
                .WithColumn("creation_date").AsDateTime().NotNullable()
                .WithColumn("subscription_count").AsInt32().NotNullable().WithDefaultValue(0)
                .WithColumn("is_default").AsBoolean().WithDefaultValue(false)
                .WithColumn("description").AsString(512);

            Create.ForeignKey().FromTable("space").ForeignColumn("user_id").ToTable("\"user\"").PrimaryColumn("id");
        }

        public override void Down() {
            Delete.Table("space");
        }
    }
}
