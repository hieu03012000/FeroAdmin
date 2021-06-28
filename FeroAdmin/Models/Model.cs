using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeroAdmin.Models
{
    public class Model
    {
        public string Id { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
        public byte Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string SubAddress { get; set; }
        public string Phone { get; set; }
        public string Gifted { get; set; }
        public bool Status { get; set; }
    }
}
