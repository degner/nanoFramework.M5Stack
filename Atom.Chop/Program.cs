using Iot.Device.Ws28xx.Esp32;
using nanoFramework.AtomMatrix;
using nanoFramework.Hardware.Esp32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Device.Adc;
using System.Device.Pwm;

namespace Atom.Chop
{
    public class Program
    {
        //private static Ws2812c _ledMatrix;

        private static bool enableLight = false;

        public static void Main()
        {
            var button = AtomMatrix.Button;

            /*_ledMatrix = AtomMatrix.LedMatrix;



            // diagonal green line
            DrawDiagonalLine(Color.Green);
            Thread.Sleep(10);

            _ledMatrix.Image.Clear();
            _ledMatrix.Update();

            // diagonal blue line
            DrawDiagonalLine(Color.Blue);
            Thread.Sleep(10);

            _ledMatrix.Image.Clear();
            _ledMatrix.Update();

            // diagonal red line
            DrawDiagonalLine(Color.Red);
            Thread.Sleep(10);

            // clear LEDs
            _ledMatrix.Image.Clear();
            _ledMatrix.Update();*/

            button.ButtonDown += Button_ButtonDown;
            button.ButtonUp += Button_ButtonUp;

            Debug.WriteLine("Hello from nanoFramework!");






            //Bme280_sample.RunSample(_ledMatrix);

            //int ledStripPin = 22;
            //int ledStripNumber = 5; // 144
            //var ledStrip = new Iot.Device.Ws28xx.Esp32.Ws2812c(ledStripPin, 1, ledStripNumber);









            //if (ledStrip != null)
            //{
            //}


            // instantiate a new Pixel controller, ATOM Matrix has 5x5 LEDs

            bool forward = true;
            bool brighter = true;
            float brightness = 0f;



            //while (true)
            //{
            //    _ledMatrix.Image.Clear();

            //    if (forward)
            //    {


            //        for (int i = 0; i < 25; i++)
            //        {
            //            _ledMatrix.Image.SetPixel(0, i, Color.Green);
            //            _ledMatrix.Update();
            //        }
            //    }
            //    else
            //    {
            //        //_ledMatrix.Image.SetPixel(0, 0, Color.Red);
            //        //_ledMatrix.Update();

            //        for (int i = 25 - 2; i > 0; i--)
            //        {
            //            _ledMatrix.Image.SetPixel(0, i, Color.Green);
            //            _ledMatrix.Update();
            //        }
            //    }

            //    forward = !forward;
            //}

            // Light
            System.Device.Gpio.GpioController s_GpioController = new System.Device.Gpio.GpioController();
            System.Device.Gpio.GpioPin light = s_GpioController.OpenPin(19);
            light.SetPinMode(System.Device.Gpio.PinMode.Output);
            light.Write(System.Device.Gpio.PinValue.Low);

            Configuration.SetPinFunction(19, DeviceFunction.PWM1);
            PwmChannel pwmPin = PwmChannel.CreateFromPin(19, 40000);

            if (pwmPin != null)
            {
                pwmPin.DutyCycle = 0.0f;
            }



            // LDR
            //var adcChan = nanoFramework.AtomMatrix.AtomMatrix.GetAdcGpio(33);
            //int adcMax = adcChan.Controller.MaxValue;
            //float adcValue = 0;



            // Create the thread object, passing in the
            // serverObject.StaticMethod method using a
            // ThreadStart delegate.
            Thread staticCaller = new Thread(new ThreadStart(LightStripTask));

            // Start the thread.
            staticCaller.Start();




            while (true)
            {
                //

                //_ledMatrix.Image.Clear();

                /*int myAdcRawvalue = adcChan.ReadValue();

                adcValue = 100f * (float)myAdcRawvalue / (float)adcMax;

                //Debug.WriteLine($"ADC {adcValue}%   : ADC pin: {myAdcRawvalue} (max {adcChan.Controller.MaxValue}, min {adcChan.Controller.MinValue}, res {adcChan.Controller.ResolutionInBits}");

                int ledsToLight = (int)Math.Round(adcValue / (100f / 25f));
                //int ledsToLight = (int)Math.Round(adcValue / (100f / 144f));

                ledsToLight = Math.Min(25 - 1, ledsToLight);


                bool singleLed = true;



                if (singleLed)
                {

                    int x = ledsToLight / 5;
                    int y = ledsToLight % 5;
                    //_ledMatrix.Image.SetPixel(x, y, Color.Blue);
                    //_ledMatrix.Update();

                    ledsToLight = (int)(adcValue * ((float)ledStripNumber / 100f));
                    ledsToLight = Math.Min(ledStripNumber - 1, ledsToLight);
                    ledsToLight = Math.Max(0, ledsToLight);

                    byte blue = (byte)(adcValue * 2.55f);


                    ledStrip.Image.Clear();
                    ledStrip.Image.SetPixel(0, ledsToLight, 0, 0, blue);
                    ledStrip.Update();
                }
                else
                {
                    // Fill
                    //_ledMatrix.Image.Clear();
                    for (int i = 0; i < ledsToLight; i++)
                    {
                        int x = i / 5;
                        int y = i % 5;
                        //_ledMatrix.Image.SetPixel(x, y, Color.Blue);
                    }
                    //_ledMatrix.Update();
                }

                */















                //if (forward)
                //{
                //    //_ledMatrix.Image.SetPixel(0, 0, Color.Green);
                //    //_ledMatrix.Update();

                //    for (int i = 0; i < leds; i++)
                //    {
                //        ledStrip.Image.Clear();
                //        ledStrip.Image.SetPixel(0, i, Color.Blue);
                //        ledStrip.Update();
                //        //Thread.Sleep(10);
                //    }
                //}
                //else
                //{
                //    //_ledMatrix.Image.SetPixel(0, 0, Color.Red);
                //    //_ledMatrix.Update();

                //    for (int i = leds - 2; i > 0; i--)
                //    {
                //        ledStrip.Image.Clear();
                //        ledStrip.Image.SetPixel(0, i, Color.Blue);
                //        ledStrip.Update();
                //        //Thread.Sleep(10);
                //    }
                //}

                //_ledMatrix.Image.Clear();
                //if (forward)
                //{
                //    _ledMatrix.Image.SetPixel(0, 0, Color.Blue);
                //}
                //else
                //{
                //    _ledMatrix.Image.SetPixel(0, 0, Color.Red);
                //}


                Thread.Sleep(100);

                forward = !forward;

                //if (forward)
                //{
                //    pwmPin.DutyCycle = 0.25f;

                //}
                //else
                //{
                //    pwmPin.DutyCycle = 0.9f;
                //}

                if (enableLight)
                {

                    if (brighter)
                    {
                        brightness = (brightness + 0.001f) * 1.2f;

                        if (brightness >= 1)
                        {
                            brightness = 1f;
                            brighter = false;
                        }
                    }
                    else
                    {
                        brightness = (brightness / 1.2f) - 0.001f;

                        if (brightness <= 0)
                        {
                            brightness = 0f;
                            brighter = true;
                        }
                    }


                }
                else
                {
                    brightness = 0.0f;
                    brighter = true;
                }

                pwmPin.DutyCycle = brightness;


                //light.Toggle();
            }




            //DrawCross(Color.Red);
            Thread.Sleep(Timeout.Infinite);
        }

