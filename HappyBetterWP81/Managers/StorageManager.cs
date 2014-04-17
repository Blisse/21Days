using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using HappyBetterWP81.Common;

namespace HappyBetterWP81.Managers
{
    public class StorageManager
    {
        private readonly ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);

        private ApplicationDataContainer LocalSettings
        {
            get { return _localSettings; }
        }

        public void AddOrUpdateValue<T>(string key, T data)
        {
            _semaphoreSlim.Wait();

            try
            {
                if (LocalSettings.Values.ContainsKey(key))
                {
                    LocalSettings.Values[key] = JsonUtility.SerializeObject(data);
                }
                else
                {
                    LocalSettings.Values.Add(key, JsonUtility.SerializeObject(data));
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

        public T GetValueOrDefault<T>(string key, T other)
        {
            _semaphoreSlim.Wait();

            try
            {
                object data;
                bool gotData = LocalSettings.Values.TryGetValue(key, out data);

                if (gotData)
                {
                    other = JsonUtility.DeserializeObject<T>((string)data);
                }
            }
            finally
            {
                _semaphoreSlim.Release();
            }

            return other;
        }
    }
}
