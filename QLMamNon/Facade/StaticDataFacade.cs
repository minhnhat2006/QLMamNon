using ACG.Core.WinForm.Data.Static;
using QLMamNon.Dao;

namespace QLMamNon.Facade
{
    public class StaticDataFacade
    {
        private static IStaticDataMap staticDataMap = new StaticDataMap();

        public static void Add(string key, object staticData)
        {
            staticDataMap.Add(key, staticData);
        }

        public static void Save(string key, object staticData)
        {
            staticDataMap.Save(key, staticData);
        }

        public static void Remove(string key)
        {
            staticDataMap.Remove(key);
        }

        public static void Reload(string key)
        {
            ((StaticDataMap)staticDataMap).Reload(key);
        }

        public static qlmamnonEntities GetQLMNEntities()
        {
            qlmamnonEntities entities = new qlmamnonEntities();
            return entities;
        }

        public static object Get(string key)
        {
            if (staticDataMap != null)
            {
                return staticDataMap.Get(key);
            }

            return null;
        }

        public static bool Contains(string key)
        {
            if (staticDataMap != null)
            {
                return staticDataMap.Contains(key);
            }

            return false;
        }
    }
}
