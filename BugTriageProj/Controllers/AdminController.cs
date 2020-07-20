using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataAccess.Models;
using BugTriage.Utils;
using BugTriage.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BugTriage.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        public AdminController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IActionResult> ManageUserClaims(string userId)
        {
            IdentityUser user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            List<Claim> allclaims = ClaimStorage.claims;
            IList<Claim> userClaims= await userManager.GetClaimsAsync(user);

            UserClaimModel userClaimModel = new UserClaimModel
            {
                UserId = userId
            };

            List<UserClaim> IssuedClaims = new List<UserClaim>();
            foreach (Claim claim in allclaims) {
                UserClaim hashTable = new UserClaim();
                hashTable.ClaimType = claim.Type;
                if (userClaims.Any(x => x.Type == claim.Type)) {
                    hashTable.IsSelected = true;
                }
                IssuedClaims.Add(hashTable);
            }
            userClaimModel.Claims = IssuedClaims;
                return View(userClaimModel);
        }
        [HttpPost]
        public async Task<IActionResult> ManageUserClaims(UserClaimModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.UserId} cannot be found";
                return View("NotFound");
            }

            List<UserClaim> updateClaims = model.Claims;
            IList<Claim> existingClaims=await userManager.GetClaimsAsync(user);
            IdentityResult result= await userManager.RemoveClaimsAsync(user, existingClaims);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing claims");
                return View(model);
            }


            IEnumerable<Claim> updateClaimList = updateClaims.Where(x => x.IsSelected).Select(s => new Claim(s.ClaimType, s.ClaimType));
            IdentityResult addresult = await userManager.AddClaimsAsync(user, updateClaimList);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected claims to user");
            }
            return View(model);
        } 
        }
}