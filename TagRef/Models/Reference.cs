using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TagRef.Models;

namespace TagRef.Models
{
    public class Reference 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public Reference()
        {
            Tags = new List<Tag>();
        }

    }
}