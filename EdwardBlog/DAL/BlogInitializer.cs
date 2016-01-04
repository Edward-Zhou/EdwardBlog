using EdwardBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EdwardBlog.DAL
{
    public class BlogInitializer:System.Data.Entity.DropCreateDatabaseIfModelChanges<BlogContext>
    {
        protected override void Seed(BlogContext context)
        {
            //blogs
            var blogs = new List<Blog>
            {
                new Blog{ Description="This is the first Blog", ShortDescription="First", PostDate=DateTime.Now, Title="First blog"},
                new Blog{ Description="This is the second Blog", ShortDescription="Second", PostDate=DateTime.Now, Title="Second blog"},
                new Blog{ Description="This is the third Blog", ShortDescription="Third", PostDate=DateTime.Now, Title="Third blog"}
            };
            blogs.ForEach(s => context.Blogs.Add(s));
            context.SaveChanges();
            //comments
            var comments = new List<Comment>
            {
                new Comment{BlogId=1,CommentId=1, CommentDate=DateTime.Now,CommentContent="this is first comment", UserName="User1"},
                new Comment{BlogId=1,CommentId=2, CommentDate=DateTime.Now,CommentContent="this is second comment", UserName="User2"},
                new Comment{BlogId=2,CommentId=3, CommentDate=DateTime.Now,CommentContent="this is third comment", UserName="User3"}
            };
            comments.ForEach(s=>context.Comments.Add(s));
            context.SaveChanges();
            //feedbacks
            var feedbacks = new List<Feedback>
            {
                new Feedback{UserName="T1", Content="T1", SubmitDate=DateTime.Now, Fixed=true},
                new Feedback{UserName="T2", Content="T2", SubmitDate=DateTime.Now, Fixed=true},
                new Feedback{UserName="T3", Content="T3", SubmitDate=DateTime.Now, Fixed=false}
            };
            feedbacks.ForEach(f=>context.Feedbacks.Add(f));
            context.SaveChanges();
        }
    }
}