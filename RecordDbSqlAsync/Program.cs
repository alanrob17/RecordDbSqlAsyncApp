using System.Collections.Generic;
using _rt = RecordDbSqlAsync.Tests.RecordTest;
using _at = RecordDbSqlAsync.Tests.ArtistTest;
using DAL.Models;

namespace RecordDbSqlAsync
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //// *** Artist methods ***

            //await _at.GetArtistsAsync();

            //await _at.GetArtistListAsync();

            //var artistId = 114;
            //await _at.GetArtistByIdAsync(artistId);

            //var recordId = 71;
            //await _at.GetArtistByRecordIdAsync(recordId);

            //var name = "Bruce Cockburn";
            //await _at.GetArtistByNameAsync(name);

            //await _at.GetArtistsWithNoBioAsync();

            // await _at.GetNoBiographyCountAsync();

            //var artist = new Artist
            //{
            //    ArtistId = 0,
            //    FirstName = "Charley",
            //    LastName = "Robson",
            //    Biography = "Alan is a Country & Western singer, he likes both kinds of music."
            //};
            //await _at.AddArtistAsync(artist);

            //var firstName = "James";
            //var lastName = "Robson";
            //var biography = "James is a Hip Hop singer.";
            //await _at.AddArtistAsync(firstName, lastName, biography);


            //var artistId = 848;
            //var firstName = "James";
            //var lastName = "Robson";
            //var name = "James Robson";
            //var biography = "James is an Australian Rap star.";
            //await _at.UpdateArtistAsync(artistId, firstName, lastName, name, biography);


            //var artist = new Artist
            //{
            //    ArtistId = 847,
            //    FirstName = "Charley",
            //    LastName = "Robson",
            //    Name = "Charley Robson",
            //    Biography = "Charley is a Country & Western singer, he won awards at Tamworth Music Festival."
            //};
            //await _at.UpdateArtistAsync(artist);

            //var firstName = "Bob";
            //var lastName = "Dylan";
            //await _at.GetArtistIdAsync(firstName, lastName);

            //var recordId = 2196;
            //await _at.GetArtistIdAsync(recordId);

            //var artistId = 848;
            //await _at.DeleteAsync(artistId);

            //var artistId = 93;
            //await _at.ArtistHtmlAsync(artistId);

            var recordId = 2196;
            await _at.GetBiographyAsync(recordId);

            //// *** Record methods ***

            //await _rt.GetRecordsAsync();

            //var recordId = 2196;
            //await _rt.GetRecordByIdAsync(recordId);

            //var show = "aid114";
            //await _rt.GetSelectedRecordsAsync(show);

            //var artistId = 114;
            //await _rt.SelectArtistRecordsAsync(artistId);
        }
    }
}
