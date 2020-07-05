using System;
using System.Drawing;
using Newtonsoft.Json;

namespace TechnoRely.Models
{
    public class ArtistsModel
    {
        [JsonProperty("topartists")]
        public TopArtists TopArtists { get; set; }
    }
    
    public class TopArtists
    {
        [JsonProperty("artist")]
        public ArtistModel[] Artist { get; set; }
    }

    public class ArtistModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("listeners")]
        public long Listeners { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }


        [JsonProperty("image")]
        public Image[] Image { get; set; }
    }

    public class Image
    {
        [JsonProperty("#text")]
        public Uri Text { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }
    }
}