using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuartzAdmin.NetCoreWeb.Models;

namespace QuartzAdmin.NetCoreWeb.Pages.Movies
{
    public class DetailsModel : PageModel
    {
        private readonly QuartzAdmin.NetCoreWeb.Models.QuartzAdminNetCoreWebContext _context;

        public DetailsModel(QuartzAdmin.NetCoreWeb.Models.QuartzAdminNetCoreWebContext context)
        {
            _context = context;
        }

        public Movie Movie { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie = await _context.Movie.SingleOrDefaultAsync(m => m.ID == id);

            if (Movie == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
