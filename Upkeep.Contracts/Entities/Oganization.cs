using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upkeep.Contracts.Entities
{
    public class Oganization
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string RegistrationDate { get; set; }
    }
}
