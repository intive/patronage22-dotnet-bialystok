﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Models
{
    public class Status
    {
        //public Status()
        //{
        //    this.Boards = new HashSet<Board>();
        //}
        public int Id { get; set; }
        public string Code { get; set; }
        //Navigation properties 
        public virtual ICollection<BoardStatus> BoardStatuses { get; set; }
    }
}