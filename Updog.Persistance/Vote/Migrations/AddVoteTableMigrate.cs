using FluentMigrator;
using Updog.Domain;

namespace Updog.Persistance {
    [Migration(2019_10_16_4, "Add vote table.")]
    public class AddVoteTableMigrate : Migration {
        public override void Up() {
            Create.Table("vote")
                .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("user_id").AsInt32().NotNullable()
                .WithColumn("resource_id").AsInt32().NotNullable()
                .WithColumn("resource_type").AsInt16().NotNullable()
                .WithColumn("direction").AsInt16().NotNullable();

            Create.ForeignKey().FromTable("vote").ForeignColumn("user_id").ToTable("\"user\"").PrimaryColumn("id");
        }

        public override void Down() {
            Delete.Table("vote");
        }
    }
}
