using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApiTrips.Models;

namespace WebApiTrips.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly IConfiguration Configuration;

        public TripsController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpGet]
        public ObservableCollection<TripModel> Get()
        {
            return new TripModel().GetAll(Configuration.GetConnectionString("MySQL"));
        }

        [HttpGet("{id}", Name = "Get")]
        public TripModel Get(int id)
        {
            return new TripModel().Get(Configuration.GetConnectionString("MySQL"), id);
        }

        [HttpPost]
        public ApiResponse Post([FromBody]TripModel trip)
        {
            return trip.Insert(Configuration.GetConnectionString("MySQL"));
        }

        [HttpPut]
        public ApiResponse Put([FromBody]TripModel trip)
        {
            return trip.Update(Configuration.GetConnectionString("MySQL"));
        }

        [HttpDelete ("{id}")]
        public ApiResponse Delete(int id)
        {
            return new TripModel().Delete(Configuration.GetConnectionString("MySQL"), id);
        }
    }
}
