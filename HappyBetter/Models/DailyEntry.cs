﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight;

namespace HappyBetter.Models
{
    public class DailyEntry : ViewModelBase
    {
        public DailyEntry()
        {
            GratitudeEntriesList = new ObservableCollection<GratitudeEntry>();
            for (var i = 0; i < 3; i++)
            {
                GratitudeEntriesList.Add(new GratitudeEntry());
            }
        }

        public DailyEntry(DateTime entryDateTimeKey)
            : this()
        {
            LastEditedDate = DateTime.Now;
            EntryDateTimeKey = entryDateTimeKey;
        }

        public DateTime EntryDateTimeKey { get; set; }
        public DateTime LastEditedDate { get; set; }

        private ObservableCollection<GratitudeEntry> _gratitudeEntriesList;
        public ObservableCollection<GratitudeEntry> GratitudeEntriesList
        {
            get { return _gratitudeEntriesList; }
            set
            {
                _gratitudeEntriesList = value;
                RaisePropertyChanged("GratitudeEntriesList");
            }
        }

        private String _journalEntry;
        public String JournalEntry
        {
            get
            {
                return _journalEntry;
            }

            set
            {
                _journalEntry = value;
                RaisePropertyChanged("JournalEntry");
            }
        }

        private String _exerciseEntry;
        public String ExerciseEntry
        {
            get
            {
                return _exerciseEntry;
            }

            set
            {
                _exerciseEntry = value;
                RaisePropertyChanged("ExerciseEntry");
            }
        }

        private String _meditationEntry;
        public String MeditationEntry
        {
            get
            {
                return _meditationEntry;
            }

            set
            {
                _meditationEntry = value;
                RaisePropertyChanged("MeditationEntry");
            }
        }

        private String _actOfKindnessEntry;
        public String ActOfKindnessEntry
        {
            get
            {
                return _actOfKindnessEntry;
            }

            set
            {
                _actOfKindnessEntry = value;
                RaisePropertyChanged("ActOfKindnessEntry");
            }
        }

        public Boolean IsCompleted 
        {
            get
            {
                return IsDone(GratitudeEntriesList) && IsDone(JournalEntry) && IsDone(ExerciseEntry) && IsDone(MeditationEntry) &&
                       IsDone(ActOfKindnessEntry);
            }
        }

        public Boolean IsDone(ObservableCollection<GratitudeEntry> entries)
        {
            return GratitudeEntriesList.Count >= 3 &&
                                      GratitudeEntriesList.Count(x => IsDone(x.Description)) >= 3;
        }

        public Boolean IsDone(String item)
        {
            return !String.IsNullOrWhiteSpace(item);
        }

        public DailyEntry Clone()
        {
            var dailyEntry = new DailyEntry()
            {
                ActOfKindnessEntry = String.Copy(this.ActOfKindnessEntry ?? String.Empty),
                EntryDateTimeKey = this.EntryDateTimeKey,
                ExerciseEntry = String.Copy(this.ExerciseEntry ?? String.Empty),
                GratitudeEntriesList = new ObservableCollection<GratitudeEntry>(this.GratitudeEntriesList ?? new Collection<GratitudeEntry>()),
                JournalEntry = String.Copy(this.JournalEntry ?? String.Empty),
                LastEditedDate = this.LastEditedDate,
                MeditationEntry = String.Copy(this.MeditationEntry ?? String.Empty)
            };
            dailyEntry.GratitudeEntriesList = new ObservableCollection<GratitudeEntry>();
            if (this.GratitudeEntriesList != null)
            {
                foreach (var gratitudeEntry in this.GratitudeEntriesList)
                {
                    dailyEntry.GratitudeEntriesList.Add(new GratitudeEntry()
                    {
                        Description = String.Copy(gratitudeEntry.Description ?? String.Empty)
                    });
                }
            }
            return dailyEntry;
        }
    }

    public class GratitudeEntry : ObservableObject
    {
        private String _description;
        public String Description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
                RaisePropertyChanged("Description");
            }
        }
    }
}
