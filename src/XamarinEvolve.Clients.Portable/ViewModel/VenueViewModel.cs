using System;

using Xamarin.Forms;
using System.Windows.Input;
using System.Threading.Tasks;
using Plugin.ExternalMaps;
using Plugin.Messaging;
using FormsToolkit;

namespace XamarinEvolve.Clients.Portable
{
    public class VenueViewModel : ViewModelBase
    {
        public bool CanMakePhoneCall => CrossMessaging.Current.PhoneDialer.CanMakePhoneCall;
        public string EventTitle => "Labour Party Conference";
        public string LocationTitle => "The Brighton Centre";
        public string Address1 => "10A Fleet St";
        public string Address2 => "Brighton BN1 2GR";
        public double Latitude => 50.821037;
        public double Longitude => -0.146184;

        ICommand  navigateCommand;
        public ICommand NavigateCommand =>
            navigateCommand ?? (navigateCommand = new Command(async () => await ExecuteNavigateCommandAsync())); 

        async Task ExecuteNavigateCommandAsync()
        {
            Logger.Track(EvolveLoggerKeys.NavigateToEvolve);
            if(!await CrossExternalMaps.Current.NavigateTo(LocationTitle, Latitude, Longitude))
            {
                MessagingService.Current.SendMessage(MessageKeys.Message, new MessagingServiceAlert
                    {
                        Title = "Unable to Navigate",
                        Message = "Please ensure that you have a map application installed.",
                        Cancel = "OK"
                    });
            }
        }

        ICommand  callCommand;
        public ICommand CallCommand =>
            callCommand ?? (callCommand = new Command(ExecuteCallCommand)); 

        void ExecuteCallCommand()
        {
            Logger.Track(EvolveLoggerKeys.CallHotel);
            var phoneCallTask = CrossMessaging.Current.PhoneDialer;
            if (phoneCallTask.CanMakePhoneCall) 
                phoneCallTask.MakePhoneCall("14072841234");
        }
    }
}


