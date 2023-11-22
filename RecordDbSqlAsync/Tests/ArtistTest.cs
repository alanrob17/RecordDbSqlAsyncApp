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

            await Console.Out.WriteLineAsync($"Id: {artist.ArtistId} - {artist.FirstName} {artist.LastName} --\n {artist.Biography}\n");
        }

        internal static async Task GetArtistsWithNoBioAsync()
        {
            var artists = await _ad.GetArtistsWithNoBioAsync();

            foreach (var artist in artists)
            {
                await Console.Out.WriteLineAsync($"Id: {artist.ArtistId} - {artist.Name}");
            }
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
    }
}
