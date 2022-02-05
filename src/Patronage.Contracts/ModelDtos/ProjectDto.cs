﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Patronage.Common;

namespace Patronage.Contracts.ModelDtos
{
        public class ProjectDto : ICreatable, IModifable
        {
            private int Id { get; set; }
            public string Alias { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public bool IsActive { get; set; }
            public DateTime CreatedOn { get; set; }
            public DateTime? ModifiedOn { get; set; }

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