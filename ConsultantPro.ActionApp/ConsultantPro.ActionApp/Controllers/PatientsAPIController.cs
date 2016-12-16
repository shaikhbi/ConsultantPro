using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ConsultantPro.ActionApp.Models;
using System.Text;
using System.IO;

namespace ConsultantPro.ActionApp.Controllers
{
    public class PatientsAPIController : ApiController
    {
        private ConsultantContext db = new ConsultantContext();


        [HttpPost]
        [Route("api/ExecuteCommand")]
        public IHttpActionResult ExecuteCommand(root rtCommand)
        {
            IHttpActionResult view = null;
            string[] cmd = rtCommand.command.Split('.');
            if (cmd[0].Equals("Patient"))
            {
                if (cmd[1].Equals("Save"))
                {
                    foreach(PatientXMLModel _pt in rtCommand.patient)
                    view = PostPatientFromXML(_pt);
                }
            }

            return view;
        }

        [ResponseType(typeof(PatientXMLModel))]
        [Route("api/XMLPostPatient")]
        public IHttpActionResult PostPatientFromXML(PatientXMLModel _patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Patient patient = new Patient(_patient);

            //db.Patients.Add(patient);
            //db.SaveChanges();

            writeFile(patient);

            return CreatedAtRoute("DefaultApi", new { id = patient.PatientId }, patient);
        }

        private void writeFile(Patient patient)
        {
            // Compose a string that consists of three lines.
            StringBuilder patientDetails = new StringBuilder();
            patientDetails.Append("Patient ID: " + patient.PatientId);
            patientDetails.AppendLine();
            patientDetails.Append("Patient Type: " + patient.PatientType);
            patientDetails.AppendLine();
            patientDetails.Append("Status : " + patient.status);
            patientDetails.AppendLine();
            patientDetails.Append("Date Of Birth: " + patient.DateOfBirth);
            patientDetails.AppendLine();
            patientDetails.Append("First Name: " + patient.FirstName);
            patientDetails.AppendLine();
            patientDetails.Append("Last Name: " + patient.LastName);
            patientDetails.AppendLine();
            patientDetails.Append("Phone: " + patient.phone);
            patientDetails.AppendLine();
            patientDetails.Append("Office: " + patient.office);
            patientDetails.AppendLine();
            patientDetails.Append("Cell: " + patient.cell);
            patientDetails.AppendLine();
            patientDetails.Append("Fax: " + patient.fax);
            patientDetails.AppendLine();
            patientDetails.Append("Email: " + patient.email);
            patientDetails.AppendLine();

            // Write the string to a file.
            StreamWriter writer = new StreamWriter("D://patient.txt", true);
            writer.WriteLine(patientDetails.ToString());

            writer.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PatientExists(short id)
        {
            return db.Patients.Count(e => e.PatientId == id) > 0;
        }
    }
}