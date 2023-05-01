using AutoMapper;
using BowlingAlleyDAL;
using BowlingAlleyDAL.Models;
//using BowlingAlleyApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BowlingAlleyApp.Controllers
{
    public class MemberController : Controller
    {
        private readonly BowlingAlleyDBContext _context;
        BowlingAlleyRepository repObj;
        private readonly IMapper _mapper;
        public MemberController (BowlingAlleyDBContext context, IMapper mapper)
        {
            _context = context;
            repObj = new BowlingAlleyRepository(_context);
            _mapper = mapper;
        }

        public IActionResult GetFreeSlots()
        {
            var lstEntityFreeSlots = repObj.GetFreeSlots();
            List<Models.BookingSlots> lstModelFreeSlots = new List<Models.BookingSlots>();
            foreach (var freeSlots in lstEntityFreeSlots)
            {
                lstModelFreeSlots.Add(_mapper.Map<Models.BookingSlots>(freeSlots));
            }
            return View(lstModelFreeSlots);
        }
        public IActionResult BookSlots(Models.BookingSlots bookingSlotsObject)
        {
            return View(bookingSlotsObject);
        }
        public IActionResult ConfirmBooking(Models.BookingSlots confirmBookingsObj, Models.Roles rolesObj)
        {

                var returnValue = repObj.BookSlots(confirmBookingsObj.SlotId, rolesObj.EmpId);
                if (returnValue>0)
                    return View("Success");
                else
                    return View("Error");
            
        }
    }
}
