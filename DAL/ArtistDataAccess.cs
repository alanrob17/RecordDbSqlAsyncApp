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
    public class ArtistDataAccess
    {
        #region " Methods "        

        public static async Task<List<Artist>> GetArtistsAsync()
        {
            var artists = new List<Artist>();

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            using (var cmd = new SqlCommand("up_ArtistSelectAll", cn) { CommandType = CommandType.StoredProcedure })
            {
                await cn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var artist = new Artist
                        {
                            ArtistId = reader.Field<int>("ArtistId"),
                            FirstName = reader.Field<string>("FirstName"),
                            LastName = reader.Field<string>("LastName"),
                            Name = reader.Field<string>("Name"),
                            Biography = reader.Field<string>("Biography")
                        };

                        artists.Add(artist);
                    }
                }
            }

            return artists;
        }

        public static async Task<List<Artist>> GetArtistListAsync()
        {
            var artists = new List<Artist>();

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            using (var cmd = new SqlCommand("up_getArtistListandNone", cn) { CommandType = CommandType.StoredProcedure })
            {
                await cn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var artist = new Artist
                        {
                            ArtistId = reader.Field<int>("ArtistId"),
                            Name = reader.Field<string>("Name")
                        };

                        artists.Add(artist);
                    }
                }
            }

            return artists;
        }

        public static async Task<Artist> GetArtistByIdAsync(int artistId)
        {
            using (var connection = new SqlConnection(_ap.Instance.ConnectionString))
            {
                await connection.OpenAsync();

                var sql = "up_ArtistSelectById";
                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ArtistId", artistId);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Artist
                            {
                                ArtistId = reader.Field<int>("ArtistId"),
                                FirstName = reader.Field<string>("FirstName"),
                                LastName = reader.Field<string>("LastName"),
                                Name = reader.Field<string>("Name"),
                                Biography = reader.Field<string>("Biography")
                            };
                        }
                    }
                }
            }

            return null;
        }

        public static async Task<List<Artist>> GetArtistsWithNoBioAsync()
        {
            var artists = new List<Artist>();

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            using (var cmd = new SqlCommand("up_selectArtistsWithNoBio", cn) { CommandType = CommandType.StoredProcedure })
            {
                await cn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var artist = new Artist
                        {

                            ArtistId = reader.Field<int>("ArtistId"),
                            Name = reader.Field<string>("Name"),
                        };

                        artists.Add(artist);
                    }
                }
            }

            return artists;
        }

        public static async Task<int> AddArtistAsync(Artist artist)
        {
            var artistId = -1; // 0 is used for a record that is already in the db.

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                var cmd = new SqlCommand("adm_ArtistInsert", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.AddWithValue("Artistid", artist.ArtistId);
                cmd.Parameters.AddWithValue("FirstName", artist.FirstName);
                cmd.Parameters.AddWithValue("LastName", artist.LastName);
                cmd.Parameters.AddWithValue("Biography", artist.Biography);
                var parArtistId = new SqlParameter("@artistId", SqlDbType.Int, 4)
                {
                    Direction = ParameterDirection.ReturnValue
                };
                cmd.Parameters.Add(parArtistId);

                using (cn)
                {
                    await cn.OpenAsync(); 
                    await cmd.ExecuteNonQueryAsync();
                    artistId = (int)parArtistId.Value;
                }
            }

            return artistId;
        }

        public static async Task<int> AddArtistAsync(string firstName, string lastName, string biography)
        {
            var artistId = -1; // 0 is used for record is already in the db.

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                var cmd = new SqlCommand("adm_ArtistInsert", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.AddWithValue("Artistid", 0);
                cmd.Parameters.AddWithValue("FirstName", firstName);
                cmd.Parameters.AddWithValue("LastName", lastName);
                cmd.Parameters.AddWithValue("Biography", biography);
                var parArtistId = new SqlParameter("@artistId", SqlDbType.Int, 4)
                {
                    Direction = ParameterDirection.ReturnValue
                };
                cmd.Parameters.Add(parArtistId);

                using (cn)
                {
                    await cn.OpenAsync(); 
                    await cmd.ExecuteNonQueryAsync();
                    artistId = (int)parArtistId.Value;
                }
            }

            return artistId;
        }

        public static async Task<int> UpdateArtistAsync(int artistId, string firstName, string lastName, string name, string biography)
        {
            var newArtistId = -1;

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                var cmd = new SqlCommand("up_UpdateArtist", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.AddWithValue("Artistid", artistId);
                cmd.Parameters.AddWithValue("FirstName", firstName);
                cmd.Parameters.AddWithValue("LastName", lastName);
                cmd.Parameters.AddWithValue("Name", name);
                cmd.Parameters.AddWithValue("Biography", biography);
                var parArtistId = new SqlParameter("@artistId", SqlDbType.Int, 4)
                {
                    Direction = ParameterDirection.ReturnValue
                };
                cmd.Parameters.Add(parArtistId);

                using (cn)
                {
                    await cn.OpenAsync(); 
                    await cmd.ExecuteNonQueryAsync();
                    newArtistId = (int)parArtistId.Value;
                }
            }

            return newArtistId;
        }

        public static async Task<int> UpdateArtist(Artist artist)
        {
            var artistId = -1;

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                var cmd = new SqlCommand("up_UpdateArtist", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.AddWithValue("Artistid", artist.ArtistId);
                cmd.Parameters.AddWithValue("FirstName", artist.FirstName);
                cmd.Parameters.AddWithValue("LastName", artist.LastName);
                cmd.Parameters.AddWithValue("Name", artist.Name);
                cmd.Parameters.AddWithValue("Biography", artist.Biography);
                var parArtistId = new SqlParameter("@artistId", SqlDbType.Int, 4)
                {
                    Direction = ParameterDirection.ReturnValue
                };
                cmd.Parameters.Add(parArtistId);

                using (cn)
                {
                    await cn.OpenAsync(); 
                    await cmd.ExecuteNonQueryAsync();
                    artistId = (int)parArtistId.Value;
                }
            }

            return artistId;
        }

        public static async Task<int> GetArtistIdAsync(string firstName, string lastName)
        {
            var artistId = -1; // 0 is used for a record that is already in the db.

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                var cmd = new SqlCommand("up_getArtistIdByName", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                var parArtistId = new SqlParameter("@artistId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(parArtistId);

                await cn.OpenAsync();

                try
                {
                    await cmd.ExecuteNonQueryAsync();
                    artistId = (int)parArtistId.Value;
                }
                catch (Exception)
                {
                    artistId = 0;
                }
            }

            return artistId;
        }

        public static async Task<int> GetArtistIdAsync(int recordId)
        {
            var artistId = -1;

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                var cmd = new SqlCommand("up_getArtistIdFromRecord", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.AddWithValue("@RecordId", recordId);
                var parArtistId = new SqlParameter("@artistId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(parArtistId);

                await cn.OpenAsync();

                try
                {
                    await cmd.ExecuteNonQueryAsync();
                    artistId = (int)parArtistId.Value;
                }
                catch (Exception)
                {
                    artistId = 0;
                }
            }

            return artistId;
        }


        public static async Task<Artist> GetArtistByNameAsync(string name)
        {
            using (var connection = new SqlConnection(_ap.Instance.ConnectionString))
            {
                await connection.OpenAsync();

                var sql = "up_GetFullArtistByName";
                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", name);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Artist
                            {
                                ArtistId = reader.Field<int>("ArtistId"),
                                FirstName = reader.Field<string>("FirstName"),
                                LastName = reader.Field<string>("LastName"),
                                Name = reader.Field<string>("Name"),
                                Biography = reader.Field<string>("Biography")
                            };
                        }
                    }
                }
            }

            return null;
        }



        public static async Task DeleteAsync(int artistId)
        {
            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                var cmd = new SqlCommand("up_deleteArtist", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.AddWithValue("@ArtistId", artistId);

                await cn.OpenAsync();

                try
                {
                    await cmd.ExecuteNonQueryAsync();
                }
                catch (Exception)
                {
                    artistId = 0;
                }
            }
        }

        public static async Task<string> GetBiographyAsync(int recordId)
        {
            var biography = string.Empty;

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                var cmd = new SqlCommand("up_getBiography", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.AddWithValue("recordId", recordId);

                await cn.OpenAsync();

                biography = (string)await cmd.ExecuteScalarAsync();
            }

            return biography;
        }

        public static async Task<Artist> GetArtistByRecordIdAsync(int recordId)
        {
            using (var connection = new SqlConnection(_ap.Instance.ConnectionString))
            {
                await connection.OpenAsync();

                var sql = "up_ArtistSelectByRecordId";
                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RecordId", recordId);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Artist
                            {
                                ArtistId = reader.Field<int>("ArtistId"),
                                FirstName = reader.Field<string>("FirstName"),
                                LastName = reader.Field<string>("LastName"),
                                Name = reader.Field<string>("Name"),
                                Biography = reader.Field<string>("Biography")
                            };
                        }
                    }
                }
            }

            return null;
        }

        public static async Task<int> GetNoBiographyCountAsync()
        {
            var count = 0;

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                var cmd = new SqlCommand("up_NoBiographyCount", cn) { CommandType = CommandType.StoredProcedure };

                var parCount = new SqlParameter("@Count", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(parCount);

                await cn.OpenAsync();

                try
                {
                    await cmd.ExecuteNonQueryAsync();
                    count = (int)parCount.Value;
                }
                catch (Exception)
                {
                    count = 0;
                }
            }

            return count;
        }

        #endregion
    }
}
