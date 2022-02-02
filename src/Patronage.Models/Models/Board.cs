using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Models
{
    public class Board
    {
        public int Id { get; set; }

        [MaxLength(256)]
        public string Alias { get; set; }

        [MaxLength(1024)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public int ProjectId { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

    }
}
