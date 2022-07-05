using System.Device.I2c;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using Iot.Device.Bmxx80;
using Iot.Device.Bmxx80.FilteringMode;
using Iot.Device.Common;
using Iot.Device.Ws28xx.Esp32;
using nanoFramework.AtomMatrix;
using nanoFramework.Hardware.Esp32;
using UnitsNet;



namespace Atom.Chop
{
    public class Bme280_sample
    {
        public static void RunSample(Ws2812c ledMatrix)
        {
            Debug.WriteLine("Hello Bme280!");

            //////////////////////////////////////////////////////////////////////
            // when connecting to an ESP32 device, need to configure the I2C GPIOs
            // used for the bus
            Configuration.SetPinFunction(25, DeviceFunction.I2C1_DATA);
            Configuration.SetPinFunction(21, DeviceFunction.I2C1_CLOCK);

            // bus id on the MCU
            const int busId = 1;
            // set this to the current sea level pressure in the area for correct altitude readings
            Pressure defaultSeaLevelPressure = WeatherHelper.MeanSeaLevel;

            I2cConnectionSettings i2cSettings = new(busId, Bme280.SecondaryI2cAddress);
            using I2cDevice i2cDevice = I2cDevice.Create(i2cSettings);
            using Bme280 bme80 = new Bme280(i2cDevice)
            {
                // set higher sampling
                TemperatureSampling = Sampling.LowPower,
                PressureSampling = Sampling.UltraHighResolution,
                HumiditySampling = Sampling.Standard,
            };

            ledMatrix.Image.SetPixel(0, 0, Color.Blue);
            ledMatrix.Update();

            bool one = true;

            while (true)
            {
                // Perform a synchronous measurement
                var readResult = bme80.Read();

                // Note that if you already have the pressure value and the temperature, you could also calculate altitude by using
                // var altValue = WeatherHelper.CalculateAltitude(preValue, defaultSeaLevelPressure, tempValue) which would be more performant.
                bme80.TryReadAltitude(defaultSeaLevelPressure, out var altValue);

                Debug.WriteLine($"Temperature: {readResult.Temperature.DegreesCelsius}\u00B0C");
                Debug.WriteLine($"Pressure: {readResult.Pressure.Hectopascals}hPa");
                Debug.WriteLine($"Altitude: {altValue.Meters}m");
                Debug.WriteLine($"Relative humidity: {readResult.Humidity.Percent}%");

                // WeatherHelper supports more calculations, such as saturated vapor pressure, actual vapor pressure and absolute humidity.
                //if (!readResult.Temperature.Equals(null) && !readResult.Humidity.Equals(null))
                {
                    //Debug.WriteLine($"Heat index: {WeatherHelper.CalculateHeatIndex(readResult.Temperature, readResult.Humidity).DegreesCelsius}\u00B0C");
                    //Debug.WriteLine($"Dew point: {WeatherHelper.CalculateDewPoint(readResult.Temperature, readResult.Humidity).DegreesCelsius}\u00B0C");
                }

                Thread.Sleep(1000);

                // change sampling and filter
                bme80.TemperatureSampling = Sampling.UltraHighResolution;
                bme80.PressureSampling = Sampling.UltraLowPower;
                bme80.HumiditySampling = Sampling.UltraLowPower;
                bme80.FilterMode = Bmx280FilteringMode.X2;

                // Perform an asynchronous measurement
                readResult = bme80.Read();

                // Note that if you already have the pressure value and the temperature, you could also calculate altitude by using
                // var altValue = WeatherHelper.CalculateAltitude(preValue, defaultSeaLevelPressure, tempValue) which would be more performant.
                bme80.TryReadAltitude(defaultSeaLevelPressure, out altValue);

                Debug.WriteLine($"Temperature: {readResult.Temperature.DegreesCelsius}\u00B0C");
                Debug.WriteLine($"Pressure: {readResult.Pressure.Hectopascals}hPa");
                Debug.WriteLine($"Altitude: {altValue.Meters}m");
                Debug.WriteLine($"Relative humidity: {readResult.Humidity.Percent}%");

                // WeatherHelper supports more calculations, such as saturated vapor pressure, actual vapor pressure and absolute humidity.
                /*if (!readResult.Temperature.Equals(null) && !readResult.Humidity.Equals(null))
                {
                    Debug.WriteLine($"Heat index: {WeatherHelper.CalculateHeatIndex(readResult.Temperature, readResult.Humidity).DegreesCelsius}\u00B0C");
                    Debug.WriteLine($"Dew point: {WeatherHelper.CalculateDewPoint(readResult.Temperature, readResult.Humidity).DegreesCelsius}\u00B0C");
                }*/

                Thread.Sleep(5000);

                ledMatrix.Image.Clear();

                if (one)
                {
                    ledMatrix.Image.SetPixel(1, 1, Color.Blue);
                }
                else
                {
                    ledMatrix.Image.SetPixel(0, 0, Color.Blue);
                }

                ledMatrix.Update();

                one = !one;


            }
        }
    }
}