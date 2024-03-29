﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootBallCompasition_WPF.FootballClass
{
    [Table("Participant")]
    public class Participant 
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string? Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Telephone { get; set; }
        public byte IdRole { get; set; }
        public Role Role { get; set; }
        public bool Active { get; set; }



        public Participant() { }

        //public Participant(int id, string surname, string name, string patronymic, DateTime dateOfBirth, string telephone, byte idRole)
        //{
        //    this.Id = id;
        //    this.Surname = surname;
        //    this.Name = name;
        //    this.Patronymic = patronymic;
        //    this.DateOfBirth = dateOfBirth;
        //    this.Telephone = telephone;
        //    this.IdRole = idRole;
        //}


        public string FullName { get; set; } = string.Empty;


        public void SetFullName()
        {
            FullName = $"{Surname} {Name} {Patronymic} {Telephone}";
        }



        //_______________________________



        public string GetFIO()
        {
            return $"{Surname} {Name} {Patronymic}";

        }



       
        public string GetAge()
        {
            return (DateTime.Now.Year - DateOfBirth.Year).ToString();

        }

        public override string ToString()
        { 
            return $"Id: {Id}; surname: {Surname}; name: {Name}; patronymic: {Patronymic}; dateOfBirth: {DateOfBirth}; telephone: {Telephone}; idRole: {IdRole}.";
        }

    }
}
