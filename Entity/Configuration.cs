using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entity
{
    public class Configuration : EntityBase
    {
        public string Name { get; set; }
        public string Type { get; set; }    
        public string Value { get; set; }
        public bool IsActive { get; set; }
        public string ApplicationName { get; set; }
    }
}
