using System;
using System.Device.Gpio;
using System.Threading;

Console.WriteLine("Blinking LED. Press Ctrl+C to end.");
int upPin = 17;
int downPin = 27;
int sleepTime = 4000;
using var controller = new GpioController();
controller.OpenPin(upPin, PinMode.Output);
controller.OpenPin(downPin, PinMode.Output);

try
{
    while (true)
    {
        TogglePinOnFor(upPin, sleepTime);
        Thread.Sleep(sleepTime);
        TogglePinOnFor(downPin, sleepTime);
        Thread.Sleep(sleepTime);
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Console.WriteLine("Setting both pins to low and exiting.");
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