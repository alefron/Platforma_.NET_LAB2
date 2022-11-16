using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformaNETlab2
{
    public class CarService
    {
        private Timer? timer;

        public void ActiveStopSign()
        {
            DeactiveStopSign();
            timer = new Timer(TimerCallback, null, 0, 100);
        }

        public void DeactiveStopSign()
        {
            timer?.Dispose();
        }

        public async Task ChangeWheelAsync()
        {
            Console.WriteLine("Wheel changing - START");
            await Task.Delay(2000).ContinueWith(write =>
            {
                Console.WriteLine("Wheel changing - END");
            });
        }

        public void ChangeWheel()
        {
            Console.WriteLine("Wheel changing - START");
            Thread.Sleep(2000);
            Console.WriteLine("Wheel changing - END");
        }

        public async Task RefuelAsync()
        {
            Console.WriteLine("Refueling - START");
            await Task.Delay(5000).ContinueWith(write =>
            {
                Console.WriteLine("Refueling - END");
            });
        }

        public void Refuel()
        {
            Console.WriteLine("Refueling - START");
            Thread.Sleep(5000);
            Console.WriteLine("Refueling - END");
        }

        public async Task SetWingAsync()
        {
            Console.WriteLine("Setting wing - START");
            await Task.Delay(1000).ContinueWith(write =>
            {
                Console.WriteLine("Setting wing - END");
            });
        }

        public void SetWing()
        {
            Console.WriteLine("Setting wing - START");
            Thread.Sleep(1000);
            Console.WriteLine("Setting wing - END");
        }

        public async Task CleanHelmetAsync()
        {
            Console.WriteLine("Cleaning a helmet - START");
            await Task.Delay(500).ContinueWith(write =>
            {
                Console.WriteLine("Cleaning a helmet - END");
            });
        }

        public void CleanHelmet()
        {
            Console.WriteLine("Cleaning a helmet - START");
            Thread.Sleep(500);
            Console.WriteLine("Cleaning a helmet - END");
        }

        public void PitStop()
        {
            var activeStopSign = Task.Run(() =>
            {
                ActiveStopSign();
            });
            ChangeWheel();
            ChangeWheel();
            ChangeWheel();
            ChangeWheel();
            Refuel();
            SetWing();
            CleanHelmet();
            DeactiveStopSign();
            activeStopSign.Dispose();
        }

        public async void PitStopAsync()
        {
            var activeStopSign = Task.Run(() =>
            {
                ActiveStopSign();
            });
            var tasks = new List<Task>();
            tasks.Add(ChangeWheelAsync());
            tasks.Add(ChangeWheelAsync());
            tasks.Add(ChangeWheelAsync());
            tasks.Add(ChangeWheelAsync());
            tasks.Add(RefuelAsync());
            tasks.Add(SetWingAsync());
            tasks.Add(CleanHelmetAsync());

            await Task.WhenAll(tasks.ToArray());

            DeactiveStopSign();
            activeStopSign.Dispose();
        }

        private void TimerCallback(object o)
        {
            Console.WriteLine("STOP is active!!!!");
        }
    }
}
