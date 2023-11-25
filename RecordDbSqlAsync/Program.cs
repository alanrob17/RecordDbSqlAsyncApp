using System.Collections.Generic;
using _rt = RecordDbSqlAsync.Tests.RecordTest;
using _at = RecordDbSqlAsync.Tests.ArtistTest;
using DAL.Models;
using Microsoft.Extensions.Hosting;

namespace RecordDbSqlAsync
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //// -----------------------------------------------------------------------------
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
            //    Biography = "Charley is a Country & Western singer, he likes both kinds of music."
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

            //var recordId = 2196;
            //await _at.GetBiographyAsync(recordId);

            //// -----------------------------------------------------------------------------
            //// *** Record methods ***

            //await _rt.GetRecordsAsync();

            //var recordId = 2196;
            //await _rt.GetRecordByIdAsync(recordId);

            //var show = "cd";
            //await _rt.GetSelectedRecordsAsync(show);

            //var artistId = 114;
            //await _rt.SelectArtistRecordsAsync(artistId);

            // await _rt.SelectRecordReviewsAsync();

            //var name = "Cutting Edge";
            //await _rt.GetRecordByNameAsync(name);

            //var artistId = 114;
            //await _rt.GetRecordsByArtistIdAsync(artistId);

            //var dateString = "2023/09/27 12:00:00 AM";
            //DateTime date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture);
            //var record = new Record
            //{
            //    RecordId = 0,
            //    ArtistId = 849,
            //    Name = "Lost In Wonderland",
            //    Field = "Jazz",
            //    Recorded = 2023,
            //    Label = "Rubber Dubber",
            //    Pressing = "Aus",
            //    Rating = "****",
            //    Discs = 1,
            //    Media = "CD",
            //    Bought = date,
            //    Cost = 26.99m,
            //    CoverName = null,
            //    Review = "This is Charley's first album",
            //};
            //await _rt.CreateRecordAsync(record);

            //var dateString = "2023/09/27 12:00:00 AM";
            //DateTime date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture);
            //var recordId = 0;
            //var artistId = 849;
            //var name = "Found In Wonderland";
            //var field = "Blues";
            //var recorded = 2023;
            //var label = "Rubber Dubber Flubber";
            //var pressing = "Aus";
            //var rating = "***";
            //var discs = 2;
            //var media = "CD";
            //var bought = date;
            //var cost = 32.99m;
            //string coverName = null;
            //var review = "This is Charley's second album this year!";
            //await _rt.CreateRecordAsync(artistId, name, field, recorded, label, pressing, rating, discs, media, bought, cost, coverName, review);

            //var dateString = "2023/10/28 12:00:00 AM";
            //DateTime date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture);
            //var record = new Record
            //{
            //    RecordId = 5269,
            //    Name = "Lost In A Winter Wonderland",
            //    Field = "Rock",
            //    Recorded = 2023,
            //    Label = "Rubber Dubber Dubber",
            //    Pressing = "Am",
            //    Rating = "***",
            //    Discs = 2,
            //    Media = "CD",
            //    Bought = date,
            //    Cost = 31.50m,
            //    CoverName = null,
            //    Review = "This is Charley's first Christmas album",
            //};
            //await _rt.UpdateRecordAsync(record);

            //var dateString = "2023/11/23 12:00:00 AM";
            //DateTime date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture);
            //var recordId = 5270;
            //    var name = "Lost In A Winter Wonderland";
            //var field = "Rock";
            //var recorded = 2023;
            //var label = "Rubber Dubber Ducky";
            //var pressing = "Aus";
            //var rating = "****";
            //var discs = 1;
            //var media = "R";
            //var bought = date;
            //var cost = 65.50m;
            //string coverName = null;
            //var review = "This is Charley's first rockin' Christmas album.";
            //await _rt.UpdateRecordAsync(recordId, name, field, recorded, label, pressing, rating, discs, media, bought, cost, coverName, review);

            //var recordId = 5269;
            //await _rt.DeleteRecordAsync(recordId);

            //var artistId = 114;
            //await _rt.GetArtistNumberOfRecordsAsync(artistId);

            var year = 1987;
            await _rt.GetRecordsByYearAsync(year);
            // GetRecordedYearNumber(int year)

            //await _rt.GetTotalNumberOfCDsAsync();

            //await _rt.CountDiscsAsync("ALL");

            // await _rt.CountDiscsAsync("DVDs");

            // await _rt.CountDiscsAsync("CD");

            // await _rt.CountDiscsAsync("Records");


            //await _rt.GetTotalNumberOfDiscsAsync(); 
            //await _rt.GetTotalNumberOfRecordsAsync(); 
            //await _rt.GetTotalNumberOfBluraysAsync();
            //await _rt.GetRecordListAsync();
            //await _rt.GetRecordListMultipleTablesAsync();
            


            //await _rt.GetArtistRecordEntityAsync(2196);
            //await _rt.GetRecordDetailsAsync(2196);
            //await _rt.GetArtistNameFromRecordAsync(2196);
            //await _rt.GetDiscCountForYearAsync(1974);
            //await _rt.GetBoughtDiscCountForYearAsync("2000");
            //await _rt.GetNoRecordReviewAsync();
            //await _rt.GetNoReviewCountAsync();
            //await _rt.GetTotalArtistCostAsync();
            //await _rt.GetTotalArtistDiscsAsync();
            //await _rt.RecordHtmlAsync(2196);
        }
    }
}
