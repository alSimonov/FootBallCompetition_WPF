﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace FootBallCompasition_WPF.FootballClass
{

    [Table("Stadium")]
    public class Stadium
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int IdCity { get; set; }
        public City City { get; set; }
        public int Capacity { get; set; }
        public byte IdTypeOfСoverage { get; set; }
        public TypeOfСoverage TypeOfСoverage { get; set; }
        public byte IdTypeOfStadium { get; set; }
        public TypeOfStadium TypeOfStadium { get; set; }
        public bool Active { get; set; }

        public Stadium() { }

        
        public string StadiumAndCityName { get; set; } = string.Empty;


        public void SetStadiumAndCityName() 
        {
            StadiumAndCityName = $"{Name} ({City.Name})";
        }




        //public Stadium(int id, string name, int idCity, int capacity, byte idTypeOfСoverage, byte idTypeOfStadium)
        //{
        //    this.Id = id;
        //    this.Name = name;
        //    this.IdCity = idCity;
        //    this.Capacity = capacity;
        //    this.IdTypeOfСoverage = idTypeOfСoverage;
        //    this.IdTypeOfStadium = idTypeOfStadium;
        //}



        public string GetNameCityCapacity()
        {
            return $"{Name} {IdCity} {Capacity}";

        }

        public override string ToString()
        {
            return $"Id: {Id}; name: {Name}; idCity: {IdCity}; capacity: {Capacity}; idСoverage: {IdTypeOfСoverage}; idTypeOfStadium: {IdTypeOfStadium}.";
        }

    }
}
