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
                "('Meditative 01','.././Music/anjos.mp3','.././Images/cover.png','2009-06-15T13:45:30',1,1)," +
                "('Piano 01','.././Music/aurora.mp3','.././Images/cover.png','2022-06-15T13:45:30',1,1)," +
                "('Vintage Electronic 01','.././Music/bulletBeat.mp3','.././Images/cover.png','2022-06-15T13:45:30',1,1)," +
                "('Vintage Electronic 02','.././Music/chip_synthwave.mp3','.././Images/cover.png','2022-06-15T13:45:30',1,1)," +
                "('Lofi 01','.././Music/chillhop01.mp3','.././Images/cover.png','2009-06-15T13:45:30',1,1)," +
                "('Chiptune 01','.././Music/clovis.mp3','.././Images/cover.png','2009-06-15T13:45:30',1,1)," +
                "('Electronic Rockabilly','.././Music/crazyRace.mp3','.././Images/cover.png','2009-06-15T13:45:30',1,1)," +
                "('Synthwave 01','.././Music/cyber01.mp3','.././Images/cover.png','2009-06-15T13:45:30',1,1)," +
                "('Synthwave 02','.././Music/incontrolavel.mp3','.././Images/cover.png','2009-06-15T13:45:30',1,1)," +
                "('Synthwave 03','.././Music/introBulletBeat.mp3','.././Images/cover.png','2009-06-15T13:45:30',1,1)," +
                "('Epic Hybrid Orchestra','.././Music/menu_music.mp3','.././Images/cover.png','2009-06-15T13:45:30',1,1)," +
                "('Synthwave 04','.././Music/menuBulletBeat.mp3','.././Images/cover.png','2009-06-15T13:45:30',1,1)," +
                "('Orchestral Hybrid','.././Music/music_basic.mp3','.././Images/cover.png','2009-06-15T13:45:30',1,1)," +
                "('Orchestral Hybrid Energic','.././Music/music_boss.mp3','.././Images/cover.png','2009-06-15T13:45:30',1,1)," +
                "('Lofi 02','.././Music/nonchalance.mp3','.././Images/cover.png','2009-06-15T13:45:30',1,1)," +
                "('Chiptune 02','.././Music/Phantom.mp3','.././Images/cover.png','2009-06-15T13:45:30',1,1)," +
                "('Electronic Energic','.././Music/Rider.mp3','.././Images/cover.png','2009-06-15T13:45:30',1,1)," +
                "('Electronic Vintage','.././Music/robo.mp3','.././Images/cover.png','2009-06-15T13:45:30',1,1)," +
                "('Electronic Heavy','.././Music/Skull.mp3','.././Images/cover.png','2009-06-15T13:45:30',1,1)"
          );
            migrationBuilder.Sql(
           "INSERT " +
           "INTO Tags" +
               "(Title) " +
           "VALUES" +
               "('piano')," +
               "('relaxing')," +
               "('electronic')," +
               "('chiptune')," +
               "('lofi')," +
               "('orchestral')," +
               "('epic')," +
               "('cinematic')," +
               "('meditative')," +
               "('energic')," +
               "('mysterious')," +
               "('cyber')," +
               "('experimental')," +
               "('hybrid')," +
               "('heavy')"

         );

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
