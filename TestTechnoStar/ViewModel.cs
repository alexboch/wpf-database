using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TestTechnoStar.Database;

namespace TestTechnoStar
{
    internal class ViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<ILogDataEntry> LogDataEntries { get; set; } = new ObservableCollection<ILogDataEntry>();

        private ILogDataEntry _selectedLogDataEntry;

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
            if (SelectedLogDataEntry is LogEntryWithData)
            {
                using (var context = new Context())
                {
                    //Если выбрано значение в списке, то редактируем его
                    context.SaveChanges();
                }
            }
            else if(SelectedLogDataEntry is PlaceholderLogDataEntry placeholderEntry)
            {
                //Если значение в списке не выбрано, добавим новую запись
                AddNewEntry();
                placeholderEntry.Text = string.Empty;
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
            LogDataEntries.Clear();
            using (var context = new Context())
            {
 
                foreach (var logEntry in context.LogEntries)
                {
                    var dataLogEntry = new LogEntryWithData(logEntry, logEntry.Data);
                    LogDataEntries.Add(dataLogEntry);
                }
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
