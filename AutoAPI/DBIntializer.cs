using AutoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoAPI
{
    public class DBIntializer
    {
        public static void Initialize(MyDBContext context)
        {

            // create database if not exist
            context.Database.EnsureCreated();

            // Create new Merk
            var mr = new Merk()
            {
                Naam = "OPEL",
            };

            // Create new Model
            var mod = new Model()
            {
                Naam = "ASTRA",
                Merk = mr
            };

            var pr = new Person()
            {
                email = "boulakhrifprive@hotmail.com",
                nickname = "boulakhrifprive",
                user_id = "auth0|609188f9258a310070cb850f",
                geld = 0
            };

            // Create new car
            var cr = new Car()
            {
                Bouwjaar = 2021,
                Brandstof = "Diesel",
                prijs = 8000,
                Verkocht = false,
                Merk = mr,
                Model = mod,
                person = pr
            };

            if (!context.Car.Any())
            {
                context.Car.Add(cr);
            }

            if (!context.m1.Any())
            {
                context.m1.Add(mr);
            }

            if (!context.m2.Any())
            {
                context.m2.Add(mod);
            }
            if (!context.Person.Any())
            {
                context.Person.Add(pr);
            }

            context.SaveChanges();
        }
    }
}
