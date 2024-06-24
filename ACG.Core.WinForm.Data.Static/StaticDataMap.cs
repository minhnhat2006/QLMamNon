using System;
using System.Collections.Generic;

namespace ACG.Core.WinForm.Data.Static
{
    public class StaticDataMap : IStaticDataMap
    {
        #region Properties

        private IDictionary<string, object> keyDataMap = new Dictionary<string, object>();

        private IDictionary<string, IStaticData> keyFunctionMap = new Dictionary<string, IStaticData>();

        #endregion

        #region IStaticData Members

        public void Add(string key, object obj)
        {
            if (obj is IStaticData)
            {
                this.addFunction(key, (IStaticData)obj);
            }
            else
            {
                if (this.keyDataMap.ContainsKey(key))
                {
                    throw new Exception(String.Format("Key '{0}' already existed", key));
                }

                this.keyDataMap.Add(key, obj);
            }
        }

        private void addFunction(string key, IStaticData function)
        {
            if (this.keyFunctionMap.ContainsKey(key))
            {
                throw new Exception(String.Format("Key '{0}' already existed", key));
            }

            this.keyFunctionMap.Add(key, function);
        }

        public void Save(string key, object obj)
        {
            if (this.Contains(key))
            {
                this.Remove(key);
            }

            this.Add(key, obj);
        }

        public void Remove(string key)
        {
            if (this.keyFunctionMap.ContainsKey(key))
            {
                this.keyFunctionMap.Remove(key);
            }

            if (this.keyDataMap.ContainsKey(key))
            {
                this.keyDataMap.Remove(key);
            }
        }

        public void Reload(string keyFunction)
        {
            if (this.keyFunctionMap.ContainsKey(keyFunction))
            {
                this.keyDataMap.Remove(keyFunction);
            }
        }

        public object Get(string key)
        {
            if (!keyDataMap.ContainsKey(key) || keyDataMap[key] == null)
            {
                if (keyFunctionMap.ContainsKey(key))
                {
                    IStaticData staticData = keyFunctionMap[key];
                    object data = staticData.Retrieve();
                    this.keyDataMap.Add(key, data);
                }
            }

            return this.keyDataMap[key];
        }

        public bool Contains(string key)
        {
            return keyFunctionMap.ContainsKey(key) || keyDataMap.ContainsKey(key);
        }

        #endregion
    }
}
