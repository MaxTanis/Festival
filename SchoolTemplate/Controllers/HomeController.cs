using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SchoolTemplate.Database;
using SchoolTemplate.Models;

namespace SchoolTemplate.Controllers
{
    public class HomeController : Controller
    {
        // zorg ervoor dat je hier je gebruikersnaam (leerlingnummer) en wachtwoord invult
        string connectionString = "Server=172.16.160.21;Port=3306;Database=110074;Uid=110074;Pwd=uTuRgent;";
        //string connectionString = "Server=informatica.st-maartenscollege.nl;Port=3306;Database=110074;Uid=110074;Pwd=uTuRgent;";

        public IActionResult Index()
        {
            return View();
        }

        // functie die alle festivals ophaalt voor overzicht 
        private List<Festival> GetFestivals()
        {
            List<Festival> festivals = new List<Festival>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from festival", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Festival f = new Festival
                        {
                            id = Convert.ToInt32(reader["id"]),
                            naam = reader["naam"].ToString(),
                            plaats = reader["plaats"].ToString(),
                            image = reader["image"].ToString(),
                            start_dt = DateTime.Parse(reader["start_dt"].ToString()),
                            eind_dt = DateTime.Parse(reader["eind_dt"].ToString())

                        };
                        festivals.Add(f);
                    }
                }
            }

            return festivals;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        // word uitgevoerd nadat contact form in gepost
        [Route("contact")]
        [HttpPost]
        public IActionResult Contact(PersonModel model)
        {
            // als form niet goed is ingevuld
            if (!ModelState.IsValid)
                return View(model);

            // wel geod ingevuld
            SavePerson(model);

            return Redirect("/gelukt");
        }

        [Route("overzicht")]
        public IActionResult Overzicht()
        {
            List<Festival> festivals = new List<Festival>();
            festivals = GetFestivals();

            return View(festivals);
        }

        // slaat de gegevens op van iemand die form heeft ingevuld
        private void SavePerson(PersonModel person)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO klant(voornaam, achternaam, emailadres, geb_datum) VALUES (?voornaam, ?achternaam, ?email, ?geb_datum)", conn);

                cmd.Parameters.Add("?voornaam", MySqlDbType.VarChar).Value = person.Voornaam;
                cmd.Parameters.Add("?achternaam", MySqlDbType.VarChar).Value = person.Achternaam;
                cmd.Parameters.Add("?email", MySqlDbType.VarChar).Value = person.Email;
                cmd.Parameters.Add("?geb_datum", MySqlDbType.VarChar).Value = person.Geb_datum;
                cmd.ExecuteNonQuery();
            }

        }

        // pagina die wordt getoond nadat form geldig is ingevuld
        [Route("gelukt")]
        public IActionResult Gelukt()
        {
            return View();

        }

        [Route("informatie")]
        public IActionResult informatie()
        {
            return View();

        }

        // detail pagina voor festival
        [Route("festival/{id}")]
        public IActionResult Festival(string id)
        {
            var model = GetFestival(id);

            return View(model);
        }

        // functie om specifiek festival op te halen
        private Festival GetFestival(string id)
        {
            List<Festival> festivals = new List<Festival>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from festival where id = {id}", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Festival p = new Festival
                        {
                            id = Convert.ToInt32(reader["id"]),
                            naam = reader["naam"].ToString(),
                            plaats = reader["plaats"].ToString(),
                            image = reader["image"].ToString(),
                            start_dt = DateTime.Parse(reader["start_dt"].ToString()),
                            eind_dt = DateTime.Parse(reader["eind_dt"].ToString()),
                        };
                        festivals.Add(p);
                    }
                }
            }

            return festivals[0];
        }

    }

}
