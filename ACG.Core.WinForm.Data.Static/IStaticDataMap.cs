
namespace ACG.Core.WinForm.Data.Static
{
    public interface IStaticDataMap
    {
        void Add(string key, object obj);

        object Get(string key);

        void Remove(string key);

        bool Contains(string key);

        void Save(string key, object obj);
    }
}
