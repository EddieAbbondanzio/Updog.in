using FluentMigrator;
using Updog.Domain;

namespace Updog.Persistance {
    [Migration(2019_10_16_5, "Add vote table.")]
    public class AddRoleTableMigrate : Migration {
        public override void Up() {
            Create.Table("Role")
                .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("UserId").AsInt32().NotNullable()
                .WithColumn("RoleType").AsByte().NotNullable()
                .WithColumn("Domain").AsString(24).NotNullable();

            Create.ForeignKey().FromTable("Role").ForeignColumn("UserId").ToTable("\"User\"").PrimaryColumn("Id");
        }

        public override void Down() {
            Delete.Table("Role");
        }
    }
}
