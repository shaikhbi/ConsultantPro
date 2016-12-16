using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace ConsultantPro.ActionApp.Models
{
    [XmlType(TypeName = "patient")]
    [DataContract(Namespace ="")]
    public class PatientXMLModel
    {
        [XmlAttribute]
        public string o { get; set; }

        public string first { get; set; }

        public string last { get; set; }

        public string contacts { get; set; }
        
    }
}