using System;
using System.Collections.Generic;
using System.Text;

namespace AppTrips.Models
{
    public class TripModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime TripDate { get; set; }
        public string Notes { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Rating { get; set; }
        public string ImageUrl { get; set; }
    }
}
