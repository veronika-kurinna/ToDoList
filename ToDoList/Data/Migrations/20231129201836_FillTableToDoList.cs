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
            migrationBuilder.Sql("INSERT INTO ToDoList (Task) VALUES ('Read article')");
            migrationBuilder.Sql("INSERT INTO ToDoList (Task) VALUES ('Clean flat')");
            migrationBuilder.Sql("INSERT INTO ToDoList (Task) VALUES ('Bake cake')");
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
