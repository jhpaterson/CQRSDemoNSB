using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Messaging;
using CQRSDemo.ReadModelAC;
using CQRSDemo.Commands;
using NServiceBus;

namespace CQRSDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        ReadContext context;
        public IBus Bus { get; set; }

        public HomeController()
        {
            context = new ReadContext();
        }

        public ActionResult Index()
        {
            var customers = context.Customers.ToList();
            return View(customers);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CustomerReadModel customer)
        {
            customer.CustomerId = Guid.NewGuid();

            var command = new CreateNewCustomerCommand
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Address = customer.Address,
                PostCode = customer.PostCode
            };
            Bus.Send(command);

            return View("ConfirmCreate", customer);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            CustomerReadModel customer = context.Customers.
                Where(c => c.CustomerId == id).
                FirstOrDefault();
            return View(customer);
        }

        [HttpPost]
        public ActionResult Edit(CustomerReadModel customer)
        {
            CustomerReadModel customerToUpdate = context.Customers.
                Where(c => c.CustomerId == customer.CustomerId).
                FirstOrDefault();

            var command = new UpdateCustomerAddressCommand
            {
                CustomerId = customer.CustomerId,
                Address = customer.Address,
                PostCode = customer.PostCode
            };
            Bus.Send(command);

            return View("ConfirmEdit", customer);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
