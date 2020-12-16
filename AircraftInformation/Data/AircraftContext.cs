using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AircraftInformation.Models;
using Microsoft.EntityFrameworkCore;

namespace AircraftInformation.Data
{
    public sealed class AircraftContext : DbContext
    {
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Plane> Planes { get; set; }



        public AircraftContext(DbContextOptions<AircraftContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Plane>().HasData(new List<Plane>
            {
                new Plane
                {
                    Id = 1,
                    Model = "TU-154",
                    YearCreation = 1990,
                    Capacity = 220
                },
                new Plane
                {
                    Id = 2,
                    Model = "AIR 777",
                    YearCreation = 1987,
                    Capacity = 400
                },
            });

            modelBuilder.Entity<Flight>().HasData(new List<Flight>
            {
                new Flight
                {
                    Id = 1,
                    DepartureCountry = "Russia",
                    DepartureCity = "Moscow",
                    DepartureAirportCode = "VKO",
                    ArrivalAirportCode = "VVO",
                    ArrivalCountry = "Russia",
                    ArrivalCity = "Vladivostok",
                    DepartureTime = new DateTime(2020, 12, 12, 12, 12, 12),
                    ArrivalTime = new DateTime(2020, 12, 12, 19, 00, 12),
                    DeparturePlaneId = 1,
                    ArrivalPlaneId = 2
                }
            });
        }
    }
}
