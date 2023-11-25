using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lab_WT_Data.Data;
using Lab_WT_Data.Entities;
using Microsoft.AspNetCore.Hosting;

namespace AspNet_Projects.Areas_Admin
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        [ActivatorUtilitiesConstructor]
        public CreateModel(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _environment = env;
        }

        public IActionResult OnGet()
        {
            ViewData["DishGroupId"] = new SelectList(_context.Groups, "DishGroupId", "GroupName");

            return Page();
        }

        [BindProperty]
        public Dish Dish { get; set; }
        [BindProperty]
        public IFormFile Image { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            Dish.Image = Image.FileName;


            // if (!ModelState.IsValid || _context.Dishes == null || Dish == null) //! Не проходит валидацию, даже если сущность целиком заполнена
            // {

            //     return Page();
            // }

            _context.Dishes.Add(Dish);
            await _context.SaveChangesAsync();

            if (Image != null)
            {
                var fileName = $"{Dish.DishId}" +
                Path.GetExtension(Image.FileName);
                Dish.Image = fileName;
                var path = Path.Combine(_environment.WebRootPath, "Images",
                fileName);
                using (var fStream = new FileStream(path, FileMode.Create))
                {
                    await Image.CopyToAsync(fStream);
                }
                await _context.SaveChangesAsync();
            }


            return RedirectToPage("./Index");
        }
    }
}
