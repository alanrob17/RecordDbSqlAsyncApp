using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using _rd = DAL.RecordDataAccess;
using _ad = DAL.ArtistDataAccess;

namespace RecordDbSqlAsync.Tests
{
    internal class RecordTest
    {
        public static string ToShortDate(DateTime bought)
        {
            var shortDate = "unk";

            if (bought != null)
            {
                // DateTime dt = Convert.ToDateTime(bought);

                shortDate = Heinemann.Components.Dates.ShortDateString(bought);
            }

            return shortDate;
        }

        public static async Task<string> ToString(Record record)
        {
            var artist = await _ad.GetArtistByIdAsync(record.ArtistId);
            var str = new StringBuilder();

            str.Append("<strong>RecordId: </strong>" + record.RecordId + "<br/>");
            str.Append("<strong>ArtistId: </strong>" + record.ArtistId + "<br/>");
            str.Append("<strong>ArtistName: </strong>" + artist.Name + "<br/>");
            str.Append("<strong>Name: </strong>" + record.Name + "<br/>");
            str.Append("<strong>Field: </strong>" + record.Field + "<br/>");
            str.Append("<strong>Recorded: </strong>" + record.Recorded + "<br/>");
            str.Append("<strong>Label: </strong>" + record.Label + "<br/>");
            str.Append("<strong>Pressing: </strong>" + record.Pressing + "<br/>");
            str.Append("<strong>Rating: </strong>" + record.Rating + "<br/>");
            str.Append("<strong>Discs: </strong>" + record.Discs + "<br/>");
            str.Append("<strong>Media: </strong>" + record.Media + "<br/>");

            if (record.Bought > DateTime.MinValue)
            {
                str.Append("<strong>Bought: </strong>" + ToShortDate(record.Bought) + "<br/>");
            }

            if (!string.IsNullOrEmpty(record.Cost.ToString(CultureInfo.InvariantCulture)))
            {
                str.Append($"<strong>Cost: </strong>$" + string.Format("{0:0.00}", record.Cost) + "<br/>");
            }

            if (!string.IsNullOrEmpty(record.CoverName))
            {
                str.Append("<strong>CoverName: </strong>" + record.CoverName + "<br/>");
            }

            if (!string.IsNullOrEmpty(record.Review))
            {
                str.Append("<strong>Review: </strong>" + record.Review + "<br/>");
            }

            return str.ToString();
        }

        internal static async Task GetRecordsAsync()
        {
            var records = await _rd.SelectAsync();

            foreach (var record in records)
            {
                await Console.Out.WriteLineAsync($"{record.ArtistName} -- {record.Recorded} - {record.Name} ({record.Media})\n");
            }
        }

        internal static async Task GetRecordByIdAsync(int recordId)
        {
            var record = await _rd.SelectAsync(recordId);

            if (record != null)
            {
                await Console.Out.WriteLineAsync($"Id: {record.RecordId} - {record.Recorded} - {record.Name} ({record.Media})");
            }
            else
            {
                await Console.Out.WriteLineAsync($"Record with Id {recordId} not found.");
            }
        }

        internal static async Task GetSelectedRecordsAsync(string show)
        {
            var records = await _rd.SelectAsync(show);

            foreach (var record in records)
            {
                await Console.Out.WriteLineAsync($"{record.ArtistName} -- {record.Recorded} - {record.Name} ({record.Media})");
            }
        }

        internal static async Task SelectArtistRecordsAsync(int artistId)
        {
            var records = await _rd.SelectArtistRecordsAsync(artistId);

            foreach (var record in records)
            {
                await Console.Out.WriteLineAsync($"{record.RecordId} - {record.Name}");
            }
        }

        internal static async Task SelectRecordReviewsAsync()
        {
            var records = await _rd.SelectRecordReviewsAsync();

            foreach (var record in records)
            {
                var review = string.IsNullOrEmpty(record.Review) ? "No Review" : (record.Review.Length > 60 ? record.Review.Substring(0, 60) + "..." : record.Review);

                await Console.Out.WriteLineAsync($"{record.ArtistName} -- {record.Name}\t ({review})");
            }
        }

