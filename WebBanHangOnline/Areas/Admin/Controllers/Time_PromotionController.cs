using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;
using PagedList;
using PagedList.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class Time_PromotionController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Time_Promotion
        public ActionResult Index(int? page, string SO)
        {
            IEnumerable<Time_Promotion> items = db.Time_Promotions.ToList();
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            ViewBag.TenTieuDe = SO == "TenCombo" ? "TenCombod" : "TenCombo";
            ViewBag.NgayTao = SO == "NgayTao" ? "NgayTaod" : "NgayTao";
            ViewBag.TinhTrang = SO == "NguoiTao" ? "NguoiTaod" : "NguoiTao";
            ViewBag.Khoa = SO == "Khoa" ? "Khoad" : "Khoa";
            switch (SO)
            {
                case "TenCombo": items = items.OrderBy(x => x.Title); break;
                case "TenCombod": items = items.OrderByDescending(x => x.Title); break;
                case "NgayTao": items = items.OrderBy(x => x.CreatedDate); break;
                case "NgayTaod": items = items.OrderByDescending(x => x.CreatedDate); break;
                case "NguoiTao": items = items.OrderBy(x => x.IsActive); break;
                case "NguoiTaod": items = items.OrderByDescending(x => x.IsActive); break;
                case "Khoa": items = items.OrderBy(x => x.IsBan); break;
                case "Khoad": items = items.OrderByDescending(x => x.IsBan); break;
                default: break;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }

        public ActionResult Add()
        {
            List<Product> a = db.Products.ToList();
            ViewBag.p = a;
            return View();
        }
        [HttpPost]
        public ActionResult Add(FormCollection f, List<Product> LProduct)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
                var userManager = new UserManager<ApplicationUser>(userStore);
                var user = userManager.FindByName(User.Identity.Name);

                DateTime sd = Convert.ToDateTime(f["StartDate"]);
                sd = sd.AddHours(Convert.ToInt32(f["SHour"]));
                sd = sd.AddMinutes(Convert.ToInt32(f["SMinute"]));
                DateTime ed = Convert.ToDateTime(f["EndDate"]);
                ed = ed.AddHours(Convert.ToInt32(f["EHour"]));
                ed = ed.AddMinutes(Convert.ToInt32(f["EMinute"]));
                Time_Promotion tp = new Time_Promotion();
                tp.Title = f["TenKM"];
                tp.StartDate = sd;
                tp.EndDate = ed;
                tp.PercentValue = Convert.ToDecimal(f["PercentPrice"]);
                tp.CreatedDate = DateTime.Now;
                tp.CreatedBy = user.FullName;
                tp.ModifiedDate = DateTime.Now;
                tp.Modifiedby = user.FullName;
                tp.IsActive = false;
                tp.IsBan = false;
                var tmp = DateTime.Now;
                if (tmp >= tp.StartDate && tmp <= tp.EndDate)
                {
                    tp.IsActive = true;
                }
                db.Time_Promotions.Add(tp);
                db.SaveChanges();
                for (var i = 0; i < LProduct.Count; i++)
                {
                    var productId = LProduct[i].Id;
                    Time_Promotion_Detail tpd = new Time_Promotion_Detail();
                    tpd.ProductId = productId;
                    tpd.TimePromotionId = tp.Id;
                    db.Time_Promotion_Details.Add(tpd);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Detail(int id)
        {
            var items = db.Time_Promotions.FirstOrDefault(x => x.Id == id);
            return View(items);
        }

        [HttpPost]
        public ActionResult IsBan(int id)
        {
            var item = db.Time_Promotions.Find(id);
            if (item != null)
            {
                item.IsBan = !item.IsBan;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isBan = item.IsBan });
            }
            return Json(new { success = false });
        }
    }
}
