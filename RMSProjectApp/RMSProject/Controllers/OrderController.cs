﻿using Microsoft.AspNetCore.Mvc;
using RMSProject.Repositories.IRepository;

namespace RMSProject.Controllers
{
    public class OrderController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
