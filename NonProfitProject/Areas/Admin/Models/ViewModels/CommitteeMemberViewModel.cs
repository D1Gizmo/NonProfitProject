﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NonProfitProject.Models;

namespace NonProfitProject.Areas.Admin.Models.ViewModels
{
    public class CommitteeMemberViewModel
    {
        //used to display committee
        public Committees Committee { get; set; }
        public List<Employees> Employees { get; set; }
    }
}
