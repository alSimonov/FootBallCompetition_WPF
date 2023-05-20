﻿using FootBallCompasition_WPF.FootballClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallCompasition_WPF.Short
{
    public class ParticipantShort
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public string DateOfBirth { get; set; }
        public string Telephone { get; set; }
        public string RoleName { get; set; }
        public bool Active { get; set; }


        public ParticipantShort() { }

    }
}
