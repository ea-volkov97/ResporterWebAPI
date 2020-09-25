using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resporter.Models;
using Resporter.Services;

namespace Resporter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly EventService _eventService;

        public EventController(EventService eventService)
        {
            _eventService = eventService;
        }

        // api/event/create?title=title&organizatorId=id&description=description
        //[HttpPost]
        //public async Task<ActionResult> CreateNewEvent([FromBody] string title, [FromBody] string organizatorId, [FromBody] string descrition)
        //{
        //    Event _event = new Event(title, organizatorId);
        //    //await _eventService.CreateNewEvent(_event);
        //    return Ok();
        //}
    }
}