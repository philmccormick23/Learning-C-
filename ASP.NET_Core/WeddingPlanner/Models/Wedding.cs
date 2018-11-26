using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models {
    public class Wedding {
        
        public int WeddingId { get; set; }

        [Required]
        public string WedderOne { get; set; }

        [Required]
        public string WedderTwo { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Address { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public List<Guest> Guests { get; set; }

        public int UserId { get; set;}
        public User Planner { get; set; } 

    }

}