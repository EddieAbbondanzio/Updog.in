using FluentMigrator;
using Updog.Domain;

namespace Updog.Persistance {
    [Migration(2019_10_20_0, "Add user login table.")]
    public class AddUserLoginTableMigrate : Migration {
        public override void Up() {
            Create.Table("user-login")
                .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("user_id").AsInt32().NotNullable();

            Create.ForeignKey().FromTable("user-login").ForeignColumn("user_id").ToTable("\"user\"").PrimaryColumn("id");
        }

        public override void Down() {
            Delete.Table("user-login");
        }
    }
}


