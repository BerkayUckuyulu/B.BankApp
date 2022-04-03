using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Udemy.BankApp.Web.Data.Context;
using Udemy.BankApp.Web.Data.Entities;
using Udemy.BankApp.Web.Data.Interfaces;
using Udemy.BankApp.Web.Data.Repositories;
using Udemy.BankApp.Web.Data.UnitOfWork;
using Udemy.BankApp.Web.Mapping;
using Udemy.BankApp.Web.Models;

namespace Udemy.BankApp.Web.Controllers
{
    public class HomeController : Controller
    {
        //private readonly BankContext _context;
        //private readonly IApplicationUserRepository _applicationUserRepository;
        //private readonly IUserMapper _userMapper;

        //public HomeController(BankContext context, IApplicationUserRepository applicationUserRepository, IUserMapper userMapper)
        //{
        //    _context = context;

        //    _applicationUserRepository = applicationUserRepository;
        //    _userMapper = userMapper;
        //}
        private readonly IUserMapper _userMapper;
        private readonly IUow _uow;
        public HomeController(IUserMapper userMapper, IUow uow)
        {
            _userMapper = userMapper;
            _uow = uow;
        }

        public IActionResult Index()
        {
            //return View(_context.ApplicationUsers.Select(x=>new UserListModel { 
            //Id=x.Id,
            //Name=x.Name,
            //Surname=x.Surname

            //}).ToList());
            
            return View(_userMapper.MapToListOfUserList(_uow.GetRepository<ApplicationUser>().GetAll()));
        }
    }
}
