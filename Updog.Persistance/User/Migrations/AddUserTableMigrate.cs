using FluentMigrator;
using Updog.Domain;

namespace Updog.Persistance {
    [Migration(2019_10_16_0, "Add user table.")]
    public class AddUserTableMigrate : Migration {
        public override void Up() {
            Create.Table("\"user\"")
                .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("username").AsString(24).NotNullable().Unique()
                .WithColumn("email").AsString(64).Unique()
                .WithColumn("password_hash").AsString(60).NotNullable()
                .WithColumn("joined_date").AsDateTime().NotNullable()
                .WithColumn("post_karma").AsInt32().NotNullable().WithDefaultValue(0)
                .WithColumn("comment_karma").AsInt32().NotNullable().WithDefaultValue(0);
        }

        public override void Down() {
            Delete.Table("\"user\"");
        }
    }
}


