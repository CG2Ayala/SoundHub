using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SoundHub.Web.Services
{
    public class SpotifyArtist
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class SpotifyService : ISpotifyService
    {
        private readonly HttpClient _http;

        public SpotifyService(HttpClient http)
        {
            _http = http;
        }

        // Simple stub that returns a mock artist (replace with real OAuth + API later)
        public async Task<SpotifyArtist?> SearchArtistAsync(string name)
        {
            await Task.Delay(150); // simulate latency
            if (string.IsNullOrWhiteSpace(name)) return null;
            return new SpotifyArtist { Id = $"mock-{name.Replace(" ", "-").ToLower()}", Name = name };
        }
    }
}

