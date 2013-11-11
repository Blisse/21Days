using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HappyBetter.Models
{
    public class DataManager
    {
        private static DataManager _instance;

        private DataManager()
        {
        }

        public static DataManager Instance
        {
            get { return _instance ?? (_instance = new DataManager()); }
        }

        public Boolean SaveDailyEntriesData(Dictionary<DateTime, DailyEntry> data)
        {
            try
            {
                App.StorageManager.AddOrUpdateValue(StorageKeys.DailyEntriesDataStorageKey, data);
                App.StorageManager.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Dictionary<DateTime, DailyEntry> GetDailyEntriesData()
        {
            return App.StorageManager.GetValueOrDefault(StorageKeys.DailyEntriesDataStorageKey, new Dictionary<DateTime, DailyEntry>());
        }

        public Boolean ClearDailyEntriesData()
        {
            try
            {
                App.StorageManager.DeleteKey(StorageKeys.DailyEntriesDataStorageKey);
                App.StorageManager.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
