using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AircraftInformation.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string DepartureCountry { get; set; }
        public string ArrivalCountry { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public string DepartureAirportCode { get; set; }
        public string ArrivalAirportCode { get; set; }

        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public TimeSpan FlightTime => ArrivalTime - DepartureTime;


        public int? DeparturePlaneId { get; set; }
        public Plane DeparturePlane { get; set; }
        public int? ArrivalPlaneId { get; set; }
        public Plane ArrivalPlane { get; set; }
    }
}
