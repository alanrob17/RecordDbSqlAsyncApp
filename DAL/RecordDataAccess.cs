using DAL.Components;
using DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _ap = DAL.Components.AppSettings;

namespace DAL
{
    public class RecordDataAccess
    {
        public static async Task<List<Record>> SelectAsync()
        {
            var records = new List<Record>();

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            using (var cmd = new SqlCommand("up_RecordSelectAll", cn) { CommandType = CommandType.StoredProcedure })
            {
                await cn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var record = new Record
                        {
                            ArtistName = reader.Field<string>("ArtistName"),
                            ArtistId = reader.Field<int>("ArtistId"),
                            RecordId = reader.Field<int>("RecordId"),
                            Name = reader.Field<string>("Name"),
                            Field = reader.Field<string>("Field"),
                            Recorded = reader.Field<int>("Recorded"),
                            Label = reader.Field<string>("Label"),
                            Pressing = reader.Field<string>("Pressing"),
                            Rating = reader.Field<string>("Rating"),
                            Discs = reader.Field<int>("Discs"),
                            Media = reader.Field<string>("Media"),
                            Bought = reader.Field<DateTime?>("Bought").GetValueOrDefault(),
                            Cost = reader.Field<decimal?>("Cost").GetValueOrDefault(),
                            CoverName = reader.Field<string>("CoverName"),
                            Review = reader.Field<string>("Review")
                        };

                        records.Add(record);
                    }
                }
            }

            return records;
        }

        public static async Task<Record> SelectAsync(int recordId)
        {
            using (var connection = new SqlConnection(_ap.Instance.ConnectionString))
            {
                await connection.OpenAsync();

                var sql = "up_RecordSelectById";
                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RecordId", recordId);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Record
                            {
                                ArtistName = reader.Field<string>("ArtistName"),
                                ArtistId = reader.Field<int>("ArtistId"),
                                RecordId = reader.Field<int>("RecordId"),
                                Name = reader.Field<string>("Name"),
                                Field = reader.Field<string>("Field"),
                                Recorded = reader.Field<int>("Recorded"),
                                Label = reader.Field<string>("Label"),
                                Pressing = reader.Field<string>("Pressing"),
                                Rating = reader.Field<string>("Rating"),
                                Discs = reader.Field<int>("Discs"),
                                Media = reader.Field<string>("Media"),
                                Bought = DataConvert.ConvertTo<DateTime>(reader["Bought"], default(DateTime)),
                                Cost = DataConvert.ConvertTo<decimal>(reader["Cost"], default(decimal)),
                                CoverName = reader.Field<string>("CoverName"),
                                Review = reader.Field<string>("Review")
                            };
                        }
                    }
                }
            }

            return null;
        }

        public static async Task<List<Record>> SelectAsync(string show)
        {
            if (show == null)
            {
                throw new ArgumentNullException("show");
            }

            var records = new List<Record>();

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            using (var cmd = new SqlCommand("up_RecordSelectShowNew", cn) { CommandType = CommandType.StoredProcedure })
            {
                await cn.OpenAsync();

                cmd.Parameters.AddWithValue("@show", show);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var record = new Record
                        {
                            ArtistName = reader.Field<string>("ArtistName"),
                            ArtistId = reader.Field<int>("ArtistId"),
                            RecordId = reader.Field<int>("RecordId"),
                            Name = reader.Field<string>("Name"),
                            Field = reader.Field<string>("Field"),
                            Recorded = reader.Field<int>("Recorded"),
                            Label = reader.Field<string>("Label"),
                            Pressing = reader.Field<string>("Pressing"),
                            Rating = reader.Field<string>("Rating"),
                            Discs = reader.Field<int>("Discs"),
                            Media = reader.Field<string>("Media"),
                            Bought = reader.Field<DateTime?>("Bought").GetValueOrDefault(),
                            Cost = reader.Field<decimal?>("Cost").GetValueOrDefault(),
                            CoverName = reader.Field<string>("CoverName"),
                            Review = reader.Field<string>("Review")
                        };

                        records.Add(record);
                    }
                }
            }

            return records;
        }

        public static async Task<List<Record>> SelectArtistRecordsAsync(int artistId)
        {
            var records = new List<Record>();

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            using (var cmd = new SqlCommand("up_getRecordListAndNone", cn) { CommandType = CommandType.StoredProcedure })
            {
                await cn.OpenAsync();

                cmd.Parameters.AddWithValue("@artistId", artistId);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var record = new Record
                        {
                            RecordId = reader.Field<int>("RecordId"),
                            Name = reader.Field<string>("Name")
                        };

                        records.Add(record);
                    }
                }
            }

            return records;
        }
    }
}
