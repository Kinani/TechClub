using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechClub.Models
{
    public class Applicant
    {
        [Key]
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName{ get; set; }
        public int ThebesId { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        [DisplayName("Level")]
        [Required]
        public Level? Level { get; set; }
        [DisplayName("Specialization")]
        [Required]
        public Specialization? Specialization { get; set; }
        [DisplayName("EnglishLevel")]
        [Required]
        public EnglishLevel? EnglishLevel { get; set; }
        [DisplayName("ChosenTeam")]
        [Required]
        public Team? ChosenTeam { get; set; }
        [DisplayName("Laptop")]
        [Required]
        public Laptop? Laptop { get; set; } // Bound to Developers
        [DisplayName("BasicCoding")]
        [Required]
        public BasicCoding? BasicCoding { get; set; } // Bound to Developers
        [DisplayName("AvailableOnDayX")]
        [Required]
        public DayAvail AvailableOnDayX { get; set; }
        public string GotSomethingElseToSay { get; set; }
        public string WhatDoYouThinkOfThisForm { get; set; }

    }
    public enum Team
    {
        Developers,
        Marcom
    }
    public enum Level
    {
        One,
        Two,
        Three,
        Four,
        Five
    }
    public enum Specialization
    {
        Computer_Science,
        Engineering,
        Information_Systems,
        Commerce,
        Business_Administration
    }
    public enum EnglishLevel
    {
        Low,
        Intermediate,
        Advanced,
        Proficient
    }
    public enum BasicCoding
    {
        Yes,
        No
    }
    public enum Laptop
    {
        Yes,
        No
    }
    public enum DayAvail
    {
        Yes,
        No
    }
}