using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicPlayer.DAL.Migrations
{
    public partial class FixTrackPlaylists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistTrack_Playlists_PlaylistId",
                table: "PlaylistTrack");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistTrack_Tracks_TrackId",
                table: "PlaylistTrack");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaylistTrack",
                table: "PlaylistTrack");

            migrationBuilder.RenameTable(
                name: "PlaylistTrack",
                newName: "PlaylistTracks");

            migrationBuilder.RenameIndex(
                name: "IX_PlaylistTrack_TrackId",
                table: "PlaylistTracks",
                newName: "IX_PlaylistTracks_TrackId");

            migrationBuilder.RenameIndex(
                name: "IX_PlaylistTrack_PlaylistId",
                table: "PlaylistTracks",
                newName: "IX_PlaylistTracks_PlaylistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaylistTracks",
                table: "PlaylistTracks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistTracks_Playlists_PlaylistId",
                table: "PlaylistTracks",
                column: "PlaylistId",
                principalTable: "Playlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistTracks_Tracks_TrackId",
                table: "PlaylistTracks",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistTracks_Playlists_PlaylistId",
                table: "PlaylistTracks");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistTracks_Tracks_TrackId",
                table: "PlaylistTracks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaylistTracks",
                table: "PlaylistTracks");

            migrationBuilder.RenameTable(
                name: "PlaylistTracks",
                newName: "PlaylistTrack");

            migrationBuilder.RenameIndex(
                name: "IX_PlaylistTracks_TrackId",
                table: "PlaylistTrack",
                newName: "IX_PlaylistTrack_TrackId");

            migrationBuilder.RenameIndex(
                name: "IX_PlaylistTracks_PlaylistId",
                table: "PlaylistTrack",
                newName: "IX_PlaylistTrack_PlaylistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaylistTrack",
                table: "PlaylistTrack",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistTrack_Playlists_PlaylistId",
                table: "PlaylistTrack",
                column: "PlaylistId",
                principalTable: "Playlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistTrack_Tracks_TrackId",
                table: "PlaylistTrack",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
