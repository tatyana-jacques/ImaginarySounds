using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicBankAPI.Migrations
{
    /// <inheritdoc />
    public partial class SecondCharge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
            "INSERT " +
            "INTO Songs" +
                "(Title,StorageData,Cover,RegisterDate,ComposerId,ArtistId) " +
            "VALUES" +
                "('Meditative 01','./wwwroot/Music/anjos.mp3','./wwwroot/Images/cover.png','2009-06-15T13:45:30',1,1)," +
                "('Piano 01','./wwwroot/Music/aurora.mp3','./wwwroot/Images/cover.png','2022-06-15T13:45:30',1,1)," +
                "('Vintage Electronic 01','./wwwroot/Music/bulletBeat.mp3','./wwwroot/Images/cover.png','2022-06-15T13:45:30',1,1)," +
                "('Vintage Electronic 02','./wwwroot/Music/chip_synthwave.mp3','./wwwroot/Images/cover.png','2022-06-15T13:45:30',1,1)," +
                "('Lofi 01','./wwwroot/Music/chillhop01.mp3','./wwwroot/Images/cover.png','2009-06-15T13:45:30',1,1)"
          );
            migrationBuilder.Sql(
           "INSERT " +
           "INTO Tag" +
               "(Title) " +
           "VALUES" +
               "('piano')," +
               "('relaxing')," +
               "('electronic')," +
               "('chiptune')," +
               "('lofi')," +
               "('dark')"
         );


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
