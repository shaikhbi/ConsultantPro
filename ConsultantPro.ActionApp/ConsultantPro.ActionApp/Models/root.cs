using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace ConsultantPro.ActionApp.Models
{
    [DataContract(Namespace = "")]
    public class root
    {
        [XmlAttribute]
        public string command { get; set; }
        [XmlElement]
        public PatientXMLModel[] patient { get; set; }
    }
}