using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _ad = DAL.ArtistDataAccess;

namespace RecordDbSqlAsync.Tests
{
    internal class ArtistTest
    {
        internal static async Task GetArtistsAsync()
        {
            var artists = await _ad.GetArtistsAsync();

            foreach (var artist in artists)
            {
                var bio = string.IsNullOrEmpty(artist.Biography) ? "No Biography" : (artist.Biography.Length > 30 ? artist.Biography.Substring(0, 30) + "..." : artist.Biography);

                await Console.Out.WriteLineAsync($"Id: {artist.ArtistId} - {artist.Name} -- {bio}\n");
            }
        }

        internal static async Task GetArtistListAsync()
        {
            var artists = await _ad.GetArtistListAsync();

            foreach (var artist in artists)
            {
                await Console.Out.WriteLineAsync($"Id: {artist.ArtistId} - {artist.Name}");
            }
        }

        internal static async Task GetArtistByIdAsync(int artistId)
        {
            var artist = await _ad.GetArtistByIdAsync(artistId);

            string artistData = ArtistTest.ToString(artist);

            await Console.Out.WriteLineAsync(artistData);
        }

        internal static async Task GetArtistByRecordIdAsync(int recordId)
        {
            var artist = await _ad.GetArtistByRecordIdAsync(recordId);

            string artistData = ArtistTest.ToString(artist);

            await Console.Out.WriteLineAsync(artistData);
        }


        internal static async Task GetArtistByNameAsync(string name)
        {
            var artist = await _ad.GetArtistByNameAsync(name);

            string artistData = ArtistTest.ToString(artist);

            await Console.Out.WriteLineAsync(artistData);
        }

        internal static async Task GetArtistsWithNoBioAsync()
        {
            var artists = await _ad.GetArtistsWithNoBioAsync();

            foreach (var artist in artists)
            {
                await Console.Out.WriteLineAsync($"Id: {artist.ArtistId} - {artist.Name}");
            }
        }


        internal static async Task GetNoBiographyCountAsync()
        {
            int count = await _ad.GetNoBiographyCountAsync();

            await Console.Out.WriteLineAsync($"Number of missing biographies: {count}.");
        }

        internal static async Task AddArtistAsync(Artist artist)
        {
            var artistId = await _ad.AddArtistAsync(artist);

            if (artistId > 0)
            {
                await Console.Out.WriteLineAsync($"New ArtistId: {artistId}");
            }
            else 
            {
                await Console.Out.WriteLineAsync($"Artist already exists in the database!");
            }

        }

        internal static async Task AddArtistAsync(string firstName, string lastName, string biography)
        {
            var artistId = await _ad.AddArtistAsync(firstName, lastName, biography);

            if (artistId > 0)
            {
                await Console.Out.WriteLineAsync($"New ArtistId: {artistId}");
            }
            else
            {
                await Console.Out.WriteLineAsync($"Artist already exists in the database!");
            }
        }

        internal static async Task UpdateArtistAsync(int artistId, string firstName, string lastName, string name, string biography)
        {
            var newArtistId = await _ad.UpdateArtistAsync(artistId, firstName, lastName, name, biography);

            if (newArtistId > 0)
            {
                await Console.Out.WriteLineAsync($"New ArtistId: {newArtistId}");
            }
            else
            {
                await Console.Out.WriteLineAsync($"Artist wasn't updated!");
            }
        }

        internal static async Task UpdateArtistAsync(Artist artist)
        {
            var newArtistId = await _ad.UpdateArtistAsync(artist.ArtistId, artist.FirstName, artist.LastName, artist.Name, artist.Biography);

            if (newArtistId > 0)
            {
                await Console.Out.WriteLineAsync($"New ArtistId: {newArtistId}");
            }
            else
            {
                await Console.Out.WriteLineAsync($"Artist wasn't updated!");
            }
        }

        internal static async Task GetArtistIdAsync(string firstName, string lastName)
        {
            var artistId = await _ad.GetArtistIdAsync(firstName, lastName);

            if (artistId > 0)
            {
                await Console.Out.WriteLineAsync($"Artist Id for {firstName} {lastName}: {artistId}");
            }
            else
            {
                await Console.Out.WriteLineAsync($"Artist Id couldn't be found!");
            }
        }

        internal static async Task GetArtistIdAsync(int recordId)
        {
            var artistId = await _ad.GetArtistIdAsync(recordId);

            if (artistId > 0)
            {
                await Console.Out.WriteLineAsync($"Artist Id from Record Id: {recordId} is {artistId}");
            }
            else
            {
                await Console.Out.WriteLineAsync($"Artist Id couldn't be found!");
            }
        }

        internal static async Task DeleteAsync(int artistId)
        {
            await _ad.DeleteAsync(artistId);

            var artist = await _ad.GetArtistByIdAsync(artistId);
        
            if (artist != null)
            {
                await Console.Out.WriteLineAsync("Artist not deleted!");
            }
            else 
            {
                await Console.Out.WriteLineAsync("Artist deleted.");
            }
        }

        internal static async Task GetBiographyAsync(int recordId)
        {
            var biography = await _ad.GetBiographyAsync(recordId);

            if (biography != null)
            {
                await Console.Out.WriteLineAsync($"Biography:\n\n{biography}");
            }
            else
            {
                await Console.Out.WriteLineAsync($"Biography doesn't exist!");
            }
        }

        internal static async Task ArtistHtmlAsync(int artistId)
        {
            var artist = await _ad.GetArtistByIdAsync(artistId);
            var message = artist.ArtistId > 0 ? $"<p><strong>Id:</strong> {artist.ArtistId}</p>\n<p><strong>Name:</strong> {artist.FirstName} {artist.LastName}</p>\n<p><strong>Biography:</strong></p>\n<div>{artist.Biography}</p></div>" : "ERROR: Artist not found!";

            Console.WriteLine(message);
        }

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
    }
}
