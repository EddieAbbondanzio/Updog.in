using FluentMigrator;
using Updog.Domain;

namespace Updog.Persistance {
    [Migration(2019_10_16_0, "Add user table.")]
    public class AddUserTableMigrate : Migration {
        public override void Up() {
            Create.Table("\"User\"")
                .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("Username").AsString(24).NotNullable().Unique()
                .WithColumn("Email").AsString(64).Unique()
                .WithColumn("PasswordHash").AsString(60).NotNullable()
                .WithColumn("JoinedDate").AsDateTime().NotNullable()
                .WithColumn("PostKarma").AsInt32().NotNullable().WithDefaultValue(0)
                .WithColumn("CommentKarma").AsInt32().NotNullable().WithDefaultValue(0);
        }

        public override void Down() {
            Delete.Table("\"User\"");
        }
    }
}


