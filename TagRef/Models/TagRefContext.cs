using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TagRef.Models
{
    public class TagRefContext : DbContext
    {

        public DbSet<Tag> Tags { get; set; }
        public DbSet<Reference> References { get; set; }
    }

    public class MyContextInitializer : DropCreateDatabaseAlways<TagRefContext>
    {
        protected override void Seed(TagRefContext db)
        {
           // db.Tags.AddRange(new List<Tag> { new Tag() { } });
            db.SaveChanges();
        }
    }
}