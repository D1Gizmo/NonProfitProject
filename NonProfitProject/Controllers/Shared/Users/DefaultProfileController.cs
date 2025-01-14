﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NonProfitProject.Models;
using NonProfitProject.Models.ViewModels.Shared.Users;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using NonProfitProject.Code.Security;
using System.Globalization;

namespace NonProfitProject.Controllers.Shared.Users
{
    public class DefaultProfileController : Controller
    {

        protected NonProfitContext context { get; set; }
        protected UserManager<User> userManager;
        protected SignInManager<User> signInManager;

        //Displays edit login page based on the currently logged in user
        [HttpGet]
        public async Task<IActionResult> EditLogin()
        {
            var user = await userManager.GetUserAsync(User);
            var EditLoginViewModel = new EditLoginViewModel { userID = user.Id, Email = user.Email, Username = user.UserName, ReceiveWeeklyNewsletter = user.ReceiveWeeklyNewsletter, CurrentPassword = "", NewPassword = "", NewPasswordConfirmed = "" };
            return View("EditLogin", EditLoginViewModel);
        }
        //chnages the login information if it has been changes and changes password if new password has a value and the current password is correct
        public async Task<IActionResult> EditLogin(EditLoginViewModel model)
        {
            if(User.FindFirstValue(ClaimTypes.NameIdentifier) != model.userID)
            {
                model.userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                ModelState.AddModelError("", "ERROR - Invalid UserID");    
            }
            if(model.NewPassword != null || model.NewPasswordConfirmed != null)
            { 
                if (!model.NewPassword.Equals(model.NewPasswordConfirmed))
                {
                ModelState.AddModelError("NewPassword", "Passwords do not match");
                }
            }
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.userID);
                var isPasswordCorrect = await userManager.CheckPasswordAsync(user, model.CurrentPassword);
                if (isPasswordCorrect)
                {
                    user.Email = model.Email;
                    user.UserName = model.Username;
                    user.ReceiveWeeklyNewsletter = false;
                    if (model.NewPassword != null)
                    {
                        var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                        if (!result.Succeeded)
                        {
                            foreach (IdentityError i in result.Errors)
                            {
                                ModelState.AddModelError("", i.Description);
                            }
                            return View();
                        }
                    }
                    context.Users.Update(user);
                    context.SaveChanges();
                    TempData["ChangesSaved"] = "Your login information has been Saved";
                }
                else
                {
                    ModelState.AddModelError("CurrentPassword", "Password is incorrect");
                }
            }
            return View(model);
        }
        //displays users current personal information
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            var ProfileViewModel = new ProfileViewModel { UserID = user.Id, FirstName=user.UserFirstName, LastName=user.UserLastName, Email = user.Email, Username = user.UserName, Addr1 = user.UserAddr1, Addr2 = user.UserAddr2, City = user.UserCity, PostalCode = user.UserPostalCode, State = user.UserState, BirthDate = user.UserBirthDate, Gender = user.UserGender, CreationDate=user.UserCreationDate, LastActivity=user.UserLastActivity };
            return View(ProfileViewModel);
        }
        public async Task<IActionResult> Index(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.UserID);
            }
            return View();
        }
        //displays all users payment methods if they have any
        public IActionResult ManagePayments()
        {
            var payments = context.SavedPayments.Where(sp => sp.UserID == User.FindFirstValue(ClaimTypes.NameIdentifier)).ToList();
            return View(payments);
        }
        //deletes payment method
        [HttpPost]
        public IActionResult DeletePayment(int id)
        {
            var payment = context.SavedPayments.Where(sp => sp.SavedPaymentID == id && sp.UserID == User.FindFirstValue(ClaimTypes.NameIdentifier)).FirstOrDefault();
            if(payment == null)
            {
                return RedirectToAction("ManagePayments");
            }
            context.SavedPayments.Remove(payment);
            context.SaveChanges();
            return RedirectToAction("ManagePayments");
        }

        //displays add payment method page
        [HttpGet]
        public IActionResult AddPayment()
        {
            var user = context.Users.Find(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(new SavedPaymentViewModel() {AcctFirstName = user.UserFirstName, AcctLastName = user.UserLastName,AcctAddr1 = user.UserAddr1, AcctAddr2 = user.UserAddr2, AcctCity = user.UserCity, AcctState = user.UserState, AcctPostalCode = user.UserPostalCode, BillingPostalCode = null});
        }
        
        //adds payment method based on model if the card doesnt already exists in their saved payments
        [HttpPost]
        public IActionResult AddPayment(SavedPaymentViewModel model)
        {
            //used to encrypt sensitive ionformation
            AesEncryption aes = new AesEncryption();
            //check if the user already has this card in their saved payments
            var checkSavedPayments = context.SavedPayments.Where(sp => sp.CardNumber.Equals(aes.Encrypt(model.CardNumber)) &&
                sp.UserID == User.FindFirstValue(ClaimTypes.NameIdentifier)).AsNoTracking().FirstOrDefault();
            if (checkSavedPayments != null)
            {
                ModelState.AddModelError("", "Error - This card has already been added");
            }
            if (DateTime.ParseExact(model.CardExpDate,"MM/yy", CultureInfo.InvariantCulture) < DateTime.UtcNow)
            {
                ModelState.AddModelError("CardExpDate", "This payment method has expired");
            }
            if (model.CardNumber.Length != 19)
            {
                ModelState.AddModelError("CardNumber", "This payment is incomplete");
            }
            if (model.CardCVV.Length != 3)
            {
                ModelState.AddModelError("CardCVV", "CVV requires 3 digits");
            }
            if (ModelState.IsValid)
            {
                SavedPaymentInformation savedPaymentInformation = new SavedPaymentInformation()
                {
                    UserID = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    CardholderName = model.CardholderName,
                    CardType = model.CardType,
                    CardNumber = aes.Encrypt(model.CardNumber),
                    ExpDate = model.CardExpDate,
                    CVV = aes.Encrypt(model.CardCVV),
                    BillingFirstName = model.BillingFirstName,
                    BillingLastName = model.BillingLastName,
                    BillingAddr1 = model.BillingAddr1,
                    BillingAddr2 = model.BillingAddr2,
                    BillingCity = model.BillingCity,
                    BillingState = model.BillingState,
                    BillingPostalCode = (int)model.BillingPostalCode,
                    Last4Digits = Int32.Parse(model.CardNumber[^4..])
                };
                context.SavedPayments.Add(savedPaymentInformation);
                context.SaveChanges();
                return RedirectToAction("ManagePayments");
            }
            return View(model);
        }
    }
}
