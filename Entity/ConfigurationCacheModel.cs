using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ConfigurationCacheModel
    {
        public string id { get; set; }

        public Configuration ConfigurationObj { get; set; }

        public DateTime ExpireTime { get; set; }

        public ConfigurationCacheModel(string id, Configuration configurationObj, DateTime expireTime)
        {
            this.id = id;
            this.ConfigurationObj = configurationObj;   
            this.ExpireTime = expireTime;
        }
    }
}
