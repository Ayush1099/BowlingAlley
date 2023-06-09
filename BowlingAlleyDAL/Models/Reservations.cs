﻿using System;
using System.Collections.Generic;

namespace BowlingAlleyDAL.Models
{
    public partial class Reservations
    {
        public int ReservationId { get; set; }
        public int ReservedBy { get; set; }
        public DateTime ReservedOn { get; set; }
        public int? Status { get; set; }
        public int SlotId { get; set; }

        public virtual Roles ReservedByNavigation { get; set; }
        public virtual BookingSlots Slot { get; set; }
    }
}
