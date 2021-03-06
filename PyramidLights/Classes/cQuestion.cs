﻿using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class cQuestion
{
    // If modifying these scopes, delete your previously saved credentials
    // at ~/.credentials/sheets.googleapis.com-dotnet-quickstart.json
    static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
    static string ApplicationName = "Google Sheets API Pyramid Lights";

    public DateTime startDate;
    public int startHour;
    public int startMinutes;
    public DateTime startDateTime;
    public DateTime endDate;
    public int endHour;
    public int endMinutes;
    public DateTime endDateTime;
    public string pyramidScene = null;
    public string questionText = null;
    public string answer1 = null;
    public string answer2 = null;
    public string answer3 = null;
    public string answer4 = null;
    public string tweetString = null;
    public int correctAnswer = -99;
    public string correctResponseTweetString = null;
    public string incorrectResponseTweetString = null;
    private cConfigurationHelper configHelper = null;
           
    public cQuestion(cConfigurationHelper vConfigHelper)
    {
        configHelper = vConfigHelper;
    }

    public bool tweetTooLong()
    {
        return (tweetString.ToUpper() == "TOO LONG");
    }

    public bool correctResponseTweetTooLong()
    {
        return (correctResponseTweetString.ToUpper() == "TOO LONG");
    }
    public bool incorrectResponseTweetTooLong()
    {
        return (incorrectResponseTweetString.ToUpper() == "TOO LONG");
    }
    public void getQuestion()
    {
        // Create Google Sheets API service.
        var service = new SheetsService(new BaseClientService.Initializer()
        {
           // HttpClientInitializer = credential,
            ApplicationName = ApplicationName,
            ApiKey = configHelper.apiKey()
        });

        //https://docs.google.com/spreadsheets/d/1hpj2qvhHXj1QRcjlU34urkKeXGvltHfjCgJ1tsk9AB0/edit#gid=0
        String range = "Sheet1!A1:R";

        SpreadsheetsResource.ValuesResource.GetRequest request =
                service.Spreadsheets.Values.Get(configHelper.spreadsheetID(), range);

        ValueRange response = request.Execute();
        IList<IList<Object>> values = response.Values;
        if (values != null && values.Count > 0)
        {
           bool breakLoop = false;
            //public DateTime startDate;
            //public int startHour;
            //public int startMinutes;
            //public DateTime endDate;
            //public int endHour;
            //public int endMinutes;
            //public string pyramidScene = null;
            //public string questionText = null;
            //public string answer1 = null;
            //public string answer2 = null;
            //public string answer3 = null;
            //public string answer4 = null;
            //public string tweetString = null;
            //public string correctAnswer = null;
            //public string correctResponseTweetString = null;
            //public string incorrectResponseTweetString = null;
            //start at 1 to skip header row
            for (int i = 2; i< values.Count;i++)
            {
                var row = values[i];
                if (breakLoop == false)
                {
                   
                    //public DateTime startDate;
                    //public int startHour;
                    //public int startMinutes;
                    //public DateTime endDate;
                    //public int endHour;
                    //public int endMinutes;
                    startDate = DateTime.Parse((string) row[0]);
                    startHour = Convert.ToInt32(row[1]);
                    startMinutes = Convert.ToInt32(row[2]);
                    endDate = DateTime.Parse((string)row[3]);
                    endHour = Convert.ToInt32(row[4]);
                    endMinutes = Convert.ToInt32(row[5]);
                    startDateTime = new DateTime(startDate.Year, startDate.Month, startDate.Day, startHour, startMinutes, 0);
                    endDateTime = new DateTime(endDate.Year, endDate.Month, endDate.Day, endHour, endMinutes,0);
                    if (DateTime.Now > startDateTime && DateTime.Now <= endDateTime)
                    {
                        breakLoop = true;
                        //public string pyramidScene = null;
                        //public string questionText = null;
                        //public string answer1 = null;
                        //public string answer2 = null;
                        //public string answer3 = null;
                        //public string answer4 = null;
                        //public string tweetString = null;
                        //public string correctAnswer = null;
                        //public string correctResponseTweetString = null;
                        //public string incorrectResponseTweetString = null;
                        try
                        {
                            pyramidScene = Convert.ToString(row[6]);
                        }
                        catch (System.ArgumentOutOfRangeException ex)
                        {
                            pyramidScene = "scene10";
                            Console.WriteLine(ex.Message);
                        }
                        questionText = Convert.ToString(row[7]);
                        answer1 = Convert.ToString(row[8]);
                        answer2 = Convert.ToString(row[9]);
                        answer3 = Convert.ToString(row[10]);
                        answer4 = Convert.ToString(row[11]);
                        tweetString = Convert.ToString(row[12]);
                        correctAnswer = Convert.ToInt32(row[13]);
                        
                        correctResponseTweetString = Convert.ToString(row[15]);
                        incorrectResponseTweetString = Convert.ToString(row[17]);
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("No data found.");
        }
    }

    private void setQuestionValues(DateTime vStartDate, int vStartHour, int vStartMinutes, DateTime vEndDate, int vEndHour,
                                    int vEndMinutes, String vQuestionText, String vAnswer1, String vAnswer2, String vAnswer3, 
                                    String vAnswer4, Int32 vCorrectAnswer, String vFollowUpMessage, String vPyramidScene, 
                                    String vTweetString, String vCorrectResponseTweetString, String vIncorrectResponseTweetString)
    {

        startDate = vStartDate;
        startHour = vStartHour;
        startMinutes = vStartMinutes;
        endDate = vEndDate;
        endHour = vEndHour;
        endMinutes = vEndMinutes;
        if (vPyramidScene == "scene9" || vPyramidScene == "scene10" || vPyramidScene == "scene11" ||
             vPyramidScene == "scene12" || vPyramidScene == "scene13" || vPyramidScene == "scene14" ||
             vPyramidScene == "scene15" || vPyramidScene == "scene16")
        {
            pyramidScene = vPyramidScene;
        }
        else
        {
            pyramidScene = "scene10";
        }
        questionText = vQuestionText;
        answer1 = vAnswer1;
        answer2 = vAnswer2;
        answer3 = vAnswer3;
        answer4 = vAnswer4;
        tweetString = vTweetString;
        correctAnswer =vCorrectAnswer;
        correctResponseTweetString = vCorrectResponseTweetString;
        incorrectResponseTweetString = vIncorrectResponseTweetString;

    }
}


