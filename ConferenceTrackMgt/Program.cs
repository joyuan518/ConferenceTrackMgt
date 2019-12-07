using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using ConferenceTrackMgt.Lib;

namespace ConferenceTrackMgt
{
    class Program
    {
        static void Main(string[] args)
        {
            var conference = new Conference();
            var talkValidationReg = new Regex(@"^(?<Title>[^\d]+) (?<Duration>([1-9]\d*min)|(lightning))$", RegexOptions.Compiled);

            Console.WriteLine("Please input conference talks. One for each line, as the format '[Talk Title] [Talk Duration in minutes]min':");
            Console.WriteLine("--- e.g. 'Introduction to Python 45min', talks which have the length of 5 minutes should be mark as lightning, e.g. 'Write Good Unit Tests lightning'");
            Console.WriteLine("--- An empty line will complete the input of talks.");
            
            while (true)
            {
                var talkString = Console.ReadLine();

                //Input of all the proposals completed, then print out the conference arrangement
                if (talkString.Trim() == string.Empty)
                {
                    Console.Write(conference.ToString());
                    break;
                }

                var matchResult = talkValidationReg.Match(talkString);

                if (matchResult.Success)
                {
                    var title = matchResult.Groups["Title"].Value;
                    var duration = matchResult.Groups["Duration"].Value;
                    var length = duration == "lightning" ? 5 : int.Parse(duration.Replace("min", string.Empty));

                    if (!conference.TryAddTalk(new Talk(TimeSpan.FromMinutes(length), title), 
                                                out AddTalkErrorType? errorType))
                    {
                        Console.WriteLine($"Error occured during adding the talk. Info: {errorType.ToString()}.");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("The talk info you have just input is not valid, please correct the format and try again...");
                }
            }
        }

        static void HandleManualInput()
        {

        }

        static void HandleFileInput(string fileName)
        {

        }
    }
}
