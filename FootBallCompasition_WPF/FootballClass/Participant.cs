using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        //TODO конструторк и NULL
/*
        public Participant(int id, string surname, string name, string patronymic, DateTime dateOfBirth, string telephone)
        {
            this.id = id;
            this.surname = surname;
            this.name = name;
            this.patronymic = patronymic;
            this.dateOfBirth = dateOfBirth;
            this.telephone = telephone;

        }
*/
        public Participant() { }
        
        public Participant(int id, string surname, string name, string patronymic, DateTime dateOfBirth, string telephone, byte idRole)
        {
            this.Id = id;
            this.Surname = surname;
            this.Name = name;
            this.Patronymic = patronymic;
            this.DateOfBirth = dateOfBirth;
            this.Telephone = telephone;
            this.IdRole = idRole;
        }
/*
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public String Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Patronymic
        {
            get { return patronymic; }
            set { patronymic = value; }
        }

        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }

        public string Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        }

        public Role IdRole
        {
            get { return idRole; }
            set { idRole = value; }
        }
*/


        //_______________________________



        public string GetFIO()
        {
            return $"{Surname} {Name} {Patronymic}";

        }



        //TODO  
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
