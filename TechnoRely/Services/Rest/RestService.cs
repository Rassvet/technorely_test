using System;
using System.Net.Http;
using System.Threading.Tasks;
using TechnoRely.Models;

namespace TechnoRely.Services
{
    public class RestService : IRestService
    {
        #region Public methods

        public async Task<ArtistsModel> GetArtistsAsync(string country)
        {
            try
            {
                var url = Constants.TopArtists.Replace("{apiKey}", Constants.ApiKey);
                
                var httpClient = new HttpClient();
                var request = url.Replace("{country}", country);
                var result = await httpClient.GetAsync(new Uri(request));

                if (!result.IsSuccessStatusCode)
                    return null;

                var modelJson = await result.Content.ReadAsStringAsync();
                var countryList = Newtonsoft.Json.JsonConvert.DeserializeObject<ArtistsModel>(modelJson);

                return countryList;

            }
            catch (Exception e)
            {
                return null;
            }

        }
        
        public async Task<AlbumsModel> GetAlbumsAsync(string artist)
        {
            try
            {
                var url = Constants.TopAlbums.Replace("{apiKey}", Constants.ApiKey);
                
                var httpClient = new HttpClient();
                var request = url.Replace("{artist}", artist);
                var result = await httpClient.GetAsync(new Uri(request));

                if (!result.IsSuccessStatusCode)
                    return null;

                var modelJson = await result.Content.ReadAsStringAsync();
                var countryList = Newtonsoft.Json.JsonConvert.DeserializeObject<AlbumsModel>(modelJson);

                return countryList;

            }
            catch (Exception e)
            {
                return null;
            }

        }

        #endregion
    }
}