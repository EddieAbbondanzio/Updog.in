using FluentMigrator;
using Updog.Domain;

namespace Updog.Persistance {
    [Migration(2019_10_16_3, "Add comment table.")]
    public class AddCommentTableMigrate : Migration {
        public override void Up() {
            Create.Table("Comment")
                .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("UserId").AsInt32().NotNullable()
                .WithColumn("PostId").AsInt32().NotNullable()
                .WithColumn("ParentId").AsInt32()
                .WithColumn("Body").AsString(10_000).NotNullable()
                .WithColumn("CreationDate").AsDateTime().NotNullable()
                .WithColumn("WasUpdated").AsBoolean()
                .WithColumn("WasDeleted").AsBoolean()
                .WithColumn("Upvotes").AsInt32().WithDefaultValue(0)
                .WithColumn("Downvotes").AsInt32().WithDefaultValue(0);

            Create.ForeignKey().FromTable("Comment").ForeignColumn("UserId").ToTable("\"User\"").PrimaryColumn("Id");
            Create.ForeignKey().FromTable("Comment").ForeignColumn("PostId").ToTable("Post").PrimaryColumn("Id");
            Create.ForeignKey().FromTable("Comment").ForeignColumn("ParentId").ToTable("Comment").PrimaryColumn("Id");
        }

        public override void Down() {
            Delete.Table("Comment");
        }
    }
}

