using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace GetWDNumber
{
    public sealed class MyConfigSection :    ConfigurationSection
    {
        [ConfigurationProperty("partnumber")]
        public string PartNumber
        {
            get { return (string)this["partnumber"]; }
            set { this["partnumber"] = value; }
        }

        [ConfigurationProperty("startnumber")]
        public string StartNumber
        {
            get { return (string)this["startnumber"]; }
            set { this["startnumber"] = value; }
        }

        [ConfigurationProperty("pasuenumber")]
        public string PasueNumber
        {
            get { return (string)this["pasuenumber"]; }
            set { this["pasuenumber"] = value; }
        }

        [ConfigurationProperty("endnumber")]
        public string EndNumber
        {
            get { return (string)this["endnumber"]; }
            set { this["endnumber"] = value; }
        }

        [ConfigurationProperty("opertime")]
        public DateTime OperTime
        {
            get { return (DateTime)this["opertime"]; }
            set { this["opertime"] = value; }
        }

        [ConfigurationProperty("country")]
        public string Country {
            get { return (string)this["country"]; }
            set { this["country"] = value; }
        }
    }
}
