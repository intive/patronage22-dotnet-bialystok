using Patronage.Api;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Models
{
    public class Issue : ICreatable, IModifable
    {
        public int Id { get; set; }
        public string Alias { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int ProjectId { get; set; }
        public int? BoardId { get; set; }
        public int StatusId { get; set; }
        public string? AssignUserId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ApplicationUser? User { get; set; }
        public virtual List<Comment>? Comment { get; set; }
    }
}
