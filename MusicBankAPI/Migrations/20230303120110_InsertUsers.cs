using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicBankAPI.Migrations
{
    /// <inheritdoc />
    public partial class InsertUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
            "INSERT " +
            "INTO Users" +
                "(Name,Email,Password,RegisterDate) " +
            "VALUES" +
                "('Crazy Games','crazygames@devinhouse.com','crazy123','2022-06-15T13:45:30')," +
                "('Moonlight Studio','moonligthstudio@devinhouse.com','moonligth123','2022-06-15T13:45:30')," +
                "('Bored Cat Games','boredcatgames@devinhouse.com','boredcat123','2009-06-15T13:45:30')");


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
