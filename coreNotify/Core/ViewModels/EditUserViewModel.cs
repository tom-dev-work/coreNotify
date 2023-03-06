using CoreNotify.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace coreNotify.Core.ViewModels
{
    public class EditUserViewModel
    {
        public NotifyUser User { get; set;}
        public IList<SelectListItem> Roles { get; set;}
    }
}
