using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;

namespace HappyBetterWP81.Common
{
    public class StorageUtility
    {
        private readonly ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);

        private ApplicationDataContainer LocalSettings
        {
            get { return _localSettings; }
        }

        public void AddOrUpdateKey<T>(string key, T data)
        {
            _semaphoreSlim.Wait();

            try
            {
                if (LocalSettings.Values.ContainsKey(key))
                {
                    LocalSettings.Values[key] = data;
                }
                else
                {
                    LocalSettings.Values.Add(key, data);
                }
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public void RemoveKey(string key)
        {
            _semaphoreSlim.Wait();

            try
            {
                if (LocalSettings.Values.ContainsKey(key))
                {
                    LocalSettings.Values.Remove(key);
                }
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public bool TryGetData<T>(string key, out T value)
        {
            _semaphoreSlim.Wait();
            object data;
            bool gotData;

            try
            {
                gotData = LocalSettings.Values.TryGetValue(key, out data);
            }
            finally
            {
                _semaphoreSlim.Release();
            }

            if (gotData)
            {
                value = (T) data;
                return true;
            }

            value = default(T);
            return false;
        }
    }
}