        /*private static void DrawDiagonalLine(Color color)
        {
            _ledMatrix.Image.SetPixel(0, 0, color);
            _ledMatrix.Image.SetPixel(1, 1, color);
            _ledMatrix.Image.SetPixel(2, 2, color);
            _ledMatrix.Image.SetPixel(3, 3, color);
            _ledMatrix.Image.SetPixel(4, 4, color);
            _ledMatrix.Update();
        }

        private static void DrawCross(Color color)
        {
            _ledMatrix.Image.SetPixel(0, 0, color);
            _ledMatrix.Image.SetPixel(1, 1, color);
            _ledMatrix.Image.SetPixel(2, 2, color);
            _ledMatrix.Image.SetPixel(3, 3, color);
            _ledMatrix.Image.SetPixel(4, 4, color);
            _ledMatrix.Image.SetPixel(0, 4, color);
            _ledMatrix.Image.SetPixel(1, 3, color);
            _ledMatrix.Image.SetPixel(2, 2, color);
            _ledMatrix.Image.SetPixel(3, 1, color);
            _ledMatrix.Image.SetPixel(4, 0, color);
            _ledMatrix.Update();
        }*/


        //private static bool buttonDisable = false;

        private static void Button_ButtonUp(object sender, EventArgs e)
        {
            //_ledMatrix.Image.Clear();
            //_ledMatrix.Update();
            //buttonDisable = false;
        }

        private static void Button_ButtonDown(object sender, EventArgs e)
        {
            //_ledMatrix.Image.Clear(Color.Green);
            //_ledMatrix.Update();

            //if (!buttonDisable)
            {
                enableLight = !enableLight;
                //buttonDisable = true;
            }
        }

        private static void LightStripTask()
        {
            var adcChan = nanoFramework.AtomMatrix.AtomMatrix.GetAdcGpio(33);
            int adcMax = adcChan.Controller.MaxValue;
            float adcValue = 0;

            int ledStripPin = 22;
            int ledStripNumber = 144; // 144
            var ledStrip = new Iot.Device.Ws28xx.Esp32.Ws2812c(ledStripPin, 1, ledStripNumber);


            while (true)
            {


                int myAdcRawvalue = adcChan.ReadValue();

                adcValue = 100f * (float)myAdcRawvalue / (float)adcMax;

                //Debug.WriteLine($"ADC {adcValue}%   : ADC pin: {myAdcRawvalue} (max {adcChan.Controller.MaxValue}, min {adcChan.Controller.MinValue}, res {adcChan.Controller.ResolutionInBits}");

                int ledsToLight = (int)Math.Round(adcValue / (100f / 25f));
                //int ledsToLight = (int)Math.Round(adcValue / (100f / 144f));

                ledsToLight = Math.Min(25 - 1, ledsToLight);








                int x = ledsToLight / 5;
                int y = ledsToLight % 5;
                //_ledMatrix.Image.SetPixel(x, y, Color.Blue);
                //_ledMatrix.Update();

                ledsToLight = (int)(adcValue * ((float)ledStripNumber / 100f));
                ledsToLight = Math.Min(ledStripNumber - 1, ledsToLight);
                ledsToLight = Math.Max(0, ledsToLight);

                byte blue = (byte)(adcValue * 2.55f);


                ledStrip.Image.Clear();
                ledStrip.Image.SetPixel(0, ledsToLight, 0, 0, blue);
                ledStrip.Update();











                Thread.Sleep(100);
            }
        }
    }
}
