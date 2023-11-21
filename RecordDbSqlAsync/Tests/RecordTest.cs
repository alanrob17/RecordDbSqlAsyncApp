using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using _rd = DAL.RecordDataAccess;

namespace RecordDbSqlAsync.Tests
{
    internal class RecordTest
    {
        internal static async Task GetRecordsAsync()
        {
            var records = await _rd.SelectAsync();

            foreach (var record in records)
            {
                Console.WriteLine($"{record.ArtistName} -- {record.Recorded} - {record.Name} ({record.Media})\n");
            }
        }

        internal static async Task GetRecordByIdAsync(int recordId)
        {
            var record = await _rd.SelectAsync(recordId);

            if (record != null)
            {
                Console.WriteLine($"Id: {record.RecordId} - {record.Recorded} - {record.Name} ({record.Media})");
            }
            else
            {
                Console.WriteLine($"Record with Id {recordId} not found.");
            }
        }

        internal static async Task GetSelectedRecordsAsync(string show)
        {
            var records = await _rd.SelectAsync(show);

            foreach (var record in records)
            {
                Console.WriteLine($"{record.ArtistName} -- {record.Recorded} - {record.Name} ({record.Media})");
            }
        }

        internal static async Task SelectArtistRecordsAsync(int artistId)
        {
            var records = await _rd.SelectArtistRecordsAsync(artistId);

            foreach (var record in records)
            {
                Console.WriteLine($"{record.RecordId} - {record.Name}");
            }
        }
    }
}
