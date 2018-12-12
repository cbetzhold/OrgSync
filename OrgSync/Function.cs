using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace OrgSync
{
    public class Function
    {
        private static List<Events> Calendar = new List<Events>();

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>

        //public static string[] calendarLines = System.IO.File.ReadAllLines(@"http://oumisprojects.com/201810/MISSA_Dates_Events.csv");

        //private static HttpClient httpClient;

        //public Function()
        //{
        //    httpClient = new HttpClient();
        //}




        //Dictionary<string, DateTime> EventsDic = new Dictionary<string, DateTime>()
        //{
        //    {"MIS Lunch and Learn", DateTime.Today},
        //    {"Banquet", DateTime.Today.AddDays(1)},
        //    {"General Meeting 1",DateTime.Today.AddDays(2)},
        //    {"MIS Lunch and Learn 2",DateTime.Today.AddDays(3)},
        //    {"General Meeting 3",DateTime.Today.AddDays(4)},
        //    {"Lunch and Learn 3",DateTime.Today.AddDays(5)},
        //    {"General Meeting 2",DateTime.Today.AddDays(6)}
        //};


        //List<Events> Calendar = new List<Events>();
        //private void Form1(object sender, EventArgs e)
        //{
        //    Calendar.Add(new Events("MIS Lunch and Learn 1", "Adams 2030", Convert.ToDateTime(12 / 19 / 2018)));
        //    Calendar.Add(new Events("MIS Lunch and Learn 2", "Adams 2030", Convert.ToDateTime(12 / 11 / 2018)));

        //}

        //Events Event1 = new Events("MIS Lunch and Learn", "Adams 2030", Convert.ToDateTime(12 / 19 / 2018));




        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            string outputText = "";
            var requestType = input.GetRequestType();
            var intent = input.Request as IntentRequest;

            if (requestType == typeof(LaunchRequest))
            {
                return BodyResponse("Welcome to the Connect to OrgSync skill!", false);
            }

            else if (requestType == typeof(IntentRequest))
            {
                //outputText += "Request type is Intent";
                var intentName = input.Request as IntentRequest;
                
                if (intent.Intent.Name.Equals("OrgSyncIntent"))
                {
                   return BodyResponse( "IntentRequest inside if", false);
                }
            }
            else
            {
                return BodyResponse("I did not understand your request, please try again", true);
            }

            if (intent.Intent.Name.Equals("OrgSyncIntent"))
            {
                //Events MISSA = ProcessCalendar(calendarLines);
                var date = intent.Intent.Slots["date"].Value;

                var eventtype = intent.Intent.Slots["event"].Value;

                outputText = " output text works";
                //if (date == DateTime.Today.ToString("yyyy-MM-dd"))
                //{
                //    string whatsGoingOn = " ";
                //    foreach (var Event in Calendar)
                //    {
                //        whatsGoingOn += Event.EventType + " on " + Event.DayTime + ".";
                //        if (Event.DayTime.Date.Equals(DateTime.Today.Date))
                //        {
                //            outputText += "You have " + Event.EventType + "located at " + Event.Location + " on " + Event.DayTime;


                //        }
                //    }
                //    outputText = whatsGoingOn;




               // }

                //// Trying to compare the date specified with the current date
                //else if (date == DateTime.Today.Month.ToString())
                //{
                //    foreach (var Event in Calendar)
                //    {
                //        //whatsGoingOn += Event.Key + " on " + Event.Value.ToString() + ".";
                //        if (Event.DayTime.Month.Equals(DateTime.Today.Month))
                //        {
                //            outputText += "You have " + Event.EventType + "located at " + Event.Location + " on " + Event.DayTime;
                //        }
                //    }

                //}

                //else if (date == "this week")
                //{
                //    foreach (var Event in Calendar)
                //    {
                //        //whatsGoingOn += Event.Key + " on " + Event.Value.ToString() + ".";
                //        if (Event.DayTime.Date.Equals(DateTime.Today.Date))
                //        {
                //            outputText += "You have " + Event.EventType + "located at " + Event.Location + " on " + Event.DayTime;
                //        }
                //        if (Event.DayTime.Date.Equals(DateTime.Today.AddDays(1).Date))
                //        {
                //            outputText += "You have " + Event.EventType + "located at " + Event.Location + " on " + Event.DayTime;
                //        }
                //        if (Event.DayTime.Date.Equals(DateTime.Today.AddDays(2).Date))
                //        {
                //            outputText += "You have " + Event.EventType + "located at " + Event.Location + " on " + Event.DayTime;
                //        }
                //        if (Event.DayTime.Date.Equals(DateTime.Today.AddDays(3).Date))
                //        {
                //            outputText += "You have " + Event.EventType + "located at " + Event.Location + " on " + Event.DayTime;
                //        }
                //        if (Event.DayTime.Date.Equals(DateTime.Today.AddDays(4).Date))
                //        {
                //            outputText += "You have " + Event.EventType + "located at " + Event.Location + " on " + Event.DayTime;

                //        }
                //        if (Event.DayTime.Date.Equals(DateTime.Today.AddDays(5).Date))
                //        {
                //            outputText += "You have " + Event.EventType + "located at " + Event.Location + " on " + Event.DayTime;
                //        }
                //        if (Event.DayTime.Date.Equals(DateTime.Today.AddDays(6).Date))
                //        {
                //            outputText += "You have " + Event.EventType + "located at " + Event.Location + " on " + Event.DayTime;
                //        }
                //        if (Event.DayTime.Date.Equals(DateTime.Today.AddDays(7).Date))
                //        {
                //            outputText += "You have " + Event.EventType + "located at " + Event.Location + " on " + Event.DayTime;
                //        }

                //    }
                //}


                //var eventInfo = await GetEventInfo(Dates, EventTypes, Location, context);
                //{
                //    outputText = $"You have a {eventtype} on {date}";
                //}
                //slots

                //if (lastName == null)
                //{
                //    return BodyResponse("I did not understand the last name of the player you wanted, please try again.", false);
                //}

                //else if (firstName == null)
                //{
                //    return BodyResponse("I did not understand the first name of the player you wanted, please try again.", false);
                //}

                


                return BodyResponse(outputText, false);
            }

            else if (intent.Intent.Name.Equals("AMAZON.StopIntent"))
            {

                return BodyResponse("You have now exited the Connect to OrgSync Skill", true);
            }

            else
            {
                return BodyResponse("I did not understand this intent, please try again", true);
            }
        }

        private SkillResponse BodyResponse(string outputSpeech,
            bool shouldEndSession,
            string repromptText = "Ask me about your events to begin. To exit, say, exit.")
        {
            var response = new ResponseBody
            {
                ShouldEndSession = shouldEndSession,
                OutputSpeech = new PlainTextOutputSpeech { Text = outputSpeech }
            };

            if (repromptText != null)
            {
                response.Reprompt = new Reprompt() { OutputSpeech = new PlainTextOutputSpeech() { Text = repromptText } };
            }

            var skillResponse = new SkillResponse
            {
                Response = response,
                Version = "1.0"
            };
            return skillResponse;
        }
        //private static Events ProcessCalendar(string[] calendarLines)
        //{
        //    Events newEvent = new Events();

        //    for (int i = 1; i < calendarLines.Length; i++)
        //    {
        //        string line = calendarLines[i].Trim();

        //        if (line != string.Empty)
        //        {
        //            var lineParts = line.Split(',');  // Separate the lines

        //            var Dates = Convert.ToDateTime(lineParts[0].Trim());
        //            var Event = lineParts[1].Trim();
        //            var Location = lineParts[2].Trim();
        //            Calendar.Add(new Events(Event, Location, Dates));
        //        }

        //    }

        //    return newEvent;
        //}
        //private async Task<Events> GetEventInfo(DateTime Dates, string Events, string Location, ILambdaContext context)
        //{
        //    Events MISSAevent = new Events();
        //    var uri = new Uri($"http://oumisprojects.com/201810/MISSA_Dates_Events.csv");

        //    try
        //    {
        //        //This is the actual GET request
        //        var response = await httpClient.GetStringAsync(uri);
        //        context.Logger.LogLine($"Response from URL:\n{response}");
        //        // TODO: (PMO) Handle bad requests
        //        //Conver the below from the JSON output into a list of player objects
        //        MISSAevent = JsonConvert.DeserializeObject<Events>(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        context.Logger.LogLine($"\nException: {ex.Message}");
        //        context.Logger.LogLine($"\nStack Trace: {ex.StackTrace}");
        //    }

        //    return MISSAevent;
        //}
    }
}























