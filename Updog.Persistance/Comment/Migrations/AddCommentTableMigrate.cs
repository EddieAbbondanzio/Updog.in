using FluentMigrator;
using Updog.Domain;

namespace Updog.Persistance {
    [Migration(2019_10_16_3, "Add comment table.")]
    public class AddCommentTableMigrate : Migration {
        public override void Up() {
            Create.Table("comment")
                .WithColumn("id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("user_id").AsInt32().NotNullable()
                .WithColumn("post_id").AsInt32().NotNullable()
                .WithColumn("parent_id").AsInt32()
                .WithColumn("body").AsString(10_000).NotNullable()
                .WithColumn("creation_date").AsDateTime().NotNullable()
                .WithColumn("was_updated").AsBoolean()
                .WithColumn("was_deleted").AsBoolean()
                .WithColumn("upvotes").AsInt32().WithDefaultValue(0)
                .WithColumn("downvotes").AsInt32().WithDefaultValue(0);

            Create.ForeignKey().FromTable("comment").ForeignColumn("user_id").ToTable("\"user\"").PrimaryColumn("id");
            Create.ForeignKey().FromTable("comment").ForeignColumn("post_id").ToTable("post").PrimaryColumn("id");
            Create.ForeignKey().FromTable("comment").ForeignColumn("parent_id").ToTable("comment").PrimaryColumn("id");
        }

        public override void Down() {
            Delete.Table("comment");
        }
    }
}

