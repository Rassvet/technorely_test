namespace TechnoRely
{
    public class Constants
    {
        public static string ApiKey = "e81f61890b7ff8633ca024d0faa449e7";
        
        #region NavigationKeys

        public static string DetailArtistNavKey = "registrationCompanyModel";
        public static string IsOfflineNavKey = "offline";

        #endregion

        #region Urls

        public static string TopArtists = "http://ws.audioscrobbler.com/2.0/?method=geo.gettopartists&country={country}&api_key={apiKey}&format=json";

        public static string TopAlbums = "http://ws.audioscrobbler.com/2.0/?method=artist.gettopalbums&artist={artist}&api_key={apiKey}&format=json";

        #endregion
    }
}