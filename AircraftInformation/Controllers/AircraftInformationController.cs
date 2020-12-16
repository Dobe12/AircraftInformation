using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AircraftInformation.Data;
using AircraftInformation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AircraftInformation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftInformationController : ControllerBase
    {
        private readonly AircraftContext _context;

        public AircraftInformationController(AircraftContext context)
        {
            _context = context;
        }

        // GET api/[controller]/flights
        [HttpGet("flights")]
        [ProducesResponseType(typeof(IEnumerable<Flight>), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get()
        {
            var allFlights = await _context.Flights.ToListAsync();

            if (allFlights == null)
            {
                NotFound("Flights not found");
            }

            return Ok(allFlights);
        }

        // GET api/[controller]/flights/{depAirportCode}/{arrivAirportCode}
        [HttpGet("flights/{depAirportCode}/{arrivAirportCode}")]
        [ProducesResponseType(typeof(IEnumerable<Flight>), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetFlightsByCode(string depAirportCode, string arrivAirportCode)
        {
            var flights = await _context.Flights
                .Where(f => f.DepartureAirportCode == arrivAirportCode
                            && f.ArrivalAirportCode == arrivAirportCode)
                .ToListAsync();

            if (flights == null)
            {
                NotFound("Flights not found");
            }

            return Ok(flights);
        }

        // GET api/[controller]/flights/{depAirportCode}/{depTime}/{arrivAirportCode}/{arrivTime}/
        [HttpGet("flights/{depAirportCode}/{depTime}/{arrivAirportCode}/{arrivTime}/")]
        [ProducesResponseType(typeof(IEnumerable<Flight>), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetFlightsByCodeAndTime(
            string depAirportCode, DateTime depTime,
            string arrivAirportCode, DateTime arrivTime)
        {
            var flights = await _context.Flights
                .Where(f => f.DepartureAirportCode == arrivAirportCode
                            && f.ArrivalAirportCode == arrivAirportCode
                            && f.DepartureTime.Day == depTime.Day
                            && f.ArrivalTime.Day == arrivTime.Day)
                .ToListAsync();

            if (flights == null)
            {
                NotFound("Flights not found");
            }

            return Ok(flights);
        }

        // GET api/[controller]/flights/{arrivTimeStart}/{arrivTimeEnd}/{arrivAirportCode}
        [HttpGet("flights/{arrivTimeStart}/{arrivTimeEnd}/{arrivAirportCode}")]
        [ProducesResponseType(typeof(IEnumerable<Flight>), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetArrivFlightsByTimeAndAirportCode(DateTime arrivTimeStart, DateTime arrivTimeEnd,
            string arrivAirportCode)
        {
            var flights = await _context.Flights
                .Where(f => f.ArrivalAirportCode == arrivAirportCode
                            && IsRange(f.ArrivalTime, arrivTimeStart, arrivTimeEnd))
                .ToListAsync();

            if (flights == null)
            {
                NotFound("Flights not found");
            }

            return Ok(flights);
        }

        // GET api/[controller]/flights/{depTimeStart}/{depTimeEnd}/{depAirportCode}
        [HttpGet("flights/{depTimeStart}/{depTimeEnd}/{depAirportCode}")]
        [ProducesResponseType(typeof(IEnumerable<Flight>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetDepFlightsByTimeAndAirportCode(DateTime depTimeStart, DateTime depTimeEnd,
            string depAirportCode)
        {
            var flights = await _context.Flights
                .Where(f => f.DepartureAirportCode == depAirportCode
                            && IsRange(f.DepartureTime, depTimeStart, depTimeEnd))
                .ToListAsync();

            if (flights == null)
            {
                NotFound("Flights not found");
            }

            return Ok(flights);
        }

        private static bool IsRange(DateTime dateToCheck, DateTime startDate, DateTime endDate)
        {
            return dateToCheck >= startDate && dateToCheck < endDate;
        }
    }

}