using MovieBooking.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MovieBooking.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Page404()
        {
            return View();
        }
        public ActionResult Grid()
        {
            return View();
        }
        public ActionResult List()
        {
            return View();
        }
        public ActionResult DetailsMovie()
        {
            return View();
        }
        public ActionResult DetailsTv()
        {
            return View();
        }
        public ActionResult Help()
        {
            return View();
        }
        public ActionResult Pricing()
        {
            return View();
        }
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(string Email, string Password)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["bookingdb"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Check if user exists
                SqlCommand cmd = new SqlCommand("SP_GetDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Password", Password);

                SqlDataReader reader = cmd.ExecuteReader();
                bool exists = reader.HasRows;
                reader.Close();

                if (exists)
                {
                    Session["UserEmail"] = Email;
                    TempData["WelcomeMessage"] = "Welcome!";
                }
                else
                {
                    // Insert new user
                    SqlCommand insertCmd = new SqlCommand("SP_InsertDetails", con);
                    insertCmd.CommandType = CommandType.StoredProcedure;
                    insertCmd.Parameters.AddWithValue("@Email", Email);
                    insertCmd.Parameters.AddWithValue("@Password", Password);
                    insertCmd.ExecuteNonQuery();

                    Session["UserEmail"] = Email;
                    TempData["WelcomeMessage"] = "Account created and signed in!";
                }

                return RedirectToAction("Index", "Home");
            }
        }


        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                string cs = ConfigurationManager.ConnectionStrings["bookingdb"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("SP_InsertDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", model.Name);
                    cmd.Parameters.AddWithValue("@Email", model.Email);
                    cmd.Parameters.AddWithValue("@Password", model.Password);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                TempData["Success"] = "Registration successful!";
                return RedirectToAction("SignIn");
            }

            return View(model);
        }
    }
}