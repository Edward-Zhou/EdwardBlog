using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EdwardBlog.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]        
        public DateTime SubmitDate { get; set; }
        public bool Fixed { get; set; }
    }

    public class FeedbackViewModel
    {
        public List<Feedback> Feedbacks { get; set; }
        public Feedback Feedback { get; set; }
    }
}