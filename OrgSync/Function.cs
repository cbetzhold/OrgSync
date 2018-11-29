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

        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            string outputText = "";
            var requestType = input.GetRequestType();

            if (requestType == typeof(LaunchRequest))
            {
                return BodyResponse("Welcome to the Connect to OrgSync skill!", false);
            }

            else if (requestType == typeof(IntentRequest))
            {
                //outputText += "Request type is Intent";
                var intent = input.Request as IntentRequest;
            }
            else
            {
                return BodyResponse("I did not understand your request, please try again", true);
            }

            if (intent.Intent.Name.Equals("OrgSyncIntent"))
                {
                   // var firstName = intent.Intent.Slots["firstName"].Value;
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




}
        }
    


