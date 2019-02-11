using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TagRef.Models;

namespace TagRef.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var db = new TagRefContext())
            {
                var all = db.References.Include(w=>w.Tags).ToList();
                return View(all);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Reference refer)
        {
            using (var db = new TagRefContext())
            {
                if (db.References.FirstOrDefault(w => w.Title == refer.Title) == null)
                {
            
                    List<Tag> tmp = new List<Tag>();
                    foreach (var item in refer.TagsValue.Split(','))
                    {
                        Tag tg = db.Tags.FirstOrDefault(w => w.Text == item);
                        if (tg == null)
                        {
                            tg = new Tag() { Text = item };
                            db.Tags.Add(tg);
                        }
                        //else
                        //{
                        //    tg.References.Remove().Where(w => w.Id == refer.Id);
                        //}
                        tmp.Add(tg);
                    }
                    refer.Tags = tmp;
                    db.References.Add(refer);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            using (var db = new TagRefContext())
            {
                Reference refer = db.References.Include(w => w.Tags).FirstOrDefault(w => w.Id == id);
                if (refer != null)
                {
                    return View(refer);
                }
            }
            return HttpNotFound();
        }
        
        [HttpPost]
        public ActionResult Edit(Reference tmp)
        {

            var a = new List<Tag>();

            using (var db = new TagRefContext())
            {
                foreach (var item in tmp.TagsValue.Split(','))
                {
                    Tag tg = db.Tags.FirstOrDefault(w => w.Text == item);
                    if (tg == null)
                    {
                        tg = new Tag() { Text = item };
                        db.Tags.Add(tg);
                    }
                    a.Add(tg);

                }
                tmp.Tags = a;

                db.Entry(tmp).State = EntityState.Modified;
                db.SaveChanges();
            }
           
            
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            using (var db = new TagRefContext())
            {
                Reference b = db.References.Find(id);
                if (b != null)
                {
                    db.References.Remove(b);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }


        public JsonResult AllTags()
        {
            using (var db = new TagRefContext())
            {
                var tags = db.Tags.Select(w => w.Text).ToList();
                return Json(tags, JsonRequestBehavior.AllowGet);

            }
        }


    }
}