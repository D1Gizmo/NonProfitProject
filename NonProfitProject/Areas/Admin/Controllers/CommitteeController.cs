﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NonProfitProject.Areas.Admin.Models.ViewModels;
using NonProfitProject.Models;
using NonProfitProject.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Areas.Admin.Controllers
{
    //If user is admin, shows this page.
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("[area]/[controller]s/[action]/{id?}")]
    public class CommitteeController : Controller
    {
        private readonly NonProfitContext context;
        public CommitteeController(NonProfitContext context)
        {
            this.context = context;
        }

        //displays all the committees in the database with members
        [Route("~/[area]/[controller]s")]
        public IActionResult Index()
        {
            var model = context.Committees.Include(c => c.committeeMembers).ThenInclude(cm => cm.employee).ThenInclude(e => e.User).ToList();
            return View(model);
        }

        //gets committee details and sets it as a session object to be used to display the name and details if the committee is not null
        [Route("~/[area]/[controller]/{name}")]
        public IActionResult Details(string name)
        {
            var committee = context.Committees?.Include(c => c.committeeMembers).ThenInclude(cm => cm.employee).ThenInclude(e => e.User).Where(c => c.CommitteeName.Equals(name)).AsNoTracking().FirstOrDefault();
            if (committee == null)
            {
                return RedirectToAction("Index");
            }
            //creates session model
            var CommitteeMemberModel = new CommitteeMemberViewModel() { Committee = context.Committees?.Where(c => c.CommitteeName == name).AsNoTracking().FirstOrDefault(), Employees = context.Employees.Include(e => e.User).Where(e => !context.CommitteeMembers.Any(cm => e.EmpID == cm.EmpID)).AsNoTracking().ToList() };
            HttpContext.Session.SetObject("CommitteeMemberModel", CommitteeMemberModel);
            return View(committee);
        }

        //displays current committee member details and resets the session object
        [Route("~/[area]/[controller]/{name}/[action]/{id}")]
        public IActionResult MemberDetails(string id)
        {
            var employee = context.Employees.Include(e => e.User).Include(e => e.CommitteeMembers).Where(e => e.EmpID == id).FirstOrDefault();
            var sessionmodel = HttpContext.Session.GetObject<CommitteeMemberViewModel>("CommitteeMemberModel");
            if (employee == null || sessionmodel == null)
            {
                TempData["AddCommitteeMemberTimeout"] = "Session Timeout";
                return RedirectToAction("Index");
            }
            ViewBag.Action = "MemberDetails";
            //resets the session time
            HttpContext.Session.SetObject("CommitteeMemberModel", sessionmodel);
            return View("EmployeeDetails", employee);
        }

        //reecives position in the route and gets the member through the route as well. Alows the admin to change the Committee position of the member
        [HttpPost]
        [Route("~/[area]/[controller]/[action]/{id}/{name}/{position}")]
        public IActionResult EditPosition(string id,string name, string position)
        {
            var member = context.CommitteeMembers.Include(cm => cm.employee).ThenInclude(e => e.User).Where(cm => cm.EmpID == id).FirstOrDefault();
            if(member == null || position.Length == 0)
            {
                return RedirectToAction("Index");
            }
            member.CommitteePosition = position;
            context.CommitteeMembers.Update(member);
            context.SaveChanges();
            TempData["CommitteeMemberChanges"] = String.Format("{0}'s position has been updated", member.employee.User.UserFirstName + " " + member.employee.User.UserLastName);
            return RedirectToAction("Details", new { name = name });
        }
        //checks to see if member exists. if it doesn't, it removes it from the committee
        [HttpPost]
        public IActionResult RemoveMember(string id)
        {
            var employee = context.Employees.Include(e => e.User).Include(e => e.CommitteeMembers).Where(e => e.EmpID == id).AsNoTracking().FirstOrDefault();
            var committeeID = employee.CommitteeMembers.CommitteeID;
            if(employee == null)
            {
                return RedirectToAction("Index");
            }
            TempData["CommitteeMemberChanges"] = String.Format("{0} has been removed from the committee", employee.User.UserFirstName + " " + employee.User.UserLastName);
            context.CommitteeMembers.Remove(employee.CommitteeMembers);
            context.SaveChanges();
            
            return RedirectToAction("Details", new { name = context.Committees.Find(committeeID).CommitteeName });
        }

        //displays the add members page by getting employees from the database that are not in other committtee and dont have a release date
        [Route("~/[area]/[controller]/{name}/[action]")]
        [HttpGet]
        public IActionResult AddMembers(string name)
        {
            var CommitteeMemberModel = new CommitteeMemberViewModel() { Committee = context.Committees?.Where(c => c.CommitteeName == name).AsNoTracking().FirstOrDefault(), Employees = context.Employees.Include(e => e.User).Where(e => !context.CommitteeMembers.Any(cm => e.EmpID == cm.EmpID) && e.ReleaseDate == null).AsNoTracking().ToList() };
            HttpContext.Session.SetObject("CommitteeMemberModel", CommitteeMemberModel);
            return View();
        }

        //adds member based on routed id and position name if model is valid
        [HttpPost]
        [Route("~/[area]/[controller]/{name}/[action]/{id}/{position}/")]
        public IActionResult AddMembers(string id, string position)
        {
            var sessionmodel = HttpContext.Session.GetObject<CommitteeMemberViewModel>("CommitteeMemberModel");
            if (sessionmodel == null)
            {
                TempData["AddCommitteeMemberTimeout"] = "Session Timeout";
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                var committeeMember = new CommitteeMembers()
                {
                    CommitteeID = context.Committees.Where(c => c.CommitteeName == sessionmodel.Committee.CommitteeName).Select(c => c.CommitteesID).FirstOrDefault(),
                    EmpID = id,
                    CommitteePosition = position
                };
                context.CommitteeMembers.Add(committeeMember);
                context.SaveChanges();
                return RedirectToAction("AddMembers", new { name = sessionmodel.Committee.CommitteeName , id = ""});
            }
            return RedirectToAction("AddMembers", new { name = sessionmodel.Committee.CommitteeName, id = "" });
        }

        //displays details of employees (whether they are in a committee or not)
        [Route("~/[area]/[controller]/{name}/AddMembers/[action]/{id}")]
        public IActionResult EmployeeDetails(string id)
        {
            var employee = context.Employees.Include(e => e.User).Where(e => !context.CommitteeMembers.Any(cm => e.EmpID == cm.EmpID) && e.EmpID == id).FirstOrDefault();
            var sessionmodel = HttpContext.Session.GetObject<CommitteeMemberViewModel>("CommitteeMemberModel");
            if (employee == null || sessionmodel == null)
            {
                TempData["AddCommitteeMemberTimeout"] = "Session Timeout";
                return RedirectToAction("Index");
            }
            //resets the session time
            HttpContext.Session.SetObject("CommitteeMemberModel", sessionmodel);
            return View(employee);
        }
        //displays add committee page
        public IActionResult AddCommittee()
        {
            ViewBag.Action = "Add";
            return View("EditCommittee", new Committees());
        }
        //displays edit committee by using routed id
        [HttpGet]
        public IActionResult EditCommittee(int id)
        {
            ViewBag.Action = "Edit";
            var Event = context.Committees.Find(id);
            if (Event == null)
            {
                return RedirectToAction("Index");
            }
            return View(Event);
        }
        //submits the chnages to edit the committee if there is an id. if not it adds a committee(used to make adding and editing easier)
        [HttpPost]
        public IActionResult EditCommittee(Committees model)
        {
            if (ModelState.IsValid)
            {
                string addOrEdit;
                if (model.CommitteesID == 0)
                {
                    model.CommitteeCreationDate = DateTime.Now;
                    context.Committees.Add(model);
                    addOrEdit = "added";
                }
                else
                {
                    var committee = context.Committees.Include(c => c.committeeMembers).Where(c => c.CommitteesID == model.CommitteesID).AsNoTracking().FirstOrDefault();
                    if(committee == null)
                    {
                        ModelState.AddModelError("", "Committee with ID  \"" + model.CommitteesID + "\" does not exist");
                        return View();
                    }
                    model.committeeMembers = committee.committeeMembers;
                    model.CommitteeCreationDate = committee.CommitteeCreationDate;
                    context.Committees.Update(model);
                    addOrEdit = "updated";
                }
                context.SaveChanges();
                TempData["CommitteeChanges"] = String.Format("The \"{0}\" has been {1}.", model.CommitteeName, addOrEdit);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Action = (model.CommitteesID == 0) ? "Add" : "Edit";
                return View(model);
            }
        }
        //deletes committee based on id
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var committee = context.Committees.Include(c => c.committeeMembers).Where(c => c.CommitteesID == id).FirstOrDefault();
            if (committee == null)
            {
                return RedirectToAction("Index");
            }
            foreach(var member in committee.committeeMembers)
            {
                context.CommitteeMembers.Remove(member);
            }
            context.Committees.Remove(committee);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
