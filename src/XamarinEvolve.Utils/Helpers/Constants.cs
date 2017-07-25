using System;

namespace XamarinEvolve.Clients.Portable
{
   
    public static class ApiKeys
    {
        public const string HockeyAppiOS = "HockeyAppiOS";
        public const string HockeyAppAndroid = "HockeyAppAndroid";
        public const string HockeyAppUWP = "HockeyAppUWP";

        public const string AzureServiceBusName = "AzureServiceBusName";
        public const string AzureServiceBusUrl = "https://MainNotifications.servicebus.windows.net:443/";
        public const string AzureKey ="Endpoint=sb://mainnotifications.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=kHB9CN8OcSmhBqBmx3L/Yhslmab/TMsYbgX1OxjdXRY=";
        public const string GoogleSenderId ="767694572213";
        public const string AzureHubName = "TestNotifications";
        public const string AzureListenConneciton = "Endpoint=sb://mainnotifications.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=kHB9CN8OcSmhBqBmx3L/Yhslmab/TMsYbgX1OxjdXRY=";
    }
    public static class MessageKeys
    {
        public const string NavigateToEvent = "navigate_event";
        public const string NavigateToSession = "navigate_session";
        public const string NavigateToSpeaker = "navigate_speaker";
        public const string NavigateToSponsor = "navigate_sponsor";
        public const string NavigateToImage = "navigate_image";
        public const string NavigateLogin = "navigate_login";
        public const string Error = "error";
        public const string Connection = "connection";
        public const string LoggedIn = "loggedin";
        public const string Message = "message";
        public const string Question = "question";
        public const string Choice = "choice";
    }
}

