using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfull.Domain
{
    public class Discipline
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public string Attestation { get; set; } = String.Empty;
        public Guid Hours { get; set; }
        public List<Section> Sections { get; set; } = null!;
        public Guid RegistryId;
        public Registry Registry { get; set; } = null!; 
    }
}