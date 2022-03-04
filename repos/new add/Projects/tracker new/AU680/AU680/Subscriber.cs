using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace AU680
{
    class Subscriber
    {
        SerialPort _serialPort;
        static string ack = char.ConvertFromUtf32(6);
        public Subscriber(SerialPort _serialPort)
        {
            this._serialPort = _serialPort;
        }

        public void Read()
        {
            Console.WriteLine("Listening For a message");
            while (true)
            {
                try
                {
                    string message = _serialPort.ReadLine();
                    Console.WriteLine("received msg : {0}", message);
                    _serialPort.WriteLine(ack);
                }
                catch (TimeoutException) { }
            }
        }
    }
}
