using System;
using System.Collections.Generic;

namespace WeddingPlanner.Models
{
    public class Guest {
        public int GuestId {get; set;}

        public int UserId {get; set;}
        public User Attendee {get; set;}

        public int WeddingId {get; set;}
        public Wedding WeddingAttended {get; set;}

    }
    

}