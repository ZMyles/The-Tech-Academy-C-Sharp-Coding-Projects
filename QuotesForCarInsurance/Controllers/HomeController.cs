using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuotesForCarInsurance.Models;

namespace QuotesForCarInsurance.Controllers
{
    public class HomeController : Controller
    {

        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Quotes;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Quote(string firstName, string lastName, string emailAddress, string dateOfBirth, string make, string model, string dui, string tickets, string coverOrLiaility, string year)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress) || string.IsNullOrEmpty(dateOfBirth) || string.IsNullOrEmpty(year) || string.IsNullOrEmpty(make) || string.IsNullOrEmpty(model) || string.IsNullOrEmpty(dui) || string.IsNullOrEmpty(coverOrLiaility))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            else
            {
                string queryString = @"INSERT INTO Quotes(FirstName, LastName, EmailAddress, DateOfBirth, Make, Model, Dui, Tickets, CoverOrLiaility ,Year) VALUES
                                                                   (@FirstName, @LastName, @EmailAddress, @DateOfBirth, @Make, @Model, @Dui, @Tickets, @CoverOrLiaility ,@Year)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Parameters.Add("@FirstName", System.Data.SqlDbType.VarChar);
                    command.Parameters.Add("@LastName", System.Data.SqlDbType.VarChar);
                    command.Parameters.Add("@EmailAddress", System.Data.SqlDbType.VarChar);
                    command.Parameters.Add("@DateOfBirth", System.Data.SqlDbType.VarChar);
                    command.Parameters.Add("@Make", System.Data.SqlDbType.VarChar);
                    command.Parameters.Add("@Model", System.Data.SqlDbType.VarChar);
                    command.Parameters.Add("@Dui", System.Data.SqlDbType.VarChar);
                    command.Parameters.Add("@Tickets", System.Data.SqlDbType.VarChar);
                    command.Parameters.Add("@CoverOrLiaility", System.Data.SqlDbType.VarChar);
                    command.Parameters.Add("@Year", System.Data.SqlDbType.VarChar);

                    command.Parameters["@FirstName"].Value = firstName;
                    command.Parameters["@LastName"].Value = lastName;
                    command.Parameters["@EmailAddress"].Value = emailAddress;
                    command.Parameters["@DateOfBirth"].Value = dateOfBirth;
                    command.Parameters["@Make"].Value = make;
                    command.Parameters["@Model"].Value = model;
                    command.Parameters["@Dui"].Value = dui;
                    command.Parameters["@Tickets"].Value = tickets;
                    command.Parameters["@CoverOrLiaility"].Value = coverOrLiaility;
                    command.Parameters["@Year"].Value = year;

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                return View("QuoteInfo");
            }

        }

        [HttpPost]
             public ActionResult QuoteInfo()
        {


     //==========================================================================

            //CALCULATING THE QUOTE COAST FOR USER

            //int baseAmount = 50;
            //int userAge = Convert.ToInt32(dateOfBirth);
            //int vehicleYear = Convert.ToInt32(year);

            //if (userAge < 25 || userAge > 100)
            //{
            //    int monthlyTotal = baseAmount + 25;
            //    return monthlyTotal;
            //}
            //else if (userAge < 18)
            //{
            //    int monthlytotal = baseAmount + 100;
            //    return monthlyTotal;
            //}

       //==========================================================================
            
            return View("QuoteInfo");

        }

        [HttpGet]
        public ActionResult Admin()
        {
            string queryString = @"SELECT Id, FirstName, LastName, EmailAddress";

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

                }
            }
            return View(quotes);
        }
    }
}