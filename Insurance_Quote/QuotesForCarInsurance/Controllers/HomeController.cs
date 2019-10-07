using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuotesForCarInsurance.Models;

namespace QuotesForCarInsurance.Controllers
{
    public class HomeController : Controller
    {

        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QuoteFiles;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                                                     
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Quote(string firstName, string lastName, string emailAddress, string dateOfBirth, string make, string model, string dui, string tickets, string coverOrLiability, string year)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress) || string.IsNullOrEmpty(dateOfBirth) || string.IsNullOrEmpty(year) || string.IsNullOrEmpty(make) || string.IsNullOrEmpty(model) || string.IsNullOrEmpty(dui) || string.IsNullOrEmpty(coverOrLiability))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                int baseAmount = 50;
                int userAge = Convert.ToInt32(dateOfBirth);
                int vehicleYear = Convert.ToInt32(year);
                int speedTickets = Convert.ToInt32(tickets);
                int increment = 10;


                if (userAge < 25 || userAge > 100)
                {
                    baseAmount += 25;
                }
                else if (userAge < 18)
                {
                    baseAmount += 100;
                }

                if (vehicleYear < 2000 || vehicleYear > 2015)
                {
                    baseAmount = baseAmount += 25;
                }

                if (make == "Porsche")
                {
                    baseAmount = baseAmount += 25;
                }

                if (make == "Porsche" || model == "911 Carrera")
                {
                    baseAmount = baseAmount += 25;
                }

                for (int i = 0; i < speedTickets; i++)
                {
                    baseAmount = baseAmount + increment;
                }

                if (dui == "Yes")
                {
                    baseAmount *= ((25 / 100) * baseAmount);

                }

                if (coverOrLiability == "Coverage")
                {
                    baseAmount *= ((50 / 100) * baseAmount);

                }

                string queryString = @"INSERT INTO QuoteData(FirstName, LastName, EmailAddress, DateOfBirth, Make, Model, Dui, Tickets, CoverOrLiability ,Year, TotalAmount) VALUES
                                                                   (@FirstName, @LastName, @EmailAddress, @DateOfBirth, @Make, @Model, @Dui, @Tickets, @CoverOrLiability ,@Year ,@TotalAmount)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.Add("@FirstName", SqlDbType.VarChar);
                    command.Parameters.Add("@LastName", SqlDbType.VarChar);
                    command.Parameters.Add("@EmailAddress", SqlDbType.VarChar);
                    command.Parameters.Add("@DateOfBirth", SqlDbType.VarChar);
                    command.Parameters.Add("@Make", SqlDbType.VarChar);
                    command.Parameters.Add("@Model", SqlDbType.VarChar);
                    command.Parameters.Add("@Dui", SqlDbType.VarChar);
                    command.Parameters.Add("@Tickets", SqlDbType.VarChar);
                    command.Parameters.Add("@CoverOrLiability", SqlDbType.VarChar);
                    command.Parameters.Add("@Year", SqlDbType.VarChar);
                    command.Parameters.Add("@TotalAmount", SqlDbType.VarChar);

                    command.Parameters["@FirstName"].Value = firstName;
                    command.Parameters["@LastName"].Value = lastName;
                    command.Parameters["@EmailAddress"].Value = emailAddress;
                    command.Parameters["@DateOfBirth"].Value = dateOfBirth;
                    command.Parameters["@Make"].Value = make;
                    command.Parameters["@Model"].Value = model;
                    command.Parameters["@Dui"].Value = dui;
                    command.Parameters["@Tickets"].Value = tickets;
                    command.Parameters["@CoverOrLiability"].Value = coverOrLiability;
                    command.Parameters["@Year"].Value = year;
                    command.Parameters["@TotalAmount"].Value = baseAmount;

                    

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                }

                Quotes quote = new Quotes
                {
                    TotalAmount = Convert.ToString(baseAmount)
                };
                ViewBag.Message = quote;

                
                      
                    return View("QuoteInfo");
            }

        }


        [HttpGet]
        public ActionResult Admin()
        {
            string queryString = @"SELECT Id, FirstName, LastName, EmailAddress, TotalAmount from QuoteData";

            List<Quotes> quotes = new List<Quotes>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var quote = new Quotes();
                    quote.Id = Convert.ToInt32(reader["Id"]);
                    quote.FirstName = reader["FirstName"].ToString();
                    quote.LastName = reader["FirstName"].ToString();
                    quote.EmailAddress = reader["EmailAddress"].ToString();
                    quote.TotalAmount = reader["TotalAmount"].ToString();
                    quotes.Add(quote);
                }
            }
            return View(quotes);
        }

        [HttpGet]
        public ActionResult QuoteInfo()
        {

            string queryString = @"SELECT Id, TotalAmount from QuoteData";

            List<Quotes> quotes = new List<Quotes>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var quote = new Quotes();
                    quote.Id = Convert.ToInt32(reader["Id"]);
                    quote.TotalAmount = reader["TotalAmount"].ToString();
                    quotes.Add(quote);
                }
            }

            return View();
        }
    }
}