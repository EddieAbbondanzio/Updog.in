using FluentMigrator;
using Updog.Domain;

namespace Updog.Persistance {
    [Migration(2019_10_16_2, "Add user table.")]
    public class AddPostTableMigrate : Migration {
        public override void Up() {
            Create.Table("Post")
                .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Identity()
                .WithColumn("UserId").AsInt32().NotNullable()
                .WithColumn("SpaceId").AsInt32().NotNullable()
                .WithColumn("Type").AsInt16().NotNullable()
                .WithColumn("Title").AsString(300).NotNullable()
                .WithColumn("Body").AsString(10_000).NotNullable()
                .WithColumn("CreationDate").AsDateTime().NotNullable()
                .WithColumn("WasUpdated").AsBoolean().WithDefaultValue(false)
                .WithColumn("WasDeleted").AsBoolean().WithDefaultValue(false)
                .WithColumn("Upvotes").AsInt32().NotNullable().WithDefaultValue(0)
                .WithColumn("Downvotes").AsInt32().NotNullable().WithDefaultValue(0);

            Create.ForeignKey().FromTable("Post").ForeignColumn("UserId").ToTable("\"User\"").PrimaryColumn("Id");
            Create.ForeignKey().FromTable("Post").ForeignColumn("SpaceId").ToTable("Space").PrimaryColumn("Id");
        }

        public override void Down() {
            Delete.Table("Post");
        }
    }
}