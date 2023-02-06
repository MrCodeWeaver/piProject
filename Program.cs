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

while (true)
{
    TogglePinOnFor(upPin, sleepTime);
    Thread.Sleep(sleepTime);
    TogglePinOnFor(downPin, sleepTime);
    Thread.Sleep(sleepTime);
}

void TogglePinOnFor(int pin, int milliseconds)
{
    if (controller is null)
    {
        return;
    }
    
    controller.Write(pin, PinValue.High);
    Thread.Sleep(milliseconds);
    controller.Write(pin, PinValue.Low);
}