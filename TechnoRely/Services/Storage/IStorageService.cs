namespace TechnoRely.Services.Storage
{
    public interface IStorageService
    {
        T Get<T>(string key);
        void Set(string key, object data);
    }
}