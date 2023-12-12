using RESTfull.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfull.Domain
{
    public class Section
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public Guid DisciplineId { get; set; }
        public Discipline Discipline { get; set; } = null!;
    }
}
