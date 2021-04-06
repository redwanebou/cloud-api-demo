﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoAPI
{
    public class DBIntializer
    {
        public static void Initialize(CarContext context)
        {

            // create database if not exist
            context.Database.EnsureCreated();

            if (!context.Car.Any())
            {
                // Create new car
                var cr = new Car()
                {
                    Merk = "First",
                    Model = "Car",
                    Bouwjaar = 7777,
                    Brandstof = "Diesel"
                };

                context.Car.Add(cr);

                context.SaveChanges();
            }
        }
    }
}