        internal static async Task GetRecordByNameAsync(string name)
        {
            var record = await _rd.GetRecordByNameAsync(name);

            string recordString = await RecordTest.ToString(record);

            if (record != null)
            {
                await Console.Out.WriteLineAsync(recordString);
            }
        }

        internal static async Task GetRecordsByArtistIdAsync(int artistId)
        {
            var artist = await _ad.GetArtistByIdAsync(artistId);

            if (artist == null)
            {
                await Console.Out.WriteLineAsync($"Artist Id:{artistId} not found!");
                return;
            }

            await Console.Out.WriteLineAsync($"{artist.Name}:\n");

            var records = await _rd.GetRecordsByArtistIdAsync(artistId);

            foreach (var record in records)
            {
                await Console.Out.WriteLineAsync($"{record.Recorded} - {record.Name} ({record.Media})");
            }
        }

        internal static async Task CreateRecordAsync(Record record)
        {
            var recordId = await _rd.AddRecordAsync(record);

            if (recordId > 0)
            {
                await Console.Out.WriteLineAsync($"New RecordId: {recordId}");
            }
            else
            {
                await Console.Out.WriteLineAsync($"Record wasn't added to the database!");
            }
        }

        internal static async Task UpdateRecordAsync(Record record)
        {
            var newRecordId = await _rd.UpdateRecordAsync(record);

            if (newRecordId > 0)
            {
                await Console.Out.WriteLineAsync($"New RecordId: {newRecordId}");
            }
            else
            {
                await Console.Out.WriteLineAsync($"Record wasn't updated!");
            }
        }

        internal static async Task UpdateRecordAsync(int recordId, string name, string field, int recorded, string label, string pressing, string rating, int discs, string media, DateTime bought, decimal cost, string coverName, string review)
        {
            var newRecordId = await _rd.UpdateRecordAsync(recordId, name, field, recorded, label, pressing, rating, discs, media, bought, cost, coverName, review);

            if (newRecordId > 0)
            {
                await Console.Out.WriteLineAsync($"Updated RecordId: {newRecordId}");
            }
            else
            {
                await Console.Out.WriteLineAsync($"Record wasn't updated!");
            }
        }

        internal static async Task DeleteRecordAsync(int recordId)
        {
            await _rd.DeleteAsync(recordId);

            var record = await _rd.SelectAsync(recordId);

            if (record != null)
            {
                await Console.Out.WriteLineAsync("Record not deleted!");
            }
            else
            {
                await Console.Out.WriteLineAsync("Record deleted.");
            }
        }

        internal static async Task GetRecordsByYearAsync(int year)
        {
            int count = await _rd.GetRecordsByYearAsync(year);

            await Console.Out.WriteLineAsync($"Number of Records recorded in {year} is {count}.");
        }

        internal static async Task CreateRecordAsync(int artistId, string name, string field, int recorded, string label, string pressing, string rating, int discs, string media, DateTime bought, decimal cost, string coverName, string review)
        {
            var recordId = await _rd.InsertRecordAsync(artistId, name, field, recorded, label, pressing, rating, discs, media, bought, cost, coverName, review);

            if (recordId > 0)
            {
                await Console.Out.WriteLineAsync($"New RecordId: {recordId}");
            }
            else
            {
                await Console.Out.WriteLineAsync($"Record not added to the database!");
            }
        }

        internal static async Task GetTotalNumberOfCDsAsync()
        {
            int count = await _rd.GetTotalNumberOfCDsAsync();

            await Console.Out.WriteLineAsync($"Number of CD's bought is {count}.");
        }

        internal static async Task CountDiscsAsync(string show)
        {
            var count = await _rd.CountDiscsAsync(show);

            await Console.Out.WriteLineAsync($"Number of {show} bought is {count}.");
        }

