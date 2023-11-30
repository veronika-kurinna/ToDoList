using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.Migrations
{
    /// <inheritdoc />
    public partial class FillTableToDoList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO ToDoList VALUES ('Read article', 0)");
            migrationBuilder.Sql("INSERT INTO ToDoList VALUES ('Check email', 0)");
            migrationBuilder.Sql("INSERT INTO ToDoList VALUES ('Discuss project', 0)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM ToDoList WHERE Id = 1");
            migrationBuilder.Sql("DELETE FROM ToDoList WHERE Id = 2");
            migrationBuilder.Sql("DELETE FROM ToDoList WHERE Id = 3");
        }
    }
}
