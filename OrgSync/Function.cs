using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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



        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            string uri = $"http://oumisprojects.com/201810/MISSA_Dates_Events.csv";

            WebRequest GetURL = WebRequest.Create(uri);

            Stream page = GetURL.GetResponse().GetResponseStream();

            StreamReader sr = new StreamReader(page);

            String csv = sr.ReadToEnd();

            ProcessCalendar(csv);

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
                    var dateslot = intent.Intent.Slots["dateslot"].Value;
                    if (dateslot == null)
                    {
                        return BodyResponse("I did not understand the date, Please try again.", false);
                    }
                    //return BodyResponse(" Meeting outside if ", false);

                    else if (dateslot == DateTime.Today.ToString("yyyy-MM-dd"))
                    {
                        foreach (Events meeting in Calendar)
                        {
                            if (meeting.DayTime.Date == DateTime.Today.Date)
                            {
                                outputText= $"Today at {meeting.DayTime.ToString("hh:mm tt")} in {meeting.Location}, you have {meeting.EventType}.";
                                break;
                            }
                            else
                            {

                                outputText= "You do not have an event today.";
                            }
                        }
                        
                        return BodyResponse(outputText, false);


                    }
                    else if (dateslot == DateTime.Today.AddDays(1).ToString("yyyy-MM-dd"))
                    {
                        foreach (Events meeting in Calendar)
                        {
                            if (meeting.DayTime == DateTime.Today.AddDays(1))
                            {
                                outputText= $"Tomorrow at {meeting.DayTime.ToString("hh:mm tt")} in {meeting.Location}, you have {meeting.EventType}.";
                                break;
                            }
                            else
                            {

                                outputText= "You do not have an event tomorrow.";
                            }
                        }
                        return BodyResponse(outputText, false);

                    }

                    else if (Convert.ToDateTime(dateslot).Month == DateTime.Today.Month)
                    {
                        foreach (Events meeting in Calendar)
                        {
                            //whatsGoingOn += Event.Key + " on " + Event.Value.ToString() + ".";
                            if (meeting.DayTime.Month.Equals(Convert.ToDateTime(dateslot).Month))
                            {
                                outputText = $"On {meeting.DayTime.Date}, you have {meeting.EventType} in {meeting.Location}.";
                                break;
                            }
                            else if (meeting.DayTime.Month != Convert.ToDateTime(dateslot).Month)
                            {
                                outputText = "You do not have any meetings this month.";
                            }
                        }

                        return BodyResponse(outputText, false);
                    }
                    else if (Convert.ToDateTime(dateslot).Month == DateTime.Today.AddMonths(1).Month)
                    {
                        foreach (Events meeting in Calendar)
                        {
                            if (meeting.DayTime.Date.ToString("yyyy-MM") == dateslot)
                            {
                                outputText = $"Your next event in {meeting.DayTime.ToString("MMMM")}  is on {meeting.DayTime.Date}. You have {meeting.EventType} in {meeting.Location}.";
                                break;
                            }
                            else if (meeting.DayTime.Date.ToString("yyyy-MM") != dateslot)
                            {
                                outputText = "You do not have any meetings next month. ";
                            }
                        }

                        return BodyResponse(outputText, false);
                    }

                    //This should be last else if before else
                    else if (dateslot != null)
                    {

                        foreach (Events meeting in Calendar)
                        {

                            if (meeting.DayTime.Date.Equals(Convert.ToDateTime(dateslot).Date))
                            {
                                outputText = $"On {meeting.DayTime.ToString("MMMM dd, yyyy")} at {meeting.DayTime.ToString("hh:mm tt")}, you have {meeting.EventType} in {meeting.Location}.";
                                break;
                            }
                            else if (meeting.DayTime.Date != Convert.ToDateTime(dateslot).Date)
                            {
                                outputText = "You do not have an event today.";
                            }


                        }
                        return BodyResponse(outputText, false);
                    }
                    else
                    {

                        return BodyResponse("Please specify a date you would like to hear about. ", false);
                    }
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


                




                // }

                //// Trying to compare the date specified with the current date

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
        private static Events ProcessCalendar(string csv)
        {
            Events newEvent = new Events();
            var eventparts = csv.Split('\n');

            for (int i = 1; i < eventparts.Length; i++)
            {
                string line = eventparts[i].Trim();

                if (line != string.Empty)
                {
                    var lineParts = line.Split(',');  // Separate the lines

                    var Dates = Convert.ToDateTime(lineParts[0].Trim());
                    var Event = lineParts[1].Trim();
                    var Location = lineParts[2].Trim();
                    Calendar.Add(new Events(Event, Location, Dates));
                }

            }

            return newEvent;
        }

    }

}























