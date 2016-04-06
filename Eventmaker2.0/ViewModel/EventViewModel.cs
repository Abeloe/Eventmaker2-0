using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Eventmaker.Common;
using Eventmaker2._0.Annotations;
using Eventmaker2._0.Handler;
using Eventmaker2._0.Model;
using Eventmaker2._0.View;

namespace Eventmaker2._0.ViewModel
{
    public class EventViewModel : INotifyPropertyChanged
    {

        private DateTimeOffset _date;
        private TimeSpan _time;
        private ICommand _createEventCommand;
        private ICommand _navigateToCreateCommand;
        private ICommand _navigateToEventCommand;
        private Event _selectedEvent;
        private ICommand _deleteEventCommand;
        public int Id { get; set; }
        public Handler.EventHandler EHandler { get; set; }
        public string Name { get; set; }
        public string Beskrivelse { get; set; }
        public string Place { get; set; }


        public Event SelectedEvent
        {
            get { return _selectedEvent; }
            set { _selectedEvent = value; OnPropertyChanged();}
        }

        public ICommand DeleteEventCommand
        {
            get { return _deleteEventCommand ?? (_deleteEventCommand = new RelayCommand(()=> EHandler.DeleteEvent(SelectedEvent))); }
        }

        public ICommand CreateEventCommand
        {
            get { return _createEventCommand ?? (_createEventCommand = new RelayCommand(EHandler.CreateEvent));}
        }

        public ICommand NavigateToCreateCommand
        {
            get { return _navigateToCreateCommand ?? (_navigateToCreateCommand = new RelayCommand( (() =>
            {
                Frame rootFrame = Window.Current.Content as Frame;
                rootFrame.Navigate(typeof (CreateEventPage));
            }))); }
        }

        public ICommand NavigateToEventCommand
        {
            get
            {
                return _navigateToEventCommand ?? (_navigateToEventCommand = new RelayCommand((() =>
                {
                    Frame rootFrame = Window.Current.Content as Frame;
                        rootFrame.Navigate(typeof(Eventpage));
                })));
            }
        }
      
        public DateTimeOffset Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public TimeSpan Time
        {
            get { return _time; }
            set { _time = value; }
        }

        public EventCatalogSingelton EventCatalogSingelton { get; set; }

        public EventViewModel()
        {
            EventCatalogSingelton = EventCatalogSingelton.EventCatalog;
            EHandler = new Handler.EventHandler(this);

            DateTime dt = System.DateTime.Now;

            _date = new DateTimeOffset(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0, new TimeSpan());
            _time = new TimeSpan(dt.Hour, dt.Minute, dt.Second);

        }

        // Auto Generated via. InotifyProportyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        
    }
}
