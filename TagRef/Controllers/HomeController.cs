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
                var all = db.References.ToList();
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
                    db.References.Add(refer);
                    db.SaveChanges();
                    List<Tag> tmp = new List<Tag>();
                    foreach (var item in refer.TagsValue.Split(','))
                    {
                        Tag tg = db.Tags.FirstOrDefault(w => w.Text == item);
                        if (tg == null)
                        {
                            tg = new Tag() { References = { db.References.First(w => w.Id == refer.Id) }, Text = item };
                            db.Tags.Add(tg);
                        }
                        else
                            db.Tags.FirstOrDefault(w => w.Text == item).References.Add(db.References.First(w => w.Id == refer.Id));
                        tmp.Add(tg);
                    }
                    refer.Tags = tmp;
                    db.Entry(refer).State = EntityState.Modified;
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
                Reference refer = db.References.Find(id);
                if (refer != null)
                {
                    return View(refer);
                }
            }
            return HttpNotFound();
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