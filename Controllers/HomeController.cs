using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sherkaty.Models;
using sherkaty.Models.ViewModels;





namespace sherkaty.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        s1 con = new s1();
        public ActionResult Index()
        {

            ViewBag.blog = con.tbl_blog.OrderBy(u => u.bid).Take(4).ToList();

            ViewBag.service = con.tbl_service.OrderBy(a => a.sid).Take(6).ToList();
            return View();
        }



        public ActionResult team(int page = 1)
        {
            int pageSize = 6;
            var totalEmployees = con.tbl_coll.Count();
            var totalPages = (int)Math.Ceiling((double)totalEmployees / pageSize);

            ViewBag.TeamMembers = con.tbl_coll
                               .OrderBy(x => x.coid)
                               .Skip((page - 1) * pageSize)
                               .Take(pageSize)
                               .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.totalPages = totalPages;

            return View();
        }




        public ActionResult team_single(int id)
        {
            ViewBag.coll = con.tbl_coll.ToList();
            var coll = con.tbl_coll.FirstOrDefault(x => x.coid == id);
            if (coll == null)
            {

                return View("error");
            }

            return View(coll);
        }









        public ActionResult services(int page = 1)
        {
    
            int totalservs = con.tbl_service.Count();
            var totalpages = (int)Math.Ceiling((double)totalservs / 6);
            ViewBag.servs = con.tbl_service.OrderBy(a => a.sid).Skip((page - 1) * 6)
                .Take(6).ToList();

            ViewBag.page = page;
            ViewBag.totalpages = totalpages;



            ViewBag.service = con.tbl_service.ToList();
             return View();
        }







        public ActionResult service_single(int id)
        {
            

            var service = con.tbl_service.FirstOrDefault(x => x.sid == id);
            var secon = con.tbl_service_second.FirstOrDefault(x => x.srcid == id);
            var card = con.tbl_service_card.FirstOrDefault(x => x.scid == id);
            var fea = con.tbl_serivice_fea.FirstOrDefault(x => x.ssid == id);

            var see = con.tbl_service.Take(6)
                .ToList();


            var viewModel = new ServiceSingleViewModel
            {

                service = service,
                secon = secon,
                card = card,
                fea = fea,

                see = see


            };
            return View(viewModel);

        }





        public ActionResult about()
        {
            ViewBag.coll = con.tbl_coll
                .OrderBy(a => a.coid)
                .Take(3)
                .ToList();
            return View();

        }



        public ActionResult blog(int page =1)
        {
            int totalblogs = con.tbl_service.Count();
            var totalpages = (int)Math.Ceiling((double)totalblogs / 6);

            ViewBag.page = page;
            ViewBag.totalpages = totalpages;

            ViewBag.blog = con.tbl_blog.OrderBy(u => u.bid).Take(6).ToList();

            return View();
        }




        public ActionResult blog_single(int id)
        {
            var bl = con.tbl_blog.FirstOrDefault(a => a.bid == id);
            return View(bl);
        }








        public ActionResult contact()
        {
            return View();
        }








        public ActionResult faqs()
        {
            return View();
        }






        public ActionResult testimonial()
        {
            return View();
        }






        public ActionResult home_layout()
        {
            return View();
        }

    }
}