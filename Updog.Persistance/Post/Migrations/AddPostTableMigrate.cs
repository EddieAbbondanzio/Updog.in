using FluentMigrator;
using Updog.Domain;

namespace Updog.Persistance {
    [Migration(2019_10_16_2, "Add user table.")]
    public class AddPostTableMigrate : Migration {
        public override void Up() {
            Create.Table("post")
                .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("user_id").AsInt32().NotNullable()
                .WithColumn("space_id").AsInt32().NotNullable()
                .WithColumn("type").AsInt16().NotNullable()
                .WithColumn("title").AsString(300).NotNullable()
                .WithColumn("body").AsString(10_000).NotNullable()
                .WithColumn("creation_date").AsDateTime().NotNullable()
                .WithColumn("was_updated").AsBoolean().WithDefaultValue(false)
                .WithColumn("was_deleted").AsBoolean().WithDefaultValue(false)
                .WithColumn("upvotes").AsInt32().NotNullable().WithDefaultValue(0)
                .WithColumn("downvotes").AsInt32().NotNullable().WithDefaultValue(0);

            Create.ForeignKey().FromTable("post").ForeignColumn("user_id").ToTable("\"user\"").PrimaryColumn("id");
            Create.ForeignKey().FromTable("post").ForeignColumn("space_id").ToTable("space").PrimaryColumn("id");
        }

        public override void Down() {
            Delete.Table("post");
        }
    }
}