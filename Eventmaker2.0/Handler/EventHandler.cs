using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventmaker2._0.Converter;
using Eventmaker2._0.Model;
using Eventmaker2._0.ViewModel;

namespace Eventmaker2._0.Handler
{
    public class EventHandler
    {
        public EventViewModel EventViewModel { get; set; }
       
        public EventHandler(EventViewModel eventViewModel)
        {
            EventViewModel = eventViewModel;
        }

        public void CreateEvent()
        {
            DateTime newDate = DateTimeConverter.DateTimeOffsetAndTimeSetToDateTime(EventViewModel.Date,
                EventViewModel.Time);
            Event newEvent= new Event(EventViewModel.Id, EventViewModel.Name, EventViewModel.Beskrivelse, EventViewModel.Place, newDate);
            EventCatalogSingelton.EventCatalog.AddEvent(newEvent);
        }

        public void DeleteEvent(Event selectedevent)
        {
           EventCatalogSingelton.EventCatalog.RemoveEvent(selectedevent);
        }
    }
}
