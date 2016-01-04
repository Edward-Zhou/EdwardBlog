using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EdwardBlog.DAL;
using EdwardBlog.Models;
using PagedList;

namespace EdwardBlog.Controllers
{
    [RequireHttps]
    public class BlogsController : Controller
    {
        private BlogContext db = new BlogContext();

        // GET: Blogs
        public ActionResult Index(int? pageIndex)
        {
            decimal i = db.Blogs.Count() / 5;
            int pageSize = 5; //(int)Math.Ceiling(i);
            int pageNumber = pageIndex ?? 1;
            var blogs = db.Blogs.OrderByDescending(s=>s.PostDate);
            return View(blogs.ToPagedList(pageNumber, pageSize));
        }

        // GET: Blogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }
        //Get: Blogs/Search/SearchString
        public ActionResult Search(string SearchString)
        {
            var blogs = from b in db.Blogs
                        select b;
            if (!String.IsNullOrEmpty(SearchString))
            {
                blogs = blogs.Where(s => s.Title.Contains(SearchString));
            }

            return View("Index", blogs.ToList());
        }
        // GET: Blogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BlogId,Title,PostDate,ShortDescription,Description")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                db.Blogs.Add(blog);
                db.SaveChanges();
                return View("Details",blog);
            }

            return View(blog);
        }

        // GET: Blogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BlogId,Title,PostDate,ShortDescription,Description")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id=blog.BlogId});
            }
            return View(blog);
        }
        // GET: Blogs/Edit/5
        public ActionResult Comment(int? id)
        {                
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            BlogComment bc = new BlogComment();
            bc.Blog = blog;
            bc.Comment = new Comment();
            return View(bc);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Comment(BlogComment blogcomment)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Add(blogcomment.Comment);             
                db.SaveChanges();
                return RedirectToAction("Comment");
            }
            return View(blogcomment);
        }

        // GET: Blogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Blogs.Find(id);
            db.Blogs.Remove(blog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
