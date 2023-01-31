using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AspnetRunBasics.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;


namespace AspnetRunBasics
{
    public class ContactModel : PageModel
    {


        private readonly IContactRepository _contactRepository;

        public ContactModel(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
        }

        public Entities.Contact Contact { get; set; }
        public void OnGet()
        {

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> OnPostLogInAsync(string userEmail, string userPwd)
        {


            Contact = await _contactRepository.GetLogInAsync(userEmail, userPwd);

            if (Contact != null)
            {

                return RedirectToPage("Index");
            }

                ModelState.AddModelError("Error", "Check ID");
                return NotFound();
            
        }

    }
}