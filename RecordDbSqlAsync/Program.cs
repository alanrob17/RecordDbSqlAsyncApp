using System.Collections.Generic;
using _rt = RecordDbSqlAsync.Tests.RecordTest;

namespace RecordDbSqlAsync
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
             await _rt.GetRecordsAsync();

            //var recordId = 2196;
            //await _rt.GetRecordByIdAsync(recordId);

            //var show = "aid114";
            //await _rt.GetSelectedRecordsAsync(show);

            //var artistId = 114;
            //await _rt.SelectArtistRecordsAsync(artistId);
        }
    }
}
