using BSB.Data.Dto;
using BSB.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BSB.Web.Controllers
{
    public class AccountController : Controller
    {
            private readonly UserManager<MAUser> userManager;
            private readonly SignInManager<MAUser> signInManager;

            public AccountController(UserManager<MAUser> userManager,
                SignInManager<MAUser> signInManager)
            {

                this.userManager = userManager;
                this.signInManager = signInManager;
            }


            public IActionResult Register()
            {
                UserRegisterDto model = new UserRegisterDto();
                return View(model);
            }

         
            [HttpPost, AllowAnonymous]
            public async Task<IActionResult> Register(UserRegisterDto request)
            {
                if (ModelState.IsValid)
                {
                    var userCheck = await userManager.FindByEmailAsync(request.Email);
                    if (userCheck == null)
                    {
                        var user = new MAUser
                        {
                            UserName = request.Email,
                            NormalizedUserName = request.Email,
                            Email = request.Email,
                            EmailConfirmed = true,
                            PhoneNumberConfirmed = true,
                            FristName = request.FirstName,
                            LastName = request.LastName,
                            PhoneNumber = request.PhoneNumber,
                            UserFavouriteMovies = new FavouriteMovies()
                     };

                        var result = await userManager.CreateAsync(user, request.Password);
                        if (result.Succeeded)
                        {
                        await this.userManager.AddToRoleAsync(user, "User");

                        return RedirectToAction("Login");
                        }
                        else
                        {
                            if (result.Errors.Count() > 0)
                            {
                                foreach (var error in result.Errors)
                                {
                                    ModelState.AddModelError("message", error.Description);
                                }
                            }
                            return View(request);
                        }


                    }
                    else
                    {
                        ModelState.AddModelError("message", "Email already exists.");
                        return View(request);
                    }
                }
                return View(request);

            }

            [HttpGet]
            [AllowAnonymous]
            public IActionResult Login()
            {
                UserLoginDto model = new UserLoginDto();
                return View(model);
            }

            [HttpPost]
            [AllowAnonymous]
            public async Task<IActionResult> Login(UserLoginDto model)
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByEmailAsync(model.Email);
                    if (user != null && !user.EmailConfirmed)
                    {
                        ModelState.AddModelError("message", "Email not confirmed yet");
                        return View(model);

                    }
                    if (await userManager.CheckPasswordAsync(user, model.Password) == false)
                    {
                        ModelState.AddModelError("message", "Invalid credentials");
                        return View(model);

                    }

                    var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);

                    if (result.Succeeded)
                    {
                        await userManager.AddClaimAsync(user, new Claim("UserRole", "Admin"));
                        return RedirectToAction("Index", "Home");
                    }
                    else if (result.IsLockedOut)
                    {
                        return View("AccountLocked");
                    }
                    else
                    {
                        ModelState.AddModelError("message", "Invalid login attempt");
                        return View(model);
                    }
                }
                return View(model);
            }


            public async Task<IActionResult> Logout()
            {
                await signInManager.SignOutAsync();
                return RedirectToAction("Login", "Account");
            }

            

         
        }
    }

