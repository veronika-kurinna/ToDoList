using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Migrations
{
    /// <inheritdoc />
    public partial class FillTableToDoListItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO ToDoListItems (Name) VALUES ('Read article')");
            migrationBuilder.Sql("INSERT INTO ToDoListItems (Name) VALUES ('Check email')");
            migrationBuilder.Sql("INSERT INTO ToDoListItems (Name) VALUES ('Discuss project')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
