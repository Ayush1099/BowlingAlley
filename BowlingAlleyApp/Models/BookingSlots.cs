using BowlingAlleyDAL.Models;
using System.Collections.Generic;
using System;

namespace BowlingAlleyApp.Models
{
    public class BookingSlots
    {
        public BookingSlots()
        {
            Reservations = new HashSet<Reservations>();
        }

        public int SlotId { get; set; }
        public TimeSpan SlotStartTime { get; set; }
        public TimeSpan SlotEndTime { get; set; }

        public virtual ICollection<Reservations> Reservations { get; set; }
    }
}
