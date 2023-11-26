using DAL.Components;
using DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Globalization;
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

        public static async Task<List<Record>> SelectRecordReviewsAsync()
        {
            var records = new List<Record>();

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            using (var cmd = new SqlCommand("up_SelectRecordReviews", cn) { CommandType = CommandType.StoredProcedure })
            {
                await cn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var record = new Record
                        {
                            ArtistName = reader.Field<string>("Name"),
                            Name = reader.Field<string>("Title"),
                            Review = reader.Field<string>("Review")
                        };

                        records.Add(record);
                    }
                }
            }

            return records;
        }

        public static async Task<int> InsertRecordAsync(int artistId, string name, string field, int recorded, string label, string pressing, string rating, int discs, string media, DateTime bought, decimal cost, string coverName, string review)
        {
            var recordId = -1; // 0 is used for record is already in the db.

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                var cmd = new SqlCommand("adm_RecordInsert", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.AddWithValue("RecordId", recordId);
                cmd.Parameters.AddWithValue("ArtistId", artistId);
                cmd.Parameters.AddWithValue("Name", name);
                cmd.Parameters.AddWithValue("Field", field);
                cmd.Parameters.AddWithValue("Recorded", recorded);
                cmd.Parameters.AddWithValue("Label", label);
                cmd.Parameters.AddWithValue("Pressing", pressing);
                cmd.Parameters.AddWithValue("Rating", rating);
                cmd.Parameters.AddWithValue("Discs", discs);
                cmd.Parameters.AddWithValue("Media", media);
                cmd.Parameters.AddWithValue("Bought", bought);
                cmd.Parameters.AddWithValue("Cost", cost);
                cmd.Parameters.AddWithValue("CoverName", coverName);
                cmd.Parameters.AddWithValue("Review", review);
                var parRecordId = new SqlParameter("@RecordId", SqlDbType.Int, 4)
                {
                    Direction = ParameterDirection.ReturnValue
                };
                cmd.Parameters.Add(parRecordId);

                using (cn)
                {
                    await cn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    recordId = (int)parRecordId.Value;
                }
            }

            return recordId;
        }

        public static async Task<int> UpdateRecordAsync(int recordId, string name, string field, int recorded, string label, string pressing, string rating, int discs, string media, DateTime bought, decimal cost, string coverName, string review)
        {
            var newRecordId = -1;

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                var cmd = new SqlCommand("adm_UpdateRecord", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.AddWithValue("RecordId", recordId);
                cmd.Parameters.AddWithValue("Name", name);
                cmd.Parameters.AddWithValue("Field", field);
                cmd.Parameters.AddWithValue("Recorded", recorded);
                cmd.Parameters.AddWithValue("Label", label);
                cmd.Parameters.AddWithValue("Pressing", pressing);
                cmd.Parameters.AddWithValue("Rating", rating);
                cmd.Parameters.AddWithValue("Discs", discs);
                cmd.Parameters.AddWithValue("Media", media);
                cmd.Parameters.AddWithValue("Bought", bought);
                cmd.Parameters.AddWithValue("Cost", cost);
                cmd.Parameters.AddWithValue("CoverName", coverName);
                cmd.Parameters.AddWithValue("Review", review);
                var parResult = new SqlParameter("@Result", SqlDbType.Int, 4)
                {
                    Direction = ParameterDirection.ReturnValue
                };
                cmd.Parameters.Add(parResult);

                using (cn)
                {
                    await cn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    newRecordId = (int)parResult.Value;
                }
            }

            return newRecordId;
        }

        public static async Task<string> CountDiscsAsync(string show)
        {
            var discs = 0;

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                var cmd = new SqlCommand("up_CountDiscs", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.AddWithValue("@Show", show);

                await cn.OpenAsync();

                var result = await cmd.ExecuteScalarAsync();

                // Check if the result is not null before casting
                if (result != null && result != DBNull.Value)
                {
                    discs = (int)result;
                }
            }

            return discs.ToString(CultureInfo.InvariantCulture);
        }

        public static async Task<string> GetArtistNumberOfRecordsAsync(int artistId)
        {
            var discs = 0;

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                var cmd = new SqlCommand("up_GetArtistNumberOfRecords", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.AddWithValue("@artistId", artistId);

                await cn.OpenAsync();

                var result = await cmd.ExecuteScalarAsync();

                // I'm getting a null back instead of 0
                if (result != null && result != DBNull.Value)
                {
                    discs = (int)result;
                }

                return discs.ToString(CultureInfo.InvariantCulture);
            }
        }

        public static async Task<List<Record>> NoRecordReviewsAsync()
        {
            var records = new List<Record>();

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            using (var cmd = new SqlCommand("up_NoRecordReviews", cn) { CommandType = CommandType.StoredProcedure })
            {
                await cn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var record = new Record
                        {
                            ArtistName = reader.Field<string>("Name"),
                            Name = reader.Field<string>("Record"),
                            RecordId = reader.Field<int>("RecordId")
                        };

                        records.Add(record);
                    }
                }
            }

            return records;
        }

        public static async Task<Record> GetRecordByNameAsync(string name)
        {
            using (var connection = new SqlConnection(_ap.Instance.ConnectionString))
            {
                await connection.OpenAsync();

                var sql = "up_GetRecordByPartialName";
                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", name);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Record
                            {
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

        public static async Task<List<Record>> GetRecordsByArtistIdAsync(int artistId)
        {
            var records = new List<Record>();

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            using (var cmd = new SqlCommand("up_GetRecordsByArtistId", cn) { CommandType = CommandType.StoredProcedure })
            {
                await cn.OpenAsync();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ArtistId", artistId);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var record = new Record
                        {
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

        public static async Task<int> AddRecordAsync(Record record)
        {
            var recordId = 0;

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                var cmd = new SqlCommand("adm_RecordInsert", cn) { CommandType = CommandType.StoredProcedure };
                 
                cmd.Parameters.AddWithValue("RecordId", record.RecordId);
                cmd.Parameters.AddWithValue("ArtistId", record.ArtistId);
                cmd.Parameters.AddWithValue("Name", record.Name);
                cmd.Parameters.AddWithValue("Field", record.Field);
                cmd.Parameters.AddWithValue("Recorded", record.Recorded);
                cmd.Parameters.AddWithValue("Label", record.Label);
                cmd.Parameters.AddWithValue("Pressing", record.Pressing);
                cmd.Parameters.AddWithValue("Rating", record.Rating);
                cmd.Parameters.AddWithValue("Discs", record.Discs);
                cmd.Parameters.AddWithValue("Media", record.Media);
                cmd.Parameters.AddWithValue("Bought", record.Bought);
                cmd.Parameters.AddWithValue("Cost", record.Cost);
                cmd.Parameters.AddWithValue("CoverName", record.CoverName);
                cmd.Parameters.AddWithValue("Review", record.Review);
                var parRecordId = new SqlParameter("@RecordId", SqlDbType.Int, 4)
                {
                    Direction = ParameterDirection.ReturnValue
                };
                cmd.Parameters.Add(parRecordId);

                using (cn)
                {
                    await cn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    recordId = (int)parRecordId.Value;
                }
            }

            return recordId;
        }

        public static async Task<int> UpdateRecordAsync(Record record)
        {
            var newRecordId = -1;

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                var cmd = new SqlCommand("adm_UpdateRecord", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.AddWithValue("RecordId", record.RecordId);
                cmd.Parameters.AddWithValue("Name", record.Name);
                cmd.Parameters.AddWithValue("Field", record.Field);
                cmd.Parameters.AddWithValue("Recorded", record.Recorded);
                cmd.Parameters.AddWithValue("Label", record.Label);
                cmd.Parameters.AddWithValue("Pressing", record.Pressing);
                cmd.Parameters.AddWithValue("Rating", record.Rating);
                cmd.Parameters.AddWithValue("Discs", record.Discs);
                cmd.Parameters.AddWithValue("Media", record.Media);
                cmd.Parameters.AddWithValue("Bought", record.Bought);
                cmd.Parameters.AddWithValue("Cost", record.Cost);
                cmd.Parameters.AddWithValue("CoverName", record.CoverName);
                cmd.Parameters.AddWithValue("Review", record.Review);
                var parRecordId = new SqlParameter("@Result", SqlDbType.Int, 4)
                {
                    Direction = ParameterDirection.ReturnValue
                };
                cmd.Parameters.Add(parRecordId);

                using (cn)
                {
                    await cn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    newRecordId = (int)parRecordId.Value;
                }
            }

            return newRecordId;
        }

        public static async Task DeleteAsync(int recordId)
        {
            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                var cmd = new SqlCommand("up_deleteRecord", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.AddWithValue("@RecordId", recordId);

                await cn.OpenAsync();

                try
                {
                    await cmd.ExecuteNonQueryAsync();
                }
                catch (Exception)
                {
                    recordId = 0;
                }
            }
        }

        public static async Task<int> GetRecordsByYearAsync(int year)
        {
            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                var cmd = new SqlCommand("up_GetRecordedYearNumber", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.AddWithValue("@Year", year);

                await cn.OpenAsync();

                var result = await cmd.ExecuteScalarAsync();

                // I'm getting a null back instead of 0
                if (result != null && result != DBNull.Value)
                {
                    return (int)result;
                }

                return 0;
            }
        }

        public static async Task<int> GetTotalNumberOfCDsAsync()
        {
            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                var cmd = new SqlCommand("adm_GetTotalCDCount", cn) { CommandType = CommandType.StoredProcedure };

                await cn.OpenAsync();

                var result = await cmd.ExecuteScalarAsync();

                // I'm getting a null back instead of 0
                if (result != null && result != DBNull.Value)
                {
                    return (int)result;
                }

                return 0;
            }
        }

        public static async Task<int> GetNoReviewCountAsync()
        {
            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                var cmd = new SqlCommand("up_GetNoRecordReviewCount", cn) { CommandType = CommandType.StoredProcedure };

                await cn.OpenAsync();

                var result = await cmd.ExecuteScalarAsync();

                if (result != null && result != DBNull.Value)
                {
                    return (int)result;
                }

                return 0;
            }
        }

        public static async Task<int> GetBoughtDiscCountForYear(int year)
        {
            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                var cmd = new SqlCommand("up_GetTotalYearNumber", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.AddWithValue("@Year", year);

                await cn.OpenAsync();

                var result = await cmd.ExecuteScalarAsync();

                if (result != null && result != DBNull.Value)
                {
                    return (int)result;
                }

                return 0;
            }
        }

        public static async Task<int> GetTotalNumberOfDiscsAsync()
        {
            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                var cmd = new SqlCommand("up_GetTotalNumberOfAllRecords", cn) { CommandType = CommandType.StoredProcedure };

                await cn.OpenAsync();

                var result = await cmd.ExecuteScalarAsync();

                if (result != null && result != DBNull.Value)
                {
                    return (int)result;
                }

                return 0;
            }
        }

        public static async Task<int> GetTotalNumberOfBluraysAsync()
        {
            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                var cmd = new SqlCommand("up_GetTotalNumberOfAllBlurays", cn) { CommandType = CommandType.StoredProcedure };

                await cn.OpenAsync();

                var result = await cmd.ExecuteScalarAsync();

                if (result != null && result != DBNull.Value)
                {
                    return (int)result;
                }

                return 0;
            }
        }

        public static async Task<Record> GetRecordDetailsAsync(int recordId)
        {
            using (var connection = new SqlConnection(_ap.Instance.ConnectionString))
            {
                await connection.OpenAsync();

                var sql = "up_getSingleArtistAndRecord";
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

        public static async Task<string> GetArtistNameFromRecordAsync(int recordId)
        {
            var name = string.Empty;

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                var cmd = new SqlCommand("up_GetArtistNameByRecordId", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.AddWithValue("@RecordId", recordId);

                await cn.OpenAsync();

                var result = await cmd.ExecuteScalarAsync();

                if (result != null && result != DBNull.Value)
                {
                    name = (string)result;
                }
            }

            return name;
        }

        public static async Task<List<dynamic>> GetTotalArtistCostAsync()
        {
            var costs = new List<dynamic>();

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            using (var cmd = new SqlCommand("sp_getTotalsForEachArtist", cn) { CommandType = CommandType.StoredProcedure })
            {
                await cn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        dynamic cost = new ExpandoObject();

                        cost.ArtistId = reader.Field<int>("ArtistId");
                        cost.Name = reader.Field<string>("Name");
                        cost.TotalDiscs = reader.Field<int>("TotalDiscs");
                        cost.TotalCost = reader.Field<decimal>("TotalCost");

                        costs.Add(cost);
                    }
                }
            }

            return costs;
        }

        public static async Task<List<dynamic>> GetTotalArtistDiscsAsync()
        {
            var discs = new List<dynamic>();

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            using (var cmd = new SqlCommand("sp_getTotalDiscsForEachArtist", cn) { CommandType = CommandType.StoredProcedure })
            {
                await cn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        dynamic disc = new ExpandoObject();

                        disc.ArtistId = reader.Field<int>("ArtistId");
                        disc.Name = reader.Field<string>("Name");
                        disc.TotalDiscs = reader.Field<int>("TotalDiscs");

                        discs.Add(disc);
                    }
                }
            }

            return discs;
        }
    }
}
