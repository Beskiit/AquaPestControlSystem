﻿using AquaPestControlSystem.DAL;
using AquaPestControlSystem.Models;
using AquaPestControlSystem.Models.DBEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AquaPestControlSystem.Controllers
{
    public class ProprieterController : Controller
    {
        private readonly ProprieterCustomerDBContext _context;

        public ProprieterController(ProprieterCustomerDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ProprieterCustomer()
        {
            List<CustomerViewModel> customerList = new List<CustomerViewModel>();
            var customer = _context.Customers.ToList();

            if (customer != null)
            {
                foreach (var customers in customer)
                {
                    var CustomerViewModel = new CustomerViewModel
                    {
                        CustomerId = customers.CustomerId,
                        FirstName = customers.FirstName,
                        LastName = customers.LastName,
                        MiddleName = customers.MiddleName,
                        ContactNum = customers.ContactNum,
                        Email = customers.Email,
                        Address = customers.Address
                    };
                    customerList.Add(CustomerViewModel);
                }
                return View(customerList);
            }
            return View(customerList);
        }
        [HttpGet]
        public IActionResult ProprieterTechnicians()
        {
            List<TechnicianViewModel> technicianList = new List<TechnicianViewModel>();
            var technician = _context.Technicians.ToList();

            if (technician != null)
            {
                foreach (var technicians in technician)
                {
                    var TechnicianViewModel = new TechnicianViewModel
                    {
                        TechnicianId = technicians.TechnicianId,
                        FirstName = technicians.FirstName,
                        LastName = technicians.LastName,
                        MiddleName = technicians.MiddleName,
                        ContactNum = technicians.ContactNum,
                        Address = technicians.Address,
                        Status = technicians.Status
                    };
                    technicianList.Add(TechnicianViewModel);
                }
                return View(technicianList);
            }
            return View(technicianList);
        }

        public IActionResult ProprieterReports()
        {
            return View();
        }

        public IActionResult ProprieterDashboard()
        {
            return View();
        }
        public IActionResult ProprieterActivityLog()
        {
            return View();
        }

        public IActionResult ProprieterArchives()
        {
            return View();
        }


        public IActionResult ProprieterViewCustomer()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ProprieterAppointments()
        {
            var appointments = _context.Appointments.ToList();
            var appointmentViewModel = appointments.Select(a => new AppointmentViewModel
            {
                FirstName = a.FirstName,
                LastName = a.LastName,
                MiddleName = a.MiddleName,
                ContactNum = a.ContactNum,
                Address = a.Address,
                Email = a.Email,
                PestProblem = a.PestProblem,
                Schedule = a.Schedule,
                ImageUrl = Url.Content(a.ImageFileName)
            }).ToList();

            return View(appointmentViewModel);
        }

        [HttpGet]
        public IActionResult ProprieterAddCustomer()
        {
            return View();
        }

        [HttpPost]
        [Route("Proprieter/ProprieterAddCustomer")]
        public IActionResult ProprieterAddCustomer(CustomerViewModel customerData)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer
                {
                    FirstName = customerData.FirstName,
                    LastName = customerData.LastName,
                    MiddleName = customerData.MiddleName,
                    ContactNum = customerData.ContactNum,
                    Email = customerData.Email,
                    Address = customerData.Address
                };

                _context.Customers.Add(customer);
                _context.SaveChanges();
                return RedirectToAction("ProprieterCustomer");
            }
            else
            {
                TempData["errorMessage"] = " Model data is not valid";
                return View();
            }
        }

        [HttpGet]
        public IActionResult ProprieterAddTechnician()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProprieterAddTechnician(TechnicianViewModel technicianData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var technician = new Technician
                    {
                        FirstName = technicianData.FirstName,
                        LastName = technicianData.LastName,
                        MiddleName = technicianData.MiddleName,
                        ContactNum = technicianData.ContactNum,
                        Address = technicianData.Address,
                        Status = technicianData.Status
                    };

                    _context.Technicians.Add(technician);
                    _context.SaveChanges();
                    return RedirectToAction("ProprieterTechnicians");
                }
                else
                {
                    TempData["errorMessage"] = " Model data is not valid";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        public IActionResult ProprieterEditTechnician()
        {
            return View();
        }

        public IActionResult ProprieterViewReport()
        {
            return View();
        }

        public IActionResult ProprieterViewArchives()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ProprieterAddAppointment()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProprieterAddAppointment(AppointmentViewModel appointmentData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (appointmentData.ImageFile == null || appointmentData.ImageFile.Length == 0)
                    {
                        ModelState.AddModelError("ImageFile", "Please select an image file.");
                        return View(appointmentData);
                    }

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(appointmentData.ImageFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        appointmentData.ImageFile.CopyTo(stream);
                    }

                    var appointment = new Appointment()
                    {
                        FirstName = appointmentData.FirstName,
                        LastName = appointmentData.LastName,
                        MiddleName = appointmentData.MiddleName,
                        ContactNum = appointmentData.ContactNum,
                        Address = appointmentData.Address,
                        PestProblem = appointmentData.PestProblem,
                        Schedule = appointmentData.Schedule,
                        ImageFileName = "~/images/" + fileName
                    };

                    _context.Appointments.Add(appointment);
                    _context.SaveChanges();

                    return RedirectToAction("ProprieterAppointments");
                }
                else
                {
                    ModelState.AddModelError("CustomError", "This is a custom validation error message.");

                    // Return the view with validation errors
                    return View(appointmentData);
                }
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
        }

        [HttpGet]
        public IActionResult ProprieterViewAppointments()
        {
            var appointments = _context.Appointments.ToList();
            var appointmentViewModel = appointments.Select(a => new AppointmentViewModel
            {
                FirstName = a.FirstName,
                LastName = a.LastName,
                MiddleName = a.MiddleName,
                ContactNum = a.ContactNum,
                Address = a.Address,
                Email = a.Email,
                PestProblem = a.PestProblem,
                Schedule = a.Schedule,
                ImageUrl = Url.Content(a.ImageFileName)
            }).ToList();

            return View(appointmentViewModel);
        }


        public IActionResult ProprieterRequestAppointments()
        {
            return View();
        }

        public IActionResult ProprieterAssignAppointment()
        {
            return View();
        }

        public IActionResult ProprieterArchiveActivityLog()
        {
            return View();
        }

        public IActionResult ProprieterArchiveAppointment()
        {
            return View();
        }

        public IActionResult ProprieterArchiveCustomers()
        {
            return View();
        }

        public IActionResult ProprieterArchiveReports()
        {
            return View();
        }

        public IActionResult ProprieterArchiveTechnicians()
        {
            return View();
        }
        public IActionResult ProprieterProfile()
        {
            return View();
        }
    }
}
