using CodingEvents.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodingEvents.ViewModels
{
    public class AddEventViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength =3, ErrorMessage = "Name must be between 3 and 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a description for your event")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage ="Location is required")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Max attendees is required")]
        [Range(0, 100000, ErrorMessage = "Must have fewer than 100K attendees")]
        public int AttendanceLimit { get; set; }

        [EmailAddress]
        public string ContactEmail { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }

        public List<SelectListItem> Categories { get; set; }

        public AddEventViewModel(List<EventCategory> categories)
        {
            Categories = new List<SelectListItem>();

            foreach (EventCategory category in categories)
            {
                Categories.Add(
                    new SelectListItem
                    {
                        Value = category.Id.ToString(),
                        Text = category.Name
                    }
                );
            }
        }

        public AddEventViewModel() { }
        
    }
}
