using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    public class Type_ClassProductController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Type_ClassProduct
        public ActionResult Index(int id)
        {
            var items = db.Type_ClassProducts.Where(x => x.ProductId == id);
            return View(items);
        }
    }
}