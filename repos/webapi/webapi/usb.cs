using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webapi
{
    class USBControl : IDisposable
    {
        // used for monitoring plugging and unplugging of USB devices.
        private ManagementEventWatcher watcherAttach;
        private ManagementEventWatcher watcherRemove;

        public USBControl()
        {
            // Add USB plugged event watching
            watcherAttach = new ManagementEventWatcher();
            //var queryAttach = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 2");
            watcherAttach.EventArrived += new EventArrivedEventHandler(watcher_EventArrived);
            watcherAttach.Query = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 2");
            watcherAttach.Start();

            // Add USB unplugged event watching
            watcherRemove = new ManagementEventWatcher();
            //var queryRemove = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 3");
            watcherRemove.EventArrived += new EventArrivedEventHandler(watcher_EventRemoved);
            watcherRemove.Query = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 3");
            watcherRemove.Start();
        }

        /// <summary>
        /// Used to dispose of the USB device watchers when the USBControl class is disposed of.
        /// </summary>
        public void Dispose()
        {
            watcherAttach.Stop();
            watcherRemove.Stop();
            //Thread.Sleep(1000);
            watcherAttach.Dispose();
            watcherRemove.Dispose();
            //Thread.Sleep(1000);
        }

        void watcher_EventArrived(object sender, EventArrivedEventArgs e)
        {
            Debug.WriteLine("watcher_EventArrived");
        }

        void watcher_EventRemoved(object sender, EventArrivedEventArgs e)
        {
            Debug.WriteLine("watcher_EventRemoved");
        }

        ~USBControl()
        {
            this.Dispose();
        }


    }
}
