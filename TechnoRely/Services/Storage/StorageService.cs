using System;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace TechnoRely.Services.Storage
{
    public class StorageService : IStorageService
    {
        #region Public methods

        public T Get<T>(string key)
        {
            try
            {
                var jsonData = Preferences.Get(key, null);
                if (jsonData == null)
                    return default(T);
            
                var data = JsonConvert.DeserializeObject<T>(jsonData);
                return data;
            }
            catch (Exception e)
            {
                return default;
            }
        }

        public void Set(string key, object data)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(data);
                Preferences.Set(key, jsonData);
            }
            catch (Exception e)
            {
            }
        }

        #endregion
    }
}