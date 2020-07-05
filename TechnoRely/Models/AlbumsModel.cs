using Newtonsoft.Json;

namespace TechnoRely.Models
{
    public class AlbumsModel
    {
        [JsonProperty("topalbums")]
        public TopAlbums Topalbums { get; set; }
    }
    public class TopAlbums
    {
        [JsonProperty("album")]
        public AlbumModel[] Album { get; set; }
    }

    public class AlbumModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("playcount")]
        public long PlayCount { get; set; }

    }

}