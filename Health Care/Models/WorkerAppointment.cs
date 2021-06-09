using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Models
{
    public class WorkerAppointment
    {
        public int id { get; set; }
        public int serviceId { get; set; }
        public int workerId { get; set; }
        public int patientId { get; set; }
        public int regionId { get; set; }
        public int servicePrice { get; set; }
        public string appointmentDate { get; set; }
        public char appointmentShift { get; set; }
        public bool ConfirmHealthWorkerCome_ByPatient { get; set; }
        public bool ConfirmHealthWorkerCome_ByHimself { get; set; }
        public bool reservedAmountUntilConfirm { get; set; }
        public int PercentageFromAppointmentPriceForApp { get; set; }
        public bool AcceptedByHealthWorker { get; set; }
        public bool cancelledByHealthWorker { get; set; }
        public string cancelReasonWrittenByHealthWorker { get; set; }
    }
}
