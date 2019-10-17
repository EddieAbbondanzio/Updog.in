using FluentMigrator;
using Updog.Domain;

namespace Updog.Persistance {
    [Migration(2019_10_16_4, "Add vote table.")]
    public class AddVoteTableMigrate : Migration {
        public override void Up() {
            Create.Table("Vote")
                .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("UserId").AsInt32().NotNullable()
                .WithColumn("ResourceId").AsInt32().NotNullable()
                .WithColumn("ResourceType").AsInt16().NotNullable()
                .WithColumn("Direction").AsInt16().NotNullable();

            Create.ForeignKey().FromTable("Vote").ForeignColumn("UserId").ToTable("\"User\"").PrimaryColumn("Id");
        }

        public override void Down() {
            Delete.Table("Vote");
        }
    }
}
