using BowlingAlleyDAL.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingAlleyDAL
{
    public class BowlingAlleyRepository
    {
        private readonly BowlingAlleyDBContext _context;

        public BowlingAlleyRepository(BowlingAlleyDBContext context)
        {
            _context = context;
        }

        public BowlingAlleyRepository()
        {
        }

        //public int ApproveOrReject(int reservationId, int? slotStatus)
        //{
        //    int status = 0;
        //    Reservations reservation = null;
        //    try
        //    {
        //        reservation = _context.Reservations.Find(reservationId);
        //        reservation.Status = slotStatus;
        //        _context.SaveChanges();
        //        status = 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        status = -99;
        //    }
        //    return status;
        //}        
        public int ApproveOrReject(int reservationId, int n)
        {
            int status = 0;
            Reservations reservation = null;
            try
            {
                reservation = _context.Reservations.Find(reservationId);
                if(n==0)
                {
                    reservation.Status = 1;
                }
                else
                {
                    reservation.Status = -1;
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                status = -99;
            }
            return status;
        }
        public int BookSlots(int slotId, int empId)
        {
            int result = 0;
            try
            {
                SqlParameter prmSlotId = new SqlParameter("@SlotId", slotId);
                SqlParameter prmEmpId = new SqlParameter("@EmpId", empId);
                SqlParameter prmReservationId = new SqlParameter("@ReservationId", System.Data.SqlDbType.Int);
                prmReservationId.Direction = System.Data.ParameterDirection.Output;

                result = _context.Database.ExecuteSqlCommand(
                    "EXEC usp_BookSlots @SlotId, @EmpId, @ReservationId OUT",
                    new[] { prmSlotId, prmEmpId, prmReservationId });
                empId = Convert.ToInt32(prmReservationId.Value);
            }
            catch (Exception)
            {
                result = -99;
                empId = 0;
            }
            return result;
        }
        public List<ReservationDetails> GetAllRejectedSlots()
        {
            List<ReservationDetails> reservationDetails = null;
            try
            {
                reservationDetails = _context.ReservationDetails.FromSqlRaw("SELECT * FROM ufn_FetchAllRejectedSlots()").ToList();
            }
            catch (Exception ex)
            {
                reservationDetails = null;
            }
            return reservationDetails;
        }
        public List<BookingSlots> GetFreeSlots()
        {
            List<Reservations> reservations = null;
            List<BookingSlots> slots = null;
            List<BookingSlots> freeSlots = null;
            try
            {
                reservations = _context.Reservations.Where(r => r.ReservedOn == DateTime.Today).ToList();
                slots = _context.BookingSlots.ToList();
                freeSlots = _context.BookingSlots.ToList();
                if (reservations != null)
                {
                    foreach (var r in reservations)
                    {
                        foreach (var s in slots)
                        {
                            if (r.SlotId == s.SlotId)
                            {
                                BookingSlots slot = s;
                                freeSlots.Remove(slot);
                            }             
                        }
                    }
                }
                else
                {
                    freeSlots = null;
                }
            }
            catch (Exception ex)
            {
                freeSlots = null; 
            }
            return freeSlots;
        }
        public List<Reservations> GetReservedSlots()
        {
            List<Reservations> reservedSlots = null;
            try
            {
                reservedSlots = _context.Reservations
                                    .Where(r => r.ReservedOn == DateTime.Today && r.Status == 0)
                                    .ToList();
            }
            catch (Exception ex)
            {
                reservedSlots = null;
            }
            return reservedSlots;
        }
        public string GetAdminName(int empId)
        {
            string adminName = null;
            try
            {
                adminName = (from r in _context.Reservations
                            select BowlingAlleyDBContext.ufn_GetAdminName()).FirstOrDefault();
            }
            catch (Exception)
            {
                adminName = null;
            }
            return adminName;
        }

        public byte? ValidateCredentials(string empname, string empid)
        {
            int roleId = Convert.ToInt32(empid);
            Roles roles = _context.Roles.Find(roleId);

            byte? status = null;
            if (roles.EmpId == roleId && roles.EmpName==empname)
            {
                status = 0;
            }

            return status;
        }
    }
}
