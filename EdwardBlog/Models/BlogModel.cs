using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EdwardBlog.Models
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]     
        public DateTime PostDate { get; set; }
        [DataType(DataType.MultilineText)]
        public string ShortDescription { get; set; }
        [AllowHtml]
        public string Description { get; set; }        
        public bool VisibleFlag { get; set; }
        public virtual ICollection<Comment> Comments  { get; set; }       
    }

    public class BlogComment
    {
        public Blog Blog { get; set; }
        public Comment Comment { get; set; }
    }
}