using BowlingAlleyDAL.Models;
using BowlingAlleyDAL;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;

namespace BowlingAlleyApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly BowlingAlleyDBContext _context;
        BowlingAlleyRepository repObj;
        private readonly IMapper _mapper;

        public AdminController(BowlingAlleyDBContext context, IMapper mapper)
        {
            _context = context;
            repObj = new BowlingAlleyRepository(_context);
            _mapper = mapper;
        }
        public IActionResult GetReservationDetails()
        {
            var entityreservedSlots = repObj.GetReservedSlots();
            List<Models.Reservations> lstModelReservedSlots = new List<Models.Reservations>();
            foreach (var reservedSlots in entityreservedSlots)
            {
                lstModelReservedSlots.Add(_mapper.Map<Models.Reservations>(reservedSlots));
            }
            return View(lstModelReservedSlots);

        }
        public IActionResult Approve(Models.Reservations reservations)
        {
            int result = repObj.ApproveOrReject(reservations.ReservationId, 0);
            return View();
        }
        public IActionResult Reject(Models.Reservations reservations)
        {
            int result = repObj.ApproveOrReject(reservations.ReservationId, 1);
            return View();
        }
        public IActionResult GetRejectedSlots()
        {
            var entityrejectedSlots = repObj.GetAllRejectedSlots();

            List<Models.ReservationDetails> lstModelRejectedSlots = new List<Models.ReservationDetails>();
            foreach (var rejectedSlots in entityrejectedSlots)
            {
                lstModelRejectedSlots.Add(_mapper.Map<Models.ReservationDetails>(rejectedSlots));
            }

            return View(lstModelRejectedSlots);
        }
    }
}
