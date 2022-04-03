using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class AccountController : Controller
    {
        //private readonly BankContext _context;
        //private readonly IApplicationUserRepository _applicationUserRepository;
        //private readonly IUserMapper _userMapper;
        //private readonly IAccountRepository _accountRepoitory;
        //private readonly IAccountMapper _accountMapper;
        //public AccountController(BankContext context, IApplicationUserRepository applicationUserRepository, IUserMapper userMapper, IAccountRepository accountRepoitory, IAccountMapper accountMapper)
        //{
        //    _context = context;
        //    _applicationUserRepository = applicationUserRepository;
        //    _userMapper = userMapper;
        //    _accountRepoitory = accountRepoitory;
        //    _accountMapper = accountMapper;
        //}

        //private readonly IRepository<Account> _accountRepository;
        //private readonly IRepository<ApplicationUser> _userRepository;

        //public AccountController(IRepository<Account> accountRepository, IRepository<ApplicationUser> userRepository)
        //{
        //    _accountRepository = accountRepository;
        //    _userRepository = userRepository;
        //}

        private readonly IUow _uow;

        public AccountController(IUow uow)
        {
            _uow = uow;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create(int id)
        {
            //var appUser = _context.ApplicationUsers.Select(x=>new UserListModel { 
            //Id=x.Id,
            //Name=x.Name,
            //Surname=x.Surname

            //}).SingleOrDefault(x => x.Id == id);

            //return View(appUser);

            var userInfo = _uow.GetRepository<ApplicationUser>().GetById(id);
            return View(new UserListModel
            {
                Id = userInfo.Id,
                Name = userInfo.Name,
                Surname = userInfo.Surname,


            });
        }

        [HttpPost]
        public IActionResult Create(AccountCreateModel model)
        {
            _uow.GetRepository<Account>().Create(new Account
            {
                Balance = model.Balance,
                ApplicationUserId = model.ApplicationUserId,
                AccountNumber = model.AccountNumber
            });
            _uow.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult GetByUserId(int userId)
        {
            var query = _uow.GetRepository<Account>().GetQueryable();
            var accounts = query.Where(x => x.ApplicationUserId == userId).ToList();
            var user = _uow.GetRepository<ApplicationUser>().GetById(userId);
            var list = new List<AccountListModel>();
            ViewBag.FullName = user.Name + " " + user.Surname;

            foreach (var account in accounts)
            {
                list.Add(new AccountListModel
                {
                    Id = account.Id,
                    AccountNumber = account.AccountNumber,
                    Balance = account.Balance,
                    ApplicationUserId = account.ApplicationUserId,

                });

            }

            return View(list);
        }
        [HttpGet]
        public IActionResult SendMoney(int accountId)
        {
            var query = _uow.GetRepository<Account>().GetQueryable();
            var accounts = query.Where(x => x.Id != accountId).ToList();
            
                
            var list = new List<AccountListModel>();
            ViewBag.SenderId = accountId;

            foreach (var account in accounts)
            {
                list.Add(new AccountListModel { AccountNumber = account.AccountNumber, ApplicationUserId = account.ApplicationUserId, Balance = account.Balance, Id = account.Id });
            }
            var list2 = new SelectList(list,"Id","AccountNumber");
            return View(list2);
        }
        [HttpPost]
        public IActionResult SendMoney(SendMoneyModel model)
        {
            var senderAccount = _uow.GetRepository<Account>().GetById(model.SenderId);
           senderAccount.Balance= senderAccount.Balance - model.Amount;
            _uow.GetRepository<Account>().Update(senderAccount);

            
           var account= _uow.GetRepository<Account>().GetById(model.AccountId);
            account.Balance += model.Amount;
            _uow.GetRepository<Account>().Update(account);
            _uow.SaveChanges();

            return RedirectToAction("Index","Home");
        
        }

    }
}
