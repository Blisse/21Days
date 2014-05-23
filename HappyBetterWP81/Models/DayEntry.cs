using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace HappyBetterWP81.Models
{
    public class DayEntry : ObservableObject
    {
        public DayEntry()
        {

        }

        private String _journal;

        public String Journal
        {
            get { return _journal; }
            set
            {
                if (_journal == value)
                {
                    return;
                }
                _journal = value;
                RaisePropertyChanged();
            }
        }

        private String _meditation;

        public String Meditation
        {
            get { return _meditation; }
            set
            {
                if (_meditation == value)
                {
                    return;
                }
                _meditation = value;
                RaisePropertyChanged();
            }
        }

        private String _exercise;

        public String Exercise
        {
            get { return _exercise; }
            set
            {
                if (_exercise == value)
                {
                    return;
                }
                _exercise = value;
                RaisePropertyChanged();
            }
        }

        private String _raok;

        public String Raok
        {
            get { return _raok; }
            set
            {
                if (_raok == value)
                {
                    return;
                }
                _raok = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Gratitude> _gratitudes;

        public ObservableCollection<Gratitude> Gratitudes
        {
            get { return _gratitudes; }
            set
            {
                if (_gratitudes == value)
                {
                    return;
                }
                _gratitudes = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _entryDay;

        public DateTime EntryDay
        {
            get { return _entryDay; }
            set
            {
                if (_entryDay == value)
                {
                    return;
                }
                _entryDay = value;
                RaisePropertyChanged();
            }
        }

        public bool IsDone()
        {
            if (Gratitudes.Count >= MinimumGratitudeLength && !Gratitudes.All(x => String.IsNullOrWhiteSpace(x.Description)) &&
                Exercise.Length >= MinimumEntryLength && !String.IsNullOrWhiteSpace(Exercise) &&
                Meditation.Length >= MinimumEntryLength && !String.IsNullOrWhiteSpace(Meditation) &&
                Raok.Length >= MinimumEntryLength && !String.IsNullOrWhiteSpace(Raok) &&
                Journal.Length >= MinimumEntryLength && !String.IsNullOrWhiteSpace(Journal))
            {
                return true;
            }
            return false;
        }

        private const int MinimumEntryLength = 5;
        private const int MinimumGratitudeLength = 3;
    }

    public class Gratitude : ObservableObject
    {
        private String _description;

        public String Description
        {
            get { return _description; }
            set
            {
                if (_description == value)
                {
                    return;
                }
                _description = value;
                RaisePropertyChanged();
            }
        }
    }
}
