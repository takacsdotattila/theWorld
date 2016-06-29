using System;
using Microsoft.AspNet.Mvc;
using TheWorld.ViewModels;
using TheWorld.Services;
using TheWorld.Models;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNet.Authorization;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IWorldRepository _repository;


        public AppController(IMailService service, IWorldRepository repository)
        {
            _mailService = service;
            _repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Trips()
        {
            var trips = _repository.GetAllTrips();
            return View(trips);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var sitemail = Startup.Configuration["AppSettings:SiteEmailAdress"];
                if(string.IsNullOrWhiteSpace(sitemail))
                {
                    ModelState.AddModelError("","Could not send email, configuration error");
                }
                if(_mailService.SendMail(sitemail, sitemail,
                    $"Contact Page from {model.Name} ({model.Email})",
                    model.Message))
                {
                    ModelState.Clear();
                    ViewBag.Message = "The email sent. Thanks!";
                }
            }
            return View();
        }
    }
} 