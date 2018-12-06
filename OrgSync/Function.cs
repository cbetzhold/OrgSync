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

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>


        // private static HttpClient httpClient;
        Dictionary<string, DateTime> Events = new Dictionary<string, DateTime>()
{
            {"MISSA meeting", DateTime.Today},
            {"Lunch and Learn", DateTime.Today.AddDays(1)},
            //{3,DateTime.Today.AddDays(2)},
            //{4,DateTime.Today.AddDays(3)},
            //{5,DateTime.Today.AddDays(4)},
            //{6,DateTime.Today.AddDays(5)},
            //{7,DateTime.Today.AddDays(6)}
};


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

                }
                else
                {
                    return BodyResponse("I did not understand your request, please try again", true);
                }

                if (intent.Intent.Name.Equals("OrgSyncIntent"))
                {
                    var date = intent.Intent.Slots["date"].Value;

                    var eventtype = intent.Intent.Slots["event"].Value;


                if (date == "month")
                {
                    DateTime dtdate = Convert.ToDateTime(date);
                    int currentM = DateTime.Today.Month;


                    outputText = "You have an event this month";
                }
                else
                {
                    outputText = "You do not have an event this month";
                }

                

                    //var eventInfo = await GetInfo(date, eventtype);
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

                    //var playerInfo = await GetPlayerInfo(lastName, firstName, context);
                    //{
                    //    outputText = $"For the 2017-2018 N.B.A. season, {playerInfo.name} plays {playerInfo.minutes_per_game} minutes per game. " + $" He has averaged shooting splits of {playerInfo.field_goal_percentage}%, {playerInfo.three_point_percentage}%, and {playerInfo.free_throw_percentage}%.";
                    //}



                    return BodyResponse(outputText, true);
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
                string repromptText = "Just say, tell my events to learn more. To exit, say, exit.")
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
    }
    }
   
 

            

            //public class GetInfo(DateTime, string)
            //    {

            //    }









