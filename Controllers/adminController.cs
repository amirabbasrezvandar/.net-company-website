using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sherkaty.Models;
using sherkaty.Models.ViewModels;
using System.Data.Entity;
using System.IO;



namespace sherkaty.Controllers
{
    public class adminController : Controller
    {
        s1 con = new s1();
        // GET: admin
        public ActionResult dash()
        {
            return View();
        }

        public ActionResult login()
        {
            return View();
        }






        //TTTTTTEEEEEEEAAAAAAAMMMMMMMMMM
        public ActionResult edteam(int page = 1)
        {
            var totalEmployees = con.tbl_coll.Count();
            var totalPages = (int)Math.Ceiling((double)totalEmployees / 6);

            ViewBag.TeamMembers = con.tbl_coll
                               .OrderBy(x => x.coid)
                               .Skip((page - 1) * 6)
                               .Take(6)
                               .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.totalPages = totalPages;

            return View();
        }





        public ActionResult edteam_single(int id)
        {
            var coll = con.tbl_coll.Find(id);
            if (coll == null) return HttpNotFound();
            return View(coll);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult edteam_single(tbl_coll model, HttpPostedFileBase ImageUpload)
        {
            var c = con.tbl_coll.Find(model.coid);
            if (c == null) return HttpNotFound();

            if (ModelState.IsValid)
            {
                // update text fields
                c.coname = model.coname;
                c.cofamily = model.cofamily;
                c.coposition = model.coposition;
                c.coabout = model.coabout;
                c.coemail = model.coemail;
                c.cophnumber = model.cophnumber;
                c.coexperience = model.coexperience;
                c.colocation = model.colocation;
                c.coexpertise = model.coexpertise;
                c.coinfo = model.coinfo;

                if (ImageUpload != null && ImageUpload.ContentLength > 0)
                {
                    string folder = Server.MapPath("~/styles/home/images/");
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);

                  
                    string extension = Path.GetExtension(ImageUpload.FileName);
                    string fileName = $"team-{model.coid}-{DateTime.Now.Ticks}{extension}";
                    string fullPath = Path.Combine(folder, fileName);

                    ImageUpload.SaveAs(fullPath);

              
                    c.coimgpth = fileName;
                }


                con.SaveChanges();
                return RedirectToAction("edteam"); 
            }

            return View(model);
        }




        public ActionResult add_team()
        {
            return View();
                 
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult add_team([Bind(Include = "coname,cofamily,coposition,coabout,coemail,cophnumber,coexperience,colocation,coinfo,coexpertise")] tbl_coll newColleague)
        {

            if (ModelState.IsValid)
            {
                con.tbl_coll.Add(newColleague);
                con.SaveChanges();
                return RedirectToAction("edteam");

            }
            return View(newColleague);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult delteam(int id)
        {
            var colleague = con.tbl_coll.Find(id);
            con.tbl_coll.Remove(colleague);
            con.SaveChanges();
            return RedirectToAction("edteam");

        }





        //BBBBBBLLLLLLOOOOOOOGGGGGGGG

        public ActionResult edblog(int page = 1)
        {
            var totalblogs = con.tbl_blog.Count();
            var totalPages = (int)Math.Ceiling((double)totalblogs / 6);

            ViewBag.blogs = con.tbl_blog.OrderBy(x => x.bid)
                               .Skip((page - 1) * 6)
                               .Take(6)
                               .ToList();
            ViewBag.CurrentPage = page;
            ViewBag.totalPages = totalPages;
            return View();
        }


        public ActionResult edblog_single(int id)
        {
            var blog = con.tbl_blog.Find(id);
            if(blog == null) return HttpNotFound();
            return View(blog);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult edblog_single(tbl_blog model , HttpPostedFileBase ImageUpload)
        {

            var c = con.tbl_blog.Find(model.bid);
            if (c == null) return HttpNotFound();
            if (ModelState.IsValid)
            {
                c.btitle = model.btitle;
                c.firsttext = model.firsttext;
                c.bold = model.bold;
                c.sectext = model.sectext;
                c.sectitle = model.sectitle;
                c.thtext = model.thtext;
                c.c1 = model.c1;
                c.c2 = model.c2;
                c.c3 = model.c3;
                c.c4 = model.c4;
                c.c5 = model.c5;
                c.dateday = model.dateday;
                c.datemon = model.datemon;
                c.dateye = model.dateye;


                if (ImageUpload != null && ImageUpload.ContentLength > 0)
                {
                    string folder = Server.MapPath("~/styles/home/images/");
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);


                    string extension = Path.GetExtension(ImageUpload.FileName);
                    string fileName = $"blog-{model.bid}-{DateTime.Now.Ticks}{extension}";
                    string fullPath = Path.Combine(folder, fileName);

                    ImageUpload.SaveAs(fullPath);


                    c.img = fileName;
                }

                con.SaveChanges();
                return RedirectToAction("edblog");
            }
            return View(model);

        }





        public ActionResult add_blog()
        {
            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult add_blog([Bind(Include = "dateye,datemon,dateday,ltext,c5,c4,c3,c2,c1,thtext,sectitle,sectext,bold,firsttext,btitle")]tbl_blog bl)
        {
            if (ModelState.IsValid)
            {
                con.tbl_blog.Add(bl);
                con.SaveChanges();
                return RedirectToAction("edblog");

            }
            return View(bl);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult delblog(int id)
        {
            var blog = con.tbl_blog.Find(id);
            con.tbl_blog.Remove(blog);
            con.SaveChanges();
            return RedirectToAction("edblog");

        }



        //SSSSSSSSEEEEEEEEVVVVVVVVVVIIIIIIIIIICCCCCCCCCEEEEEEEEEESSSSSSSSSSSS

        public ActionResult edservice(int page=1)
        {
            int totalservs = con.tbl_service.Count();
            var totalPages = (int)Math.Ceiling((double)totalservs / 6);
            ViewBag.servs = con.tbl_service.OrderBy(a => a.sid).Skip((page - 1) * 6)
                .Take(6).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.totalPages = totalPages;



            ViewBag.service = con.tbl_service.ToList();
            return View();

        }



        public ActionResult edservice_single(int id)
        {

            var service = con.tbl_service.FirstOrDefault(x => x.sid == id);
            var secon = con.tbl_service_second.FirstOrDefault(x => x.srcid == id);
            var card = con.tbl_service_card.FirstOrDefault(x => x.scid == id);
            var fea = con.tbl_serivice_fea.FirstOrDefault(x => x.ssid == id);


            var viewModel = new ServiceSingleViewModel
            {

                service = service,
                secon = secon,
                card = card,
                fea = fea,


            };
            return View(viewModel);
        }
    }

}