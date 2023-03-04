using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicBankAPI.Migrations
{
    /// <inheritdoc />
    public partial class addmigrationInitialCharge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
            "INSERT " +
            "INTO Artists" +
                "(Name) " +
            "VALUES" +
                "('Tatyana Jacques')," +
                "('Ruido Rosa')," +
                "('Imaginary Sounds')");

            migrationBuilder.Sql(
            "INSERT " +
            "INTO Composers" +
                "(Name) " +
            "VALUES" +
                "('Tatyana Jacques')," +
                "('Erik Satie')," +
                "('Frédéric Chopin')," +
                "('Johann Sebastian Bach')," +
                "('Claude Debussy')");


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