        internal static async Task GetArtistNumberOfRecordsAsync(int artistId)
        {
            var artist = await _ad.GetArtistByIdAsync(artistId);

            string count = await _rd.GetArtistNumberOfRecordsAsync(artistId);

            await Console.Out.WriteLineAsync($"{artist.Name} has {count} discs.");
        }

        internal static async Task GetNoRecordReviewAsync()
        {
            var records = await _rd.NoRecordReviewsAsync();

            foreach (var record in records)
            {
                await Console.Out.WriteLineAsync($"{record.ArtistName} -- {record.RecordId} - {record.Name}.");
            }
        }

        internal static async Task GetNoReviewCountAsync()
        {
            int count = await _rd.GetNoReviewCountAsync();

            await Console.Out.WriteLineAsync($"Number of Albums with no review is {count}.");
        }

        internal static async Task GetBoughtDiscCountForYearAsync(int year)
        {
            int count = await _rd.GetBoughtDiscCountForYear(year);

            await Console.Out.WriteLineAsync($"Number of Albums bought in {year} is {count}.");
        }

        internal static async Task GetTotalNumberOfDiscsAsync()
        {
            int count = await _rd.GetTotalNumberOfDiscsAsync();

            await Console.Out.WriteLineAsync($"Total number of Albums is {count}.");
        }

        internal static async Task GetTotalNumberOfBluraysAsync()
        {
            int count = await _rd.GetTotalNumberOfBluraysAsync();

            await Console.Out.WriteLineAsync($"Total number of Blu-rays is {count}.");
        }

        internal static async Task GetRecordListAsync()
        {
            var artists = await _ad.GetArtistsAsync();
            var records = await _rd.SelectAsync();

            foreach (var artist in artists)
            {
                await Console.Out.WriteLineAsync($"{artist.Name}:\n");

                var ar = from r in records
                         where artist.ArtistId == r.ArtistId
                         orderby r.Recorded descending
                         select r;

                foreach (var rec in ar)
                {
                    await Console.Out.WriteLineAsync($"\t{rec.Recorded} - {rec.Name} ({rec.Media})");
                }

                await Console.Out.WriteLineAsync();
            }
        }

        internal static async Task GetRecordDetailsAsync(int recordId)
        {
            Record record = await _rd.GetRecordDetailsAsync(recordId);

            if (record != null)
            {
                var review = string.IsNullOrEmpty(record.Review) ? "No Review" : (record.Review.Length > 60 ? record.Review.Substring(0, 60) + "..." : record.Review);

                await Console.Out.WriteLineAsync($"{record.ArtistName}:\n\t{record.Recorded} - {record.Name} ({record.Media})\n\t\t{record.Pressing} - {record.Rating} - {ToShortDate(record.Bought)} - ${string.Format("{0:0.00}", record.Cost)}\n\t\t{review}");
            }
        }

        internal static async Task GetArtistNameFromRecordAsync(int recordId)
        {
            string name = await _rd.GetArtistNameFromRecordAsync(recordId);

            if (name != null)
            {
                await Console.Out.WriteLineAsync($"Recordid: {recordId} - Artist name: {name}");
            }
        }

        internal static async Task GetTotalArtistCostAsync()
        {
            List<dynamic> data = new();

            data = await _rd.GetTotalArtistCostAsync();

            foreach (var item in data) 
            {
                await Console.Out.WriteLineAsync($"Total cost for {item.Name} with {item.TotalDiscs} discs is ${item.TotalCost:F2}.");
            }
        }

        internal static async Task GetTotalArtistDiscsAsync()
        {
            List<dynamic> data = await _rd.GetTotalArtistDiscsAsync();

            foreach (var item in data)
            {
                await Console.Out.WriteLineAsync($"Total discs for {item.Name} is {item.TotalDiscs}.");
            }
        }

        internal static async Task RecordHtmlAsync(int recordId)
        {
            Record record = await _rd.GetRecordDetailsAsync(recordId);

            string recordString = await RecordTest.ToString(record);

            if (record != null)
            {
                await Console.Out.WriteLineAsync(recordString);
            }
        }
    }
}
