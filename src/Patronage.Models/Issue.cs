﻿using Patronage.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Models
{
    public class Issue : ICreatable, IModifable
    {
        public int Id { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public int? BoardId { get; set; }
        public int StatusId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public void OnCreate()
        {
            CreatedOn = DateTime.UtcNow;
        }

        public void OnModify()
        {
            ModifiedOn = DateTime.UtcNow;
        }
    }
}
