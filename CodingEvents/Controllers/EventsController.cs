using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingEvents.Data;
using CodingEvents.Models;
using CodingEvents.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodingEvents.Controllers
{
    public class EventsController : Controller
    {

        private EventDbContext context;

        public EventsController(EventDbContext dbContext)
        {
            context = dbContext;
        }

        // Get: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            List<Event> events = context.Events
                .Include(e => e.Category)
                .ToList();

            return View(events);
        }

        public IActionResult Add()
        {
            List<EventCategory> categories = context.EventCategories.ToList();
            AddEventViewModel addEventViewModel = new AddEventViewModel(categories);
            
            return View(addEventViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddEventViewModel addEventViewModel)
        {
            if (ModelState.IsValid)
            {
                EventCategory theCategory = context.EventCategories.Find(addEventViewModel.CategoryId);
                Event newEvent = new Event
                {
                    Name = addEventViewModel.Name,
                    Description = addEventViewModel.Description,
                    Location = addEventViewModel.Location,
                    AttendanceLimit = addEventViewModel.AttendanceLimit,
                    ContactEmail = addEventViewModel.ContactEmail,
                    Category = theCategory
                };

                context.Events.Add(newEvent);
                context.SaveChanges();

                return Redirect("/Events");
            }

            return View(addEventViewModel);
            
        }

        public IActionResult Delete()
        {
            List<Event> deletableEvents = context.Events.ToList();
            return View(deletableEvents);
        }

        [HttpPost]
        public IActionResult Delete(int[] eventIds)
        {
            foreach (int eventId in eventIds)
            {
                //EventData.Remove(eventId);
                Event eventToDelete = context.Events.Find(eventId);
                context.Events.Remove(eventToDelete);
            }

            context.SaveChanges();

            return Redirect("/Events");
        }

        public IActionResult Detail(int id)
        {
            Event theEvent = context.Events
                .Include(e => e.Category)
                .Single(e => e.Id == id);

            List<EventTag> eventTags = context.EventTags
                    .Where(et => et.EventId == id)
                    .Include(et => et.Tag)
                    .ToList();

            EventDetailViewModel viewModel = new EventDetailViewModel(theEvent, eventTags);
            return View(viewModel);
        }



        //[Route("/events/edit/{eventId}")]
        //public IActionResult Edit(int eventId)
        //{
        //    Event eventToEdit = EventData.GetById(eventId);
        //    return View(eventToEdit);
        //}

        //[HttpPost("/Events/Edit")]
        //public IActionResult SubmitEditEventForm(int eventId, string name, string description, string location, int attendanceLimit, string contactEmail)
        //{
        //    //TODO add controller code
        //    EventData.GetById(eventId).Name = name;
        //    EventData.GetById(eventId).Description = description;
        //    EventData.GetById(eventId).Location = location;
        //    EventData.GetById(eventId).AttendanceLimit = attendanceLimit;
        //    EventData.GetById(eventId).ContactEmail = contactEmail;

        //    return Redirect("/Events");
        //}
    }
}
