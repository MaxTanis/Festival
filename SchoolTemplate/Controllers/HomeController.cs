﻿using System;
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
        string connectionString = "Server=172.16.160.21;Port=3306;Database=110157;Uid=110157;Pwd=crOLeran;";

        public IActionResult Index()
        { 
            return View();
        }

        private List<Festival> GetFestivals()
        {
            List<Festival> festivals = new List<Festival>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from festival", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Festival f = new Festival
                        {
                            id = Convert.ToInt32(reader["id"]),
                            naam = reader["naam"].ToString(),
                            plaats = reader["plaats"].ToString(),
                            // start_dt = DateTime.Parse(reader["start_dt"] as string)
                        };
                        festivals.Add(f);
                    }
                }
            }

            return festivals;
        }

        public IActionResult Privacy()
        {
            return View();
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

        [Route("contact")]
        [HttpPost]
        public IActionResult Contact(PersonModel model)
        {
            // als form niet goed is ingevuld
            if(!ModelState.IsValid)
                return View(model);

            // wel geod ingevuld
            SavePerson(model);

            return Redirect("/gelukt");
        }

        [Route("informatie")]
        public IActionResult Informatie()
        {
            List<Festival> products = new List<Festival>();
            products = GetFestivals();

            return View(products);
        }

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
        [Route("gelukt")]
        public IActionResult Gelukt()
        {
            return View();

        }
    }
    
}
