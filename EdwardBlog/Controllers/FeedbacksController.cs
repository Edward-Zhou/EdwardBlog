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
using System.Net.Mail;
using EdwardBlog.HelpClass;

namespace EdwardBlog.Controllers
{
    public class FeedbacksController : Controller
    {
        private BlogContext db = new BlogContext();

        // GET: Feedbacks
        public ActionResult Index()
        {
            var feedBacks = db.Feedbacks.ToList();
            var feedBackModel = new FeedbackViewModel
            {
                Feedback = new Feedback(),
                Feedbacks = feedBacks
            };
            return View(feedBackModel);
        }


        // POST: Feedbacks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,Content,SubmitDate,Fixed")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                db.Feedbacks.Add(feedback);
                db.SaveChanges();
                Helper helper = new Helper();
                helper.SendMail(feedback);     
                return RedirectToAction("Index");
            }
            return View(feedback);
        }

        // POST: Feedbacks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FeedbackViewModel feedbackViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feedbackViewModel.Feedback).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(feedbackViewModel);
        }
        [HttpPost]
        //if update multiple rows,only update the Fixex field
        public ActionResult Update(List<int> SelectedRows,bool Fixed=false)
        {
            foreach (int id in SelectedRows)
            {
                Feedback feedback = db.Feedbacks.Find(id);
                feedback.Fixed = Fixed;
                db.Entry(feedback).State = EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("Index"); 
        }

        // POST: Feedbacks/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed(List<int> SelectedRows)
        {
            foreach (int id in SelectedRows)
            {
                Feedback feedback = db.Feedbacks.Find(id);
                db.Feedbacks.Remove(feedback);
            }
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
