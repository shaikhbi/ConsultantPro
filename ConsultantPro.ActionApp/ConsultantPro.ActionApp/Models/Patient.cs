namespace ConsultantPro.ActionApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Patient
    {
        public short PatientId { get; set; }

        public short PatientType { get; set; }
        
        public bool? status { get; set; }

        public long DateOfBirth { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string phone { get; set; }

        public string office { get; set; }

        public string cell { get; set; }

        public string fax { get; set; }

        public string email { get; set; }

        public Patient(PatientXMLModel _patient)
        {
            // for general info
            string[] generalInfo = _patient.o.Split(',');

            this.PatientId = Convert.ToInt16(generalInfo[0]);
            this.PatientType = Convert.ToInt16(generalInfo[1]);
            this.status = Boolean.Parse(generalInfo[2]);
            this.DateOfBirth = Convert.ToInt64(generalInfo[3]);

            if (_patient.contacts != null)
            {
                List<string> list = new List<string>();
                string[] secs = _patient.contacts.Split('|');

                foreach (string sec in secs)
                {
                    string[] info = sec.Split(',');
                    switch (info[0])
                    {
                        case "n1":
                            this.phone = info[1];
                            break;
                        case "n2":
                            this.office = info[1];
                            break;
                        case "n3":
                            this.cell = info[1];
                            break;
                        case "n4":
                            this.fax = info[1];
                            break;
                        case "n5":
                            this.email = info[1];
                            break;
                    }
                }
            }
            this.FirstName = _patient.first;
            this.LastName = _patient.last;
        }
    }
}
