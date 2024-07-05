using GameZone.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{
    public class DevicesService(ApplicationDBContext context) : IDevicesService
    {
        readonly private ApplicationDBContext _context = context;
        public IEnumerable<SelectListItem> GetSelectList()
        {
            return _context.Devices.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                .OrderBy(comparer => comparer.Text)
                .ToList();
        }
    }
}
