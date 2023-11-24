using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using _rd = DAL.RecordDataAccess;
using _at = DAL.ArtistDataAccess;

namespace RecordDbSqlAsync.Tests
{
    internal class RecordTest
    {
        public static async Task<string> ToString(Record record)
        {
            var artist = await _at.GetArtistByIdAsync(record.ArtistId);
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
                str.Append("<strong>Bought: </strong>" + record.Bought.ToShortDateString() + "<br/>");
            }

            if (!string.IsNullOrEmpty(record.Cost.ToString(CultureInfo.InvariantCulture)))
            {
                str.Append("<strong>Cost: </strong>" + record.Cost + "<br/>");
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
            var artist = await _at.GetArtistByIdAsync(artistId);

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
            var newRecordId = await _rd.UpdateArtistAsync(record);

            if (newRecordId > 0)
            {
                await Console.Out.WriteLineAsync($"New RecordId: {newRecordId}");
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
    }
}
