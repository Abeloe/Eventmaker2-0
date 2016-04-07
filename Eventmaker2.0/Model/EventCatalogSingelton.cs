    using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using System.ServiceModel;
    using System.Text;
using System.Threading.Tasks;
using Eventmaker2._0.Persistency;

namespace Eventmaker2._0.Model
{
    public class EventCatalogSingelton
    {
        private static EventCatalogSingelton _eventCatalog = new EventCatalogSingelton();

        public static EventCatalogSingelton EventCatalog
        {
            get { return _eventCatalog ?? (_eventCatalog = new EventCatalogSingelton()); }
        }

        public ObservableCollection<Event> Events { get; set;}

        private EventCatalogSingelton()
        {

            Events = new ObservableCollection<Event>();
            LoadEvent();
        }

        public void AddEvent(Event e)
        {
            Events.Add(e);
            PersistencyService.SaveEventsAsJsonAsync(e);
        }

        public void RemoveEvent(Event r)
        {
            Events.Remove(r);
            PersistencyService.RemoveEvent(r);
        }

        public static async void UpdateEvent()
        {

        }


        public async void LoadEvent()
        {
            var events = await PersistencyService.LoadEventsFromJsonAsync();
            if (events != null)
            {
                foreach (var e in events)
                {
                    Events.Add(e);
                    
                }
            }
        }
    }
}
