using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security; // add using for Memberships

namespace MyBlog.Controllers
{
    public class AccountController : Controller
    {
        //Add a connection to the myBlog database
        Models.MyBlogEntities db = new Models.MyBlogEntities();

        //
        // GET: /Account/

        public ActionResult Index()
        {
            //To create a user
            Membership.CreateUser("admin", "techIsFun1");

            //To check if the username and password match
            if (Membership.ValidateUser("admin", "techIsFun1"))
            {
                //user/pass is a match, log them in 
                FormsAuthentication.SetAuthCookie("admin", false);
            }
            return View();
        }

        // GET: /Account/Register
        [HttpGet]
        public ActionResult Register()
        {
            //Creating a blank registration object
            // to pass to our view
            return View(new Models.Register());
        }

        // POST: /Account/Register
        [HttpPost]
        public ActionResult Register(Models.Register reg)
        {
            //Post action recieves a Register object

            //1. Check to see if the user name is already in use
            if (Membership.GetUser(reg.Username) == null)
            {
                //Username is valid, so add the user to the database
                Membership.CreateUser(reg.Username, reg.Password);
                //Add the user to our myBlog authors table
                Models.Author author = new Models.Author();
                //Set the properties of our author 
                author.Username = reg.Username;
                author.Name = reg.Name;
                author.ImageURL = reg.ImageURL;
                //Add the author to the database
                db.Authors.Add(author);
                db.SaveChanges();
                //Log the user in
                FormsAuthentication.SetAuthCookie(reg.Username, false);
                //Kick the user back to the landing page
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //Username is already in use, send a message
                //to the view to let the user know
                ViewBag.Error = "Username is already in use, please choose another.";
                //Return the view, with the reg object
                return View(reg);
            }
        }

       // GET: /Account/Logout
        [HttpGet]
        public ActionResult Logout()
        {
            // log out the user
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Login 
        [HttpGet]
        public ActionResult Login()
        {
            return View(new Models.Login());
        }

        // POST: /Account/Login
        [HttpPost]
        public ActionResult Login(Models.Login log)
        {
            if (Membership.ValidateUser(log.Username, log.Password))
            {
                FormsAuthentication.SetAuthCookie(log.Username, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Inc orrect Username/Password. Try again.";
                return View(log);
            }
        }
    }
}
