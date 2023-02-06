using System;
using System.Device.Gpio;
using System.Threading;

Console.WriteLine("Blinking LED. Press Ctrl+C to end.");
int pin = 17;
int downPin = 27;
using var controller = new GpioController();
controller.OpenPin(pin, PinMode.Output);
controller.OpenPin(downPin, PinMode.Output);

//bool ledOn = true;
//bool downOn = true;
while (true)
{
    controller.Write(downPin, PinValue.Low);
    Thread.Sleep(250);
    controller.Write(pin, PinValue.Low);
    
    Thread.Sleep(2500);

    controller.Write(downPin,  PinValue.Low);
    Thread.Sleep(250);
    controller.Write(pin, PinValue.High);

    Thread.Sleep(5000);

    controller.Write(downPin, PinValue.Low);
    Thread.Sleep(250);
    controller.Write(pin, PinValue.Low);

    Thread.Sleep(3000);

    controller.Write(downPin,  PinValue.High);
    Thread.Sleep(250);
    controller.Write(pin, PinValue.Low);

    Thread.Sleep(5000);

    controller.Write(downPin,  PinValue.Low);
    Thread.Sleep(250);
    controller.Write(pin, PinValue.Low);

    Thread.Sleep(2500);
}