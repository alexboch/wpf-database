﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using TestTechnoStar.Database;

namespace TestTechnoStar
{
    internal class ViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<ILogDataEntry> LogDataEntries { get; set; } = new ObservableCollection<ILogDataEntry>();

        private ILogDataEntry _selectedLogDataEntry;

        private bool _dataLoaded;

        public bool DataLoaded
        {
            get => _dataLoaded;
            set
            {
                _dataLoaded = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Выбранное в списке значение
        /// </summary>
        public ILogDataEntry SelectedLogDataEntry
        {
            get => _selectedLogDataEntry;
            set
            {
                _selectedLogDataEntry = value;
                OnPropertyChanged();
            }
        }

        public void SetText()
        {
            if (SelectedLogDataEntry is LogEntryWithData dataLogEntry)
            {
                dataLogEntry.Save();
            }
            else if(SelectedLogDataEntry is PlaceholderLogDataEntry)
            {
                //Если значение в списке не выбрано, добавим новую запись
                AddNewEntry();
            }
        }

        private void AddNewEntry()
        {
            using (var context = new Context())
            {
                var dataEntry = new DataEntry {Text = SelectedLogDataEntry.Text};
                //Добавляем запись в первую таблицу
                context.DataEntries.Add(dataEntry);
                var logEntry = new LogEntry {CreatedDate = DateTime.Now, Data = dataEntry};
                //Добавляем запись во вторую таблицу
                context.LogEntries.Add(logEntry);
                var logDataEntry = new LogEntryWithData(logEntry, dataEntry);
                LogDataEntries.Add(logDataEntry);
                context.SaveChanges();
            }
        }


        private void RefreshList()
        {
            using (var context = new Context())
            {
                foreach (var logEntry in context.LogEntries)
                {
                    var dataLogEntry = new LogEntryWithData(logEntry, logEntry.Data);
                   Application.Current.Dispatcher.Invoke( () => LogDataEntries.Add(dataLogEntry));
                }
            }
        }

        public async void LoadDataAsync()
        {
            LogDataEntries.Clear();
            await Task.Factory.StartNew(RefreshList);
            DataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
