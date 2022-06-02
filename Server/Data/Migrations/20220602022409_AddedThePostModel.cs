using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Data.Migrations
{
    public partial class AddedThePostModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ThumbnailImagePath = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Excerpt = table.Column<string>(type: "TEXT", maxLength: 512, nullable: false),
                    Content = table.Column<string>(type: "TEXT", maxLength: 65536, nullable: false),
                    PublishDate = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    Published = table.Column<bool>(type: "INTEGER", nullable: false),
                    Author = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    categoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Posts_Categories_categoryId",
                        column: x => x.categoryId,
                        principalTable: "Categories",
                        principalColumn: "categoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "Author", "Content", "Excerpt", "PublishDate", "Published", "ThumbnailImagePath", "Title", "categoryId" },
                values: new object[] { 1, "Breno Trindade", "", "This is the excerpt for opst 1. An excerpt is a little extration from a larger piece of text. Sort of like a preview.", "02/06/2022 02:24", true, "uploads/placeholder.jpg", "First post", 1 });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "Author", "Content", "Excerpt", "PublishDate", "Published", "ThumbnailImagePath", "Title", "categoryId" },
                values: new object[] { 2, "Breno Trindade", "", "This is the excerpt for opst 2. An excerpt is a little extration from a larger piece of text. Sort of like a preview.", "02/06/2022 02:24", true, "uploads/placeholder.jpg", "Second post", 2 });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "Author", "Content", "Excerpt", "PublishDate", "Published", "ThumbnailImagePath", "Title", "categoryId" },
                values: new object[] { 3, "Breno Trindade", "", "This is the excerpt for opst 3. An excerpt is a little extration from a larger piece of text. Sort of like a preview.", "02/06/2022 02:24", true, "uploads/placeholder.jpg", "Third post", 3 });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "Author", "Content", "Excerpt", "PublishDate", "Published", "ThumbnailImagePath", "Title", "categoryId" },
                values: new object[] { 4, "Breno Trindade", "", "This is the excerpt for opst 4. An excerpt is a little extration from a larger piece of text. Sort of like a preview.", "02/06/2022 02:24", true, "uploads/placeholder.jpg", "Fourth post", 1 });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "Author", "Content", "Excerpt", "PublishDate", "Published", "ThumbnailImagePath", "Title", "categoryId" },
                values: new object[] { 5, "Breno Trindade", "", "This is the excerpt for opst 5. An excerpt is a little extration from a larger piece of text. Sort of like a preview.", "02/06/2022 02:24", true, "uploads/placeholder.jpg", "Fifith post", 2 });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "Author", "Content", "Excerpt", "PublishDate", "Published", "ThumbnailImagePath", "Title", "categoryId" },
                values: new object[] { 6, "Breno Trindade", "", "This is the excerpt for opst 6. An excerpt is a little extration from a larger piece of text. Sort of like a preview.", "02/06/2022 02:24", true, "uploads/placeholder.jpg", "Sixth post", 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_categoryId",
                table: "Posts",
                column: "categoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
