using BulkyBookWeb_Temp.Data;
using BulkyBookWeb_Temp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyBookWeb_Temp.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public List<Category> CategoryList { get; set; }
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            CategoryList = _db.Categories.ToList();
        }
    }
}
