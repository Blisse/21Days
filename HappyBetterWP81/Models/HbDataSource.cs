using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HappyBetterWP81.Managers;

namespace HappyBetterWP81.Models
{
    public class HbDataSource
    {
        private readonly StorageManager _storageManager = new StorageManager();

        public bool InsertDayEntry(DayEntry entry)
        {
            var dayEntries = GetDayEntries();
            if (!dayEntries.Select(x => x.EntryDay).Contains(entry.EntryDay))
            {
                dayEntries.Add(entry);

                SaveDayEntries(dayEntries);
                return true;
            }
            return false;
        }

        public bool DeleteDayEntry(DateTime day)
        {
            var dayEntries = GetDayEntries();
            if (dayEntries.Select(x => x.EntryDay).Contains(day))
            {
                var entry = dayEntries.Single(x => x.EntryDay == day);
                dayEntries.Remove(entry);

                SaveDayEntries(dayEntries);
                return true;
            }
            return false;
        }

        public bool UpdateDayEntry(DayEntry entry)
        {
            var dayEntries = GetDayEntries();
            if (dayEntries.Select(x => x.EntryDay).Contains(entry.EntryDay))
            {
                var day = dayEntries.Single(x => x.EntryDay == entry.EntryDay);
                dayEntries.Remove(day);
                dayEntries.Add(entry);
                
                SaveDayEntries(dayEntries);
                return true;
            }
            return false;
        }

        public List<DayEntry> GetDayEntries()
        {
            var entries = _storageManager.GetValueOrDefault(StorageKeys.DailyEntriesDataStorageKey,
                new List<DayEntry>());
            return entries;
        }

        public List<DayEntry> GetDayEntriesSorted()
        {
            var entries = GetDayEntries().OrderBy(o => o.EntryDay).ToList();
            return entries;
        } 

        public void SaveDayEntries(List<DayEntry> dayEntries)
        {
            _storageManager.AddOrUpdateValue(StorageKeys.DailyEntriesDataStorageKey, dayEntries);
        }
    }
}
