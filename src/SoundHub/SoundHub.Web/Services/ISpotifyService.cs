using System.Threading.Tasks;
using SoundHub.Web.Services;

namespace SoundHub.Web.Services
{
    public interface ISpotifyService
    {
        Task<SpotifyArtist?> SearchArtistAsync(string name);
    }
}

