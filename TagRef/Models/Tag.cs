using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TagRef.Models;

namespace TagRef.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public virtual ICollection<Reference> References { get; set; }
        public Tag()
        {
            References = new List<Reference>();
        }
    }
}