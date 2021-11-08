using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class WorkerAppointmentsCategories
    {
        public List<WorkerAppointment> ConfirmedAppointmentbyworker1 { get; set; }
        public List<WorkerAppointment> WorkerComeToAppointment1 { get; set; }
        public List<WorkerAppointment> cancelledAppointmentbyworker1 { get; set; }
        public List<WorkerAppointment> UnConfirmedAppointmentbyworker1 { get; set; }

    }
}
