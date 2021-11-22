using FirebaseAdmin.Messaging;
using Health_Care.Data;
using Health_Care.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mr.Delivery.Models
{


    internal interface IScopedProcessingService
    {
        Task DoWork(CancellationToken stoppingToken);
    }

    internal class ScopedProcessingService : IScopedProcessingService
    {

        private readonly Health_CareContext _context;
        private readonly ILogger _logger;
        int num = 0;
        public ScopedProcessingService(Health_CareContext context, ILogger<ScopedProcessingService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            Console.WriteLine("CancellationToken");
            _logger.LogInformation("_logger CancellationToken");
            while (!stoppingToken.IsCancellationRequested)
            {
            //----------------------------------------------------------------------------------------------------------------------------------
                var WorkerAppointments = _context.WorkerAppointment.Where(w=> w.AcceptedByHealthWorker == false && w.cancelledByHealthWorker == false).ToList();
                foreach (var item in WorkerAppointments)
                {
                    var requst = _context.HealthWorkerRequestByUser.Where(x=>x.appointmentId ==item.id).FirstOrDefault();
                    var AppointmentDate = requst.RequestDate.Split('/');
                    var time = requst.RequestTime.Split(':');
                    DateTime date = new DateTime(Convert.ToInt32(AppointmentDate[2]), Convert.ToInt32(AppointmentDate[1]), Convert.ToInt32(AppointmentDate[0]),Convert.ToInt32(time[0]),Convert.ToInt32(time[1]),00);
                    DateTime dateNow = DateTime.UtcNow.AddHours(3);
                    DateTime dateNowLocal = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, dateNow.Hour, dateNow.Minute, 00);
                    if (date.AddMinutes(9) < dateNowLocal)
                    {
                        Patient patient = _context.Patient.FirstOrDefault(p=>p.userId == item.patientId);
                        patient.Balance += item.servicePrice;
                        _context.HealthWorkerRequestByUser.Remove(requst);
                        _context.WorkerAppointment.Remove(item);
                        _context.SaveChanges();
                    }
                }
            //----------------------------------------------------------------------------------------------------------------------------------

            //----------------------------------------------------------------------------------------------------------------------------------
                var Appointment = _context.Appointment.Where(a => a.Accepted && !a.cancelledByClinicSecretary && !a.PatientComeToAppointment && !a.Paid).ToList();
                foreach (var item in Appointment)
                {
                    List<string> Token = _context.FCMTokens.Where(f=>f.UserID == item.userId).Select(f=>f.Token).ToList();
                    var AppointmentDate = item.appointmentDate.Split('/');
                    var time = item.appointmentStartFrom.Replace('A',' ').Replace('M',' ').Replace('P',' ').Split(':');
                    _logger.LogInformation($"-------------time {time[0]} {time[1]}-------------");
                    var doctor = _context.Doctor.FirstOrDefault(d=>d.id == item.doctorId);
                    var clinic = _context.ExternalClinic.FirstOrDefault(d=>d.id == item.distnationClinicId);
                    DateTime date = new DateTime(Convert.ToInt32(AppointmentDate[2]), Convert.ToInt32(AppointmentDate[1]), Convert.ToInt32(AppointmentDate[0]), Convert.ToInt32(time[0])-2, Convert.ToInt32(time[1]), 00);
                    DateTime dateNow = DateTime.UtcNow.AddHours(3);
                    DateTime dateNowLocal = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, dateNow.Hour, dateNow.Minute, 00);

                    if (date == dateNowLocal)
                    {
                        Patient patient = _context.Patient.FirstOrDefault(p => p.userId == item.userId);
                        Notifications notification = new Notifications()
                        {
                            title = "🔔 تذكير بموعد الحجز",
                            body = $"لديك موعد بعد ساعتين في عيادة {clinic.Name} عند الطبيب {doctor.name} ",
                            isRepeated = false,
                        };
                        await SendNotificationsToListAsync(notification, Token);
                    }
                }
            //----------------------------------------------------------------------------------------------------------------------------------

            //----------------------------------------------------------------------------------------------------------------------------------
                List<string> registrationTokens = _context.FCMTokens.Select(t => t.Token).ToList();
                var notifications = await _context.Notifications.ToListAsync();
                foreach (Notifications n in notifications)
                {

                    _context.Entry(n).State = EntityState.Detached;
                    var updatedNotification = _context.Notifications.Find(n.ID);


                    num++;
                    if (DateTime.Now.TimeOfDay.ToString(@"hh\:mm") == TimeSpan.Parse(updatedNotification.time).ToString(@"hh\:mm") && updatedNotification.isRepeated)
                    {
                        await SendNotificationsToListAsync(n, registrationTokens);
                    }
                    _logger.LogInformation($"{DateTime.Now.TimeOfDay} == {TimeSpan.Parse(n.time)} {DateTime.Now.TimeOfDay == TimeSpan.Parse(n.time)}");

                }
            //----------------------------------------------------------------------------------------------------------------------------------

                await Task.Delay(58000, stoppingToken);

            }

        }

        async Task SendNotificationsToListAsync(Notifications notifications, List<string> registrationTokens, Dictionary<string, string> data = null)
        {

            for (int i = 0; i < registrationTokens.Count; i += 500)
            {
                var message = new MulticastMessage()
                {
                    Notification = new Notification()
                    {
                        Body = notifications.body,
                        Title = notifications.title

                    },
                    Data = data ?? new Dictionary<string, string> { },
                    Android = new AndroidConfig()
                    {
                        Priority = Priority.High,
                        Data = data ?? new Dictionary<string, string> { },
                        //TimeToLive = TimeSpan.FromDays(7),
                        Notification = new AndroidNotification()
                        {
                            Icon = "ic_launcher",
                            Color = "#f45342",
                            Body = notifications.body,
                            Title = notifications.title,
                        },
                    },
                    Apns = new ApnsConfig()
                    {
                        Aps = new Aps()
                        {
                            Alert = new ApsAlert() { Body = notifications.body, Title = notifications.title, },
                            Sound = "",
                            ContentAvailable = true,
                            Badge = 42,
                        },
                        Headers = new Dictionary<string, string>() {
                            {"apns-push-type", "background"},
                            { "apns-priority", "5" }, // Must be `5` when `contentAvailable` is set to true.
                            { "apns-topic", "io.flutter.plugins.firebase.messaging"} // bundle identifier
                    },
                    },

                    Tokens = registrationTokens.GetRange(i, i + 500 < registrationTokens.Count ? i + 500 : registrationTokens.Count),
                };

                var response = await FirebaseMessaging.DefaultInstance.SendMulticastAsync(message);
                // See the BatchResponse reference documentation
                // for the contents of response.
                _logger.LogInformation($"{response.SuccessCount} messages were sent successfully");
            }
        }
        // async Task SendNotificationsToUser(int userIDTo, Notifications notifications)
        //{

        //    var ToUser = _context.User.Where(u => u.id == userIDTo).FirstOrDefault();
        //    if (userIDTo == 0)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        string ToUserFCMToken = _context.FCMTokens.Where(t => t.UserID == userIDTo).FirstOrDefault().Token;

        //        {
        //            var message = new Message()
        //            {
        //                Notification = new Notification()
        //                {
        //                    Title = notifications.title,
        //                    Body = notifications.body,
        //                },
        //                Data = new Dictionary<string, string>() {
        //                //{ "rout", rout },
        //                { "message", $"{ToUser.nameAR } " },
        //            },
        //                Android = new AndroidConfig()
        //                {
        //                    Priority = Priority.High,

        //                    //TimeToLive = TimeSpan.FromDays(7),
        //                    Notification = new AndroidNotification()
        //                    {
        //                        Icon = "ic_launcher",
        //                        Color = "#f45342",
        //                    },
        //                },
        //                Apns = new ApnsConfig()
        //                {
        //                    Aps = new Aps()
        //                    {
        //                        Alert = new ApsAlert() { Body = notifications.body, Title = notifications.title, },
        //                        Sound = "",
        //                        ContentAvailable = true,
        //                        Badge = 42,
        //                    },
        //                    Headers = new Dictionary<string, string>() {
        //                    { "apns-push-type", "background" },
        //                    { "apns-priority", "5" }, // Must be `5` when `contentAvailable` is set to true.
        //                    { "apns-topic", "io.flutter.plugins.firebase.messaging" } // bundle identifier
        //                },
        //                },

        //                Token = ToUserFCMToken,
        //            };


        //            var response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
        //            // See the BatchResponse reference documentation
        //            // for the contents of response.
        //            Console.WriteLine($"{response} messages were sent successfully");
        //        }

        //        return Ok();

        //    }
        //}

    }


    //===========================================================================================================================

    public class ConsumeScopedServiceHostedService : BackgroundService
    {
        private readonly ILogger<ConsumeScopedServiceHostedService> _logger;

        public ConsumeScopedServiceHostedService(IServiceProvider services,
            ILogger<ConsumeScopedServiceHostedService> logger)
        {
            Services = services;
            _logger = logger;
        }

        public IServiceProvider Services { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Consume Scoped Service Hosted Service running.");

            await DoWork(stoppingToken);
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Consume Scoped Service Hosted Service is working.");

            using (var scope = Services.CreateScope())
            {
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<IScopedProcessingService>();

                await scopedProcessingService.DoWork(stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Consume Scoped Service Hosted Service is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}

//===========================================================================================================================
