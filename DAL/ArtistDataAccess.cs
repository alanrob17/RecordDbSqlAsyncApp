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

        /// <summary>
        /// Get the artist id.
        /// </summary>
        /// <param name="recordId">The record Id.</param>
        /// <returns>The <see cref="int"/> artist Id.</returns>
        public int GetArtistId(int recordId)
        {
            var artistId = -1;

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                var cmd = new SqlCommand("up_getArtistIdFromRecordId", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.AddWithValue("RecordId", recordId);

                using (cn)
                {
                    cn.Open();

                    artistId = (int)cmd.ExecuteScalar();
                }
            }

            return artistId;
        }

        /// <summary>
        /// Delete an existing Artist record.
        /// </summary>
        /// <param name="artistId">The artist Id.</param>
        public void Delete(int artistId)
        {
            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                var cmd = new SqlCommand("up_deleteArtist", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.AddWithValue("@ArtistId", artistId);

                using (cn)
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Get a biography.
        /// </summary>
        /// <param name="recordId">The record Id.</param>
        /// <returns>
        /// The <see cref="object"/>biography.</returns>
        public string GetBiography(int recordId)
        {
            var biography = string.Empty;

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                var cmd = new SqlCommand("up_getBiography", cn) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.AddWithValue("recordId", recordId);

                using (cn)
                {
                    cn.Open();

                    biography = (string)cmd.ExecuteScalar();
                }
            }

            // add some spacing
            return biography;
        }

        /// <summary>
        /// ToString method shows an instances properties
        /// </summary>
        /// <param name="artist">The artist.</param>
        /// <returns>The <see cref="string"/> artist record as a string.</returns>
        public static string ToString(Artist artist)
        {
            var artistDetails = new StringBuilder();

            artistDetails.Append("<strong>ArtistId: </strong>" + artist.ArtistId + "<br/>");

            if (!string.IsNullOrEmpty(artist.FirstName))
            {
                artistDetails.Append("<strong>First Name: </strong>" + artist.FirstName + "<br/>");
            }

            artistDetails.Append("<strong>Last Name: </strong>" + artist.LastName + "<br/>");

            if (!string.IsNullOrEmpty(artist.Name))
            {
                artistDetails.Append("<strong>Name: </strong>" + artist.Name + "<br/>");
            }

            if (!string.IsNullOrEmpty(artist.Biography))
            {
                artistDetails.Append("<strong>Biography: </strong>" + artist.Biography + "<br/>");
            }

            return artistDetails.ToString();
        }

        /// <summary>
        /// Get artist object by record id.
        /// </summary>
        /// <param name="recordId">The record id.</param>
        /// <returns>The <see cref="Artist"/> artist object.</returns>
        public Artist GetArtistByRecordId(int recordId)
        {
            var dt = new DataTable();

            using (var cn = new SqlConnection(_ap.Instance.ConnectionString))
            {
                var sql = "up_ArtistSelectByRecordId";
                var cmd = new SqlCommand(sql, cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@RecordId", recordId);

                var da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            var entity =
              (from dr in dt.AsEnumerable()
               select new Artist
               {
                   ArtistId = Convert.ToInt32(dr["ArtistId"]),
                   FirstName = dr["FirstName"].ToString(),
                   LastName = dr["LastName"].ToString(),
                   Name = dr["Name"].ToString(),
                   Biography = dr["Biography"].ToString()
               }).FirstOrDefault();

            return entity;
        }


        #endregion
    }
}
