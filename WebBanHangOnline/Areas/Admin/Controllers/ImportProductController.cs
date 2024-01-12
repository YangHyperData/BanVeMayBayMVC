using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;
using PagedList;
using PagedList.Mvc;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    public class ImportProductController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/ImportProduct
        public ActionResult Index(int? page, string SO)
        {
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<ImportProduct> items = db.ImportProducts.OrderByDescending(x => x.CreatedDate);
            ViewBag.TenPhieu = SO == "TenCombo" ? "TenCombod" : "TenCombo";
            ViewBag.NgayTao = SO == "NgayTao" ? "NgayTaod" : "NgayTao";
            ViewBag.NguoiTao = SO == "NguoiTao" ? "NguoiTaod" : "NguoiTao";
            switch (SO)
            {
                case "TenCombo": items = items.OrderBy(x => x.Title); break;
                case "TenCombod": items = items.OrderByDescending(x => x.Title); break;
                case "NgayTao": items = items.OrderBy(x => x.CreatedDate); break;
                case "NgayTaod": items = items.OrderByDescending(x => x.CreatedDate); break;
                case "NguoiTao": items = items.OrderBy(x => x.CreatedBy); break;
                case "NguoiTaod": items = items.OrderByDescending(x => x.CreatedBy); break;
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
            ViewBag.Titles = db.Products.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Add(FormCollection f, List<ImportProductDetail> LProduct)
        {
            var title = f["TenDotNhapHang"];
            var note = f["GhiChu"];
            ImportProduct ip = new ImportProduct();
            ip.Title = title;
            ip.Note = note;
            ip.CreatedDate = DateTime.Now;
            ip.ModifiedDate = DateTime.Now;
            if (Request.IsAuthenticated)
            {
                ip.CreatedBy = User.Identity.Name;
                ip.Modifiedby = User.Identity.Name;
            }
            db.ImportProducts.Add(ip);
            db.SaveChanges();
            foreach (var item in LProduct)
            {
                Product p = db.Products.FirstOrDefault(x => x.Id == item.ProductId);
                if(p!=null)
                {
                    if (item.OriginalPrice != null)
                    {
                        p.OriginalPrice = item.OriginalPrice;
                    }
                    p.ModifiedDate = DateTime.Now;
                    Type_ClassProduct tc = db.Type_ClassProducts.FirstOrDefault(x => x.ProductId == p.Id && x.TypeTicket == item.TypeTicket && x.ClassTicket == item.ClassTicket);
                    if (tc != null)
                    {
                        tc.Quantity += item.Quantity;
                        db.SaveChanges();
                    }
                    else
                    {
                        tc = new Type_ClassProduct();
                        tc.ProductId = item.ProductId;
                        tc.Quantity = item.Quantity;
                        tc.TypeTicket = item.TypeTicket;
                        tc.ClassTicket = item.ClassTicket;
                        db.Type_ClassProducts.Add(tc);
                        db.SaveChanges();
                    } 
                }
                else
                {
                    p = new Product();
                    p.Title = item.Title;
                    p.OriginalPrice = item.OriginalPrice;
                    p.Price = decimal.Zero;
                    p.PriceSale = decimal.Zero;
                    p.ProductCategoryId = 3;
                    p.ProductCategoryId = 1;
                    p.CreatedDate = DateTime.Now;
                    p.ModifiedDate = DateTime.Now;
                    Random rd = new Random();
                    p.ProductCode = "T" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
                    p.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(p.Title);
                    db.Products.Add(p);

                    ProductImage pi = new ProductImage();
                    pi.ProductId = p.Id;
                    pi.Image = "/Uploads/images/sanpham/product_1.png"; //hình mặc định
                    pi.IsDefault = true;
                    db.ProductImages.Add(pi);

                    Type_ClassProduct tc = new Type_ClassProduct();
                    tc.ProductId = p.Id;
                    tc.Quantity = item.Quantity;
                    tc.TypeTicket = item.TypeTicket;
                    tc.ClassTicket = item.ClassTicket;
                    db.Type_ClassProducts.Add(tc);
                    db.SaveChanges();

                    ImportProductDetail ipd = new ImportProductDetail();
                    ipd.ImportProductId = ip.Id;
                    ipd.ProductId = p.Id;
                    ipd.Title = item.Title;
                    ipd.OriginalPrice = item.OriginalPrice;
                    ipd.ClassTicket = item.ClassTicket;
                    ipd.TypeTicket = item.TypeTicket;
                    ipd.Quantity = item.Quantity;
                    db.ImportProductDetails.Add(ipd);
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Products");
        }

        public ActionResult Detail(int id)
        {
            var item = db.ImportProducts.FirstOrDefault(x => x.Id == id);
            return View(item);
        }
    }
}