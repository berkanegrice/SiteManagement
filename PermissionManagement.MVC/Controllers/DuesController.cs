using System;
using System.Linq;
using System.Linq.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PermissionManagement.MVC.Data;
using PermissionManagement.MVC.Models;
using System.Linq.Dynamic.Core;
using PermissionManagement.MVC.Models.ViewModels;

namespace PermissionManagement.MVC.Controllers
{
    public class DuesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DuesController(ApplicationDbContext context, 
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        } 
        
        public IActionResult Index()
        {
            return View();
        }

        public bool IsUserSuperAdmin(IdentityUser currentUser)
        {
            return  _userManager.IsInRoleAsync(currentUser, "SuperAdmin").Result;
        }

        public bool IsUserAdmin(IdentityUser currentUser)
        {
            return  _userManager.IsInRoleAsync(currentUser, "Admin").Result;
        }
        
        public bool IsUserBasic(IdentityUser currentUser)
        {
            return  _userManager.IsInRoleAsync(currentUser, "Basic").Result;
        }
        
        private IdentityUser GetCurrentUser()
        {
            return _userManager.GetUserAsync(HttpContext.User).Result;
        }
        
        [HttpPost]
        public JsonResult GetDuesInformation()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                var pageSize = length != null ? Convert.ToInt32(length) : 0;
                var skip = start != null ? Convert.ToInt32(start) : 0;
                var recordsTotal = 0;
            
                var duesData = GetDuesInformationData(GetCurrentUser());
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    duesData = duesData.AsQueryable()
                        .OrderBy(sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    duesData = duesData.Where(m => 
                        m.LeaseHolder.Contains(searchValue) || 
                        m.Debt.Contains(searchValue) ||
                        m.Credit.Contains(searchValue) ||
                        m.BalanceDebt.Contains(searchValue) ||
                        m.BalanceCredit.Contains(searchValue)
                    );
                }
                recordsTotal = duesData.Count();
                var data = duesData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Json(jsonData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult GetDuesDetailedInformation(int rowId)
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                var pageSize = length != null ? Convert.ToInt32(length) : 0;
                var skip = start != null ? Convert.ToInt32(start) : 0;
                var recordsTotal = 0;
            
                var duesData = GetDuesDetailedInformationData(rowId);
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    duesData = duesData.AsQueryable()
                        .OrderBy(sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    duesData = duesData.Where(m =>
                        m.AccountCode.Contains(searchValue) ||
                        m.Debt.Contains(searchValue) ||
                        m.Credit.Contains(searchValue) ||
                        m.BalanceDebt.Contains(searchValue) ||
                        m.BalanceCredit.Contains(searchValue)
                    );
                }
                recordsTotal = duesData.Count();
                var data = duesData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Json(jsonData);
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }

        private IQueryable<DuesDetailedInformation> GetDuesDetailedInformationData(int rowId)
        {
            var duesDetailedData = (
                from d in _context.DuesInformations.Where(x=>x.Id.Equals(rowId))
                join dd in _context.DuesDetailedInformations on d.AccountCode equals dd.AccountCode
                select new DuesDetailedInformation()
                {
                    Id = dd.Id,
                    AccountCode = d.AccountCode,
                    Date = dd.Date,
                    Detail = dd.Detail,
                    Debt = dd.Debt,
                    Credit = dd.Credit,
                    BalanceDebt = dd.BalanceDebt,
                    BalanceCredit = dd.BalanceCredit
                }
            );
            return duesDetailedData;
        }

        private IQueryable<DuesInformationViewModel> GetDuesInformationData(IdentityUser currentUser)
        {
            var superAdmin = IsUserSuperAdmin(currentUser);
            var admin = IsUserAdmin(currentUser);
            if (superAdmin || admin)
            {
                var duesData = (
                    from d in _context.DuesInformations
                    join u in _context.UsersModel on d.AccountCode equals u.AccountCode 
                    select new DuesInformationViewModel()
                    {
                        Id = d.Id,
                        LeaseHolder = u.AccountName,
                        Debt = d.Debt,
                        Credit = d.Credit,
                        BalanceDebt = d.BalanceDebt,
                        BalanceCredit = d.BalanceCredit
                    }
                );
                return duesData;
            }
            else
            {
                var duesData = (
                    from d in _context.DuesInformations
                    join u in _context.UsersModel.Where(x => x.Email == currentUser.Email)
                        on d.AccountCode equals u.AccountCode
                    
                    select new DuesInformationViewModel()
                    {
                        Id = d.Id,
                        LeaseHolder = u.AccountName,
                        Debt = d.Debt,
                        Credit = d.Credit,
                        BalanceDebt = d.BalanceDebt,
                        BalanceCredit = d.BalanceCredit
                    }
                );
                return duesData;
            }
        }
    }
}