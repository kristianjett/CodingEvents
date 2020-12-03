using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingEvents.Data;
using CodingEvents.Models;
using CodingEvents.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CodingEvents.Controllers
{
    public class EventsController : Controller
    {
        // Get: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            List<Event> events = new List<Event>(EventData.GetAll());

            return View(events);
        }

        public IActionResult Add()
        {
            AddEventViewModel addEventViewModel = new AddEventViewModel();
            
            return View(addEventViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddEventViewModel addEventViewModel)
        {
            if (ModelState.IsValid)
            {
                Event newEvent = new Event
                {
                    Name = addEventViewModel.Name,
                    Description = addEventViewModel.Description,
                    Location = addEventViewModel.Location,
                    AttendanceLimit = addEventViewModel.AttendanceLimit,
                    ContactEmail = addEventViewModel.ContactEmail,
                    Type = addEventViewModel.Type
                };

                EventData.Add(newEvent);

                return Redirect("/Events");
            }

            return View(addEventViewModel);
            
        }

        public IActionResult Delete()
        {
            List<Event> deletableEvents = new List<Event>(EventData.GetAll());
            return View(deletableEvents);
        }

        [HttpPost]
        public IActionResult Delete(int[] eventIds)
        {
            foreach (int eventId in eventIds)
            {
                EventData.Remove(eventId);
            }

            return Redirect("/Events");
        }

        [Route("/events/edit/{eventId}")]
        public IActionResult Edit(int eventId)
        {
            Event eventToEdit = EventData.GetById(eventId);
            return View(eventToEdit);
        }

        [HttpPost("/Events/Edit")]
        public IActionResult SubmitEditEventForm(int eventId, string name, string description, string location, int attendanceLimit, string contactEmail)
        {
            //TODO add controller code
            EventData.GetById(eventId).Name = name;
            EventData.GetById(eventId).Description = description;
            EventData.GetById(eventId).Location = location;
            EventData.GetById(eventId).AttendanceLimit = attendanceLimit;
            EventData.GetById(eventId).ContactEmail = contactEmail;

            return Redirect("/Events");
        }
    }
}
