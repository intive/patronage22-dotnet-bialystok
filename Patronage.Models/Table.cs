using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Patronage.Models
{ 
    public class Table
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        [Required]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string Description { get; set; }

    }
}
