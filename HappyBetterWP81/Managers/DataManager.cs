using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HappyBetterWP81;
using HappyBetterWP81.Models;

namespace HappyBetterWP81.Managers
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
                App.StorageManager.RemoveKey(StorageKeys.DailyEntriesDataStorageKey);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
