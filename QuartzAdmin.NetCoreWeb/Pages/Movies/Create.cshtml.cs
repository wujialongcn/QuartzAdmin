using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuartzAdmin.NetCoreWeb.Models;

namespace QuartzAdmin.NetCoreWeb.Pages.Movies
{
    public class CreateModel : PageModel
    {
        private readonly QuartzAdmin.NetCoreWeb.Models.QuartzAdminNetCoreWebContext _context;

        public CreateModel(QuartzAdmin.NetCoreWeb.Models.QuartzAdminNetCoreWebContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Movie Movie { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Movie.Add(Movie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}