using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers
{
    //Authorize data annotation requires a user to be 
    // logged in to access anything in this controller
    [Authorize()]
    public class PostController : Controller
    {
        //Make a connection to the database
        Models.MyBlogEntities db = new Models.MyBlogEntities();

        //
        // GET: /Post/

        public ActionResult Index()
        {
            //The index will return all of the user's post       
            return View(db.Posts.Where(x => x.Username.ToLower() == User.Identity.Name.ToLower()));
        }

        // GET: /Post/Create
        public ActionResult Create()
        {
            //Pass a blank post object to the view
            return View(new Models.Post());
        }

        // POST: /Post/Create
        [HttpPost]
        public ActionResult Create(Models.Post post)
        {
            //Set the username on the post object
            post.Username = User.Identity.Name;
            //Set the date created to be Now
            post.DateCreated = DateTime.Now;
            //Set the likes to 0
            post.Likes = 0;
            //Make sure that imageURL has a value
            if (post.ImageURL == null)
            {
                post.ImageURL = "";
            }
            //Add it to the database
            db.Posts.Add(post);
            //Save our changes
            db.SaveChanges();
            //Kick user back to their list of posts
            return RedirectToAction("Index", "Post");


        }

        //GET: /Post/Delete/{id}
        [HttpGet]
        public ActionResult Delete(int id)
        {
            //Get the post from the database
            Models.Post postToDelete = db.Posts.Find(id);
            //Pass the object down to the view
            return View(postToDelete);
        }

        //POST: /Post/Delete/{id}
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            //Get the post from the database
            Models.Post postToDelete = db.Posts.Find(id);
            //Delete the post from the database
            db.Posts.Remove(postToDelete);
            //Save the changes to the database
            db.SaveChanges();
            //Redirect the user back to their post listing
            return RedirectToAction("Index", "Post");
        }

        //GET: /Post/Edit/1
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Models.Post postToEdit = db.Posts.Find(id);
            return View(postToEdit);
        }

        //POST: /Post/Edit/1
        [HttpPost]
        public ActionResult Edit(Models.Post postToEdit)
        {
            //Set the post to be updated 
            db.Entry(postToEdit).State = System.Data.EntityState.Modified;
            //Save our changes
            db.SaveChanges();
            //Kick the user back to their post list
            return RedirectToAction("Index", "Post");
        }


    }
}
