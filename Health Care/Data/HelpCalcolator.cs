using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_Care.Data
{
   static public class HelpCalcolator
    {

        static public List<string> getListOfDays(List<int> startdate, List<int> finishdat)
        {
            var Requestdates = new List<string>();

            var datesplit = startdate;
            var startdate1 = startdate[0]+ "/" + startdate[1] + "/" + startdate[2];
            //var startdate1 = startdate.ToString("d/M/yyyy");
            while (true)
            {
                Requestdates.Add(startdate1);

                if (startdate1 == finishdat[0]+"/"+ finishdat[1]+"/"+ finishdat[2])
                    break;
                else
                {
                    if (datesplit[0] != 31)
                    {
                        startdate1 = (datesplit[0] + 1) + "/" + datesplit[1] + "/" + datesplit[2];
                        datesplit[0] = datesplit[0] + 1;

                    }
                    else
                    {
                        if (datesplit[1] != 12)
                        {
                            startdate1 = 1 + "/" + (datesplit[1] + 1) + "/" + datesplit[2];
                            datesplit[0] = 1;
                            datesplit[1] = datesplit[1] + 1;
                        }
                        else
                        {
                            startdate1 = 1 + "/" + (datesplit[2] + 1);
                            datesplit[0] = 1;
                            datesplit[1] = 1;
                            datesplit[2] = datesplit[2] + 1;

                        }
                    }
                }
            }
            return Requestdates;
            //startdate.Text = datesplit[2] + "/" + datesplit[1] + "/" + datesplit[0];

        }
    }
}
