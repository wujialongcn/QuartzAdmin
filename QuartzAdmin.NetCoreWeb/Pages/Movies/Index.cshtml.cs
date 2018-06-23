using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuartzAdmin.NetCoreWeb.Models;

namespace QuartzAdmin.NetCoreWeb.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly QuartzAdmin.NetCoreWeb.Models.QuartzAdminNetCoreWebContext _context;

        public IndexModel(QuartzAdmin.NetCoreWeb.Models.QuartzAdminNetCoreWebContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; }
        public SelectList Geners { get; set; }
        public String MovieGener { get; set; }

        public async Task OnGetAsync(String movieGener, String searchString)
        {
            IQueryable<String> generQuery = from m in _context.Movie
                                            orderby m.Genre
                                            select m.Genre;


            var movies = from m in _context.Movie
                         select m;

            if(!String.IsNullOrWhiteSpace(movieGener))
            {
                movies = movies.Where(p => p.Genre == movieGener);
            }

            if (!String.IsNullOrWhiteSpace(searchString))
            {
                movies = movies.Where(p => p.Title.Contains(searchString));
            }


            Geners = new SelectList(await generQuery.Distinct().ToListAsync());
            Movie = await movies.ToListAsync();
        }
    }
}
