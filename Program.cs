using Microsoft.Identity.Client;
using System;
using System.Device.Gpio;
using System.Threading;


Console.WriteLine("Hello World!");
Console.WriteLine("Blinking LED. Press Ctrl+C to end.");
int upPin = 17;
int downPin = 27;
int sleepTime = 12000;
using var controller = new GpioController();
controller.OpenPin(upPin, PinMode.Output);
controller.OpenPin(downPin, PinMode.Output);
controller.Write(upPin, PinValue.High);
controller.Write(downPin, PinValue.High);

try
{
    var settings = Settings.LoadSettings();
    // Initialize Graph
    InitializeGraph(settings);
    //getAccessToken
    await DisplayAccessTokenAsync();

    while (true)
    {
        var presence = await GraphHelper.GetUsersPresenceAsync("707fca83-7a84-4d7e-8833-19ddfd7f9d2b");
        Console.WriteLine(presence.Activity.ToString());
        if (presence != null && (presence.Activity == "InAMeeting" || presence.Activity == "InACall" || presence.Activity == "InAConferenceCall")) 
        {
            TogglePinOnFor(upPin, sleepTime);
          //  Thread.Sleep(sleepTime);

        }
        //maybe add conditions for down

        
        Thread.Sleep(sleepTime);

    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Console.WriteLine("Setting both pins to high and exiting.");
    controller.Write(upPin, PinValue.High);
    controller.Write(downPin, PinValue.High);
}

void TogglePinOnFor(int pin, int milliseconds)
{
    if (controller is null)
    {
        return;
    }

    Console.WriteLine($"Setting pin {pin} to low");
    controller.Write(pin, PinValue.Low);
    Console.WriteLine($"Sleeping for {milliseconds} milliseconds");
    Thread.Sleep(milliseconds);
    Console.WriteLine($"Setting pin {pin} to high");
    controller.Write(pin, PinValue.High);
}

void InitializeGraph(Settings settings)
{
    GraphHelper.InitializeGraphForUserAuth(settings,
        (info, cancel) =>
        {
            // Display the device code message to
            // the user. This tells them
            // where to go to sign in and provides the
            // code to use.
            Console.WriteLine(info.Message);
            return Task.FromResult(0);
        });
}


async Task DisplayAccessTokenAsync()
{
    try
    {
        var userToken = await GraphHelper.GetUserTokenAsync();
       // Console.WriteLine($"User token: {userToken}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error getting user access token: {ex.Message}");
    }
}




