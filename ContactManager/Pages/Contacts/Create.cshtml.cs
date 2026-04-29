using ContactManager.Data;
using ContactManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManager.Pages.Contacts
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Contact Contact { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Contacts == null || Contact == null)
            {
                return Page();
            }

            if (_context.Contacts.Any(c => c.Email == Contact.Email))
            {
                ModelState.AddModelError("Contact.Email", "Este endereço de email já está em uso.");
            }

            if (_context.Contacts.Any(c => c.ContactPhone == Contact.ContactPhone))
            {
                ModelState.AddModelError("Contact.ContactPhone", "Este número de contacto já está em uso.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Contacts.Add(Contact);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");

        }
    }
}
