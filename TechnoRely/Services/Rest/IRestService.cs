using System.Net.Http;
using System.Threading.Tasks;
using TechnoRely.Models;

namespace TechnoRely.Services
{
    public interface IRestService
    {
        Task<ArtistsModel> GetArtistsAsync(string country);
        Task<AlbumsModel> GetAlbumsAsync(string artist);
    }
}