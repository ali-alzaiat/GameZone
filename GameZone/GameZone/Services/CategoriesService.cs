using GameZone.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{
    public class CategoriesService(ApplicationDBContext context) : ICategoriesService
    {
        readonly private ApplicationDBContext _context = context;
        public IEnumerable<SelectListItem> GetSelectList()
        {
            return _context.Categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .OrderBy(comparer => comparer.Text)
                .AsNoTracking()
                .ToList();
        }
    }
}
