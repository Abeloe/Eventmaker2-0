using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventmaker2._0.Model
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Beskrivelse  { get; set; }
        public string Place { get; set; }
        public DateTime DateTime { get; set; }

        public Event(int id, string name, string beskrivelse, string place, DateTime dateTime)
        {
            Id = id;
            Name = name;
            Beskrivelse = beskrivelse;
            Place = place;
            DateTime = dateTime;
        }

        public Event()
        {
            
        }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Beskrivelse: {Beskrivelse}, Place: {Place}, DateTime: {DateTime}";
        }
    }


}
