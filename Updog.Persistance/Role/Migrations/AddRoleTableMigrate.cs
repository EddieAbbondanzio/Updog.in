using FluentMigrator;
using Updog.Domain;

namespace Updog.Persistance {
    [Migration(2019_10_16_5, "Add vote table.")]
    public class AddRoleTableMigrate : Migration {
        public override void Up() {
            Create.Table("role")
                .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("user_id").AsInt32().NotNullable()
                .WithColumn("role_type").AsByte().NotNullable()
                .WithColumn("domain").AsString(24).NotNullable();

            Create.ForeignKey().FromTable("role").ForeignColumn("user_id").ToTable("\"user\"").PrimaryColumn("id");
        }

        public override void Down() {
            Delete.Table("role");
        }
    }
}
