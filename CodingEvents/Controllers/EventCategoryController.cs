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
    public class EventCategoryController : Controller
    {
        private EventDbContext context;

        public EventCategoryController(EventDbContext dbContext)
        {
            context = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<EventCategory> categories = context.EventCategories.ToList();

            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            AddEventCategoryViewModel addEventCategoryViewModel = new AddEventCategoryViewModel();

            return View(addEventCategoryViewModel);
        }

        [HttpPost]
        public IActionResult ProcessCreateEventCategoryForm(AddEventCategoryViewModel addEventCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                EventCategory newCategory = new EventCategory
                {
                    Name = addEventCategoryViewModel.Name
                };

                context.EventCategories.Add(newCategory);
                context.SaveChanges();

                return Redirect("/EventCategory");
            }

            return View(addEventCategoryViewModel);
        }

        public IActionResult Delete()
        {
            List<EventCategory> deletableCategories = context.EventCategories.ToList();
            return View(deletableCategories);
        }

        [HttpPost]
        public IActionResult Delete(int[] eventIds)
        {
            foreach (int eventId in eventIds)
            {
                //EventData.Remove(eventId);
                EventCategory categoryToDelete = context.EventCategories.Find(eventId);
                context.EventCategories.Remove(categoryToDelete);
            }

            context.SaveChanges();

            return Redirect("/EventCategory");
        }
    }
}
