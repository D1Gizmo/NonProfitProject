﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NonProfitProject.Areas.Admin.Models.ViewModels;
using NonProfitProject.Code.Security;
using NonProfitProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NonProfitProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class MembershipHistoryController : Controller
    {
        NonProfitContext context;
        public MembershipHistoryController(NonProfitContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var receipts = context.Receipts.Include(r => r.MembershipDue).ThenInclude(md => md.MembershipType).Include(r => r.InvoicePayment).Include(r => r.User).Where(r => r.Donation == null).OrderBy(r => r.Date).ToList();
            return View(receipts);
        }

        public IActionResult Details(int id)
        {
            var membership = context.Receipts.Include(r => r.MembershipDue).ThenInclude(md => md.MembershipType).Include(r => r.InvoicePayment).Include(r => r.User).Where(r => r.Donation == null && r.ReceiptID == id).OrderBy(r => r.Date).FirstOrDefault();
            if (membership == null)
            {
                return RedirectToAction("Index");
            }
            EditMembershipDueViewModel model = new EditMembershipDueViewModel()
            {
                ReceiptID = membership.ReceiptID,
                Username = membership.User.UserName,
                Total = membership.MembershipDue.MembershipType.Amount,
                FirstName = membership.User.UserFirstName,
                LastName = membership.User.UserLastName,
                Email = membership.User.Email,
                Phone = membership.User.PhoneNumber,
                MembershipType = membership.MembershipDue.MembershipType.Name,
                StartDate = membership.MembershipDue.MemStartDate,
                EndDate = membership.MembershipDue.MemEndDate,
                RenewalDate = membership.MembershipDue.MemRenewalDate,
                CardholderName = membership.InvoicePayment.CardholderName,
                CardNumber = membership.InvoicePayment.Last4Digits.ToString(),
                CardType = membership.InvoicePayment.CardType,
                ExpDate = membership.InvoicePayment.ExpDate,
                BillingFirstName = membership.InvoicePayment.BillingFirstName,
                BillingLastName = membership.InvoicePayment.BillingLastName,
                BillingAddr1 = membership.InvoicePayment.BillingAddr1,
                BillingAddr2 = membership.InvoicePayment.BillingAddr2,
                BillingCity = membership.InvoicePayment.BillingCity,
                BillingState = membership.InvoicePayment.BillingState,
                BillingPostalCode = membership.InvoicePayment.BillingPostalCode

            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var membership = context.Receipts.Include(r => r.MembershipDue).ThenInclude(md => md.MembershipType).Include(r => r.InvoicePayment).Include(r => r.User).Where(r => r.Donation == null && r.ReceiptID == id).OrderBy(r => r.Date).FirstOrDefault();
            if (membership == null)
            {
                return RedirectToAction("Index");
            }
            var editMembership = new EditMembershipDueViewModel
            {
                ReceiptID = membership.ReceiptID,
                Username = membership.User.UserName,
                Total = membership.MembershipDue.MembershipType.Amount,
                FirstName = membership.User.UserFirstName,
                LastName = membership.User.UserLastName,
                Email = membership.User.Email,
                Phone = membership.User.PhoneNumber,
                MembershipType = membership.MembershipDue.MembershipType.Name,
                StartDate = membership.MembershipDue.MemStartDate,
                EndDate = membership.MembershipDue.MemEndDate,
                RenewalDate = membership.MembershipDue.MemRenewalDate,
            };
            return View(editMembership);
        }

        [HttpPost]
        public IActionResult Edit(EditMembershipDueViewModel model)
        {
            var currentMembership = context.Receipts.Include(r => r.MembershipDue).ThenInclude(md => md.MembershipType).Include(r => r.InvoicePayment).Include(r => r.User).Where(r => r.Donation == null && r.ReceiptID == model.ReceiptID).OrderBy(r => r.Date).FirstOrDefault();
            if (!model.isChangingCardInformation)
            {
                ModelState.Remove("CardholderName");
                ModelState.Remove("CardType");
                ModelState.Remove("CardNumber");
                ModelState.Remove("ExpDate");
                ModelState.Remove("CVV");
                ModelState.Remove("BillingFirstName");
                ModelState.Remove("BillingLastName");
                ModelState.Remove("BillingAddr1");
                ModelState.Remove("BillingAddr2");
                ModelState.Remove("BillingCity");
                ModelState.Remove("BillingState");
                ModelState.Remove("BillingPostalCode");
            }
            if (ModelState.IsValid)
            {
                //sets receipt information
                currentMembership.Total = model.Total;
                //sets membership due
                currentMembership.MembershipDue.MembershipTypeID = context.MembershipTypes.Where(mt => mt.Name == model.MembershipType).FirstOrDefault().MembershipTypeID;
                currentMembership.MembershipDue.MemStartDate = model.StartDate;
                currentMembership.MembershipDue.MemEndDate = model.EndDate;
                currentMembership.MembershipDue.MemRenewalDate = model.RenewalDate;

                //billing information if it was checked to be changed
                if (model.isChangingCardInformation)
                {
                    AesEncryption aes = new AesEncryption();
                    currentMembership.InvoicePayment.CardholderName = model.CardholderName;
                    currentMembership.InvoicePayment.CardType = model.CardType;
                    currentMembership.InvoicePayment.CardNumber = aes.Encrypt(model.CardNumber);
                    currentMembership.InvoicePayment.ExpDate = model.ExpDate;
                    currentMembership.InvoicePayment.CVV = model.CVV;
                    currentMembership.InvoicePayment.Last4Digits = Int32.Parse(model.CardNumber.Substring(model.CardNumber.Length - 4));

                    currentMembership.InvoicePayment.BillingFirstName = model.BillingFirstName;
                    currentMembership.InvoicePayment.BillingLastName = model.BillingLastName;
                    currentMembership.InvoicePayment.BillingAddr1 = model.BillingAddr1;
                    currentMembership.InvoicePayment.BillingAddr2 = model.BillingAddr2;
                    currentMembership.InvoicePayment.BillingCity = model.BillingCity;
                    currentMembership.InvoicePayment.BillingState = model.BillingState;
                    currentMembership.InvoicePayment.BillingPostalCode = (int)model.BillingPostalCode;
                }
                context.Receipts.Update(currentMembership);
                context.SaveChanges();
                TempData["DonationChanges"] = String.Format("The Donation with Receipt ID {0} has been updated", currentMembership.ReceiptID);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var receipt = context.Receipts.Include(r => r.InvoicePayment).Include(r => r.InvoiceDonorInformation).Include(r => r.Donation).Where(r => r.ReceiptID == id && r.Donation == null).FirstOrDefault();
            if (receipt == null)
            {
                TempData["DonationChanges"] = String.Format("The Donation with Receipt ID {0} does not exist", id);
                return RedirectToAction("Index");
            }
            context.Receipts.Remove(receipt);
            context.SaveChanges();
            TempData["DonationChanges"] = String.Format("The Donation with Receipt ID {0} has been deleted", id);
            return RedirectToAction("Index");
        }
    }
}
