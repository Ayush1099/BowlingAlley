using AutoMapper;
using BowlingAlleyDAL.Models;

namespace BowlingAlleyApp.Repository
{
    public class BowlingAlleyMapper : Profile
    {
         public BowlingAlleyMapper()
         {
            CreateMap<BookingSlots, Models.BookingSlots>();
            CreateMap<Models.BookingSlots,BookingSlots>();

            CreateMap<Reservations, Models.Reservations>();
            CreateMap<Models.Reservations,Reservations>();
            
            CreateMap<ReservationDetails, Models.ReservationDetails>();
            CreateMap<Models.ReservationDetails,ReservationDetails>();
         }
    }
}
