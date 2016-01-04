using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdwardBlog.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int BlogId { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.MultilineText)]
        public string CommentContent { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]     
        public DateTime CommentDate { get; set; }
    }
}