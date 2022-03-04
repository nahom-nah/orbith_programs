﻿using System;
using System.IO.Ports;

class PortDataReceived
{
    public static void Main()
    { 


         
        SerialPort mySerialPort = new SerialPort("COM3");

        mySerialPort.BaudRate = 9600;
        mySerialPort.Parity = Parity.None;
        mySerialPort.StopBits = StopBits.One;
        mySerialPort.DataBits = 8;
        mySerialPort.Handshake = Handshake.None;

        mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

        mySerialPort.Open();

        Console.WriteLine("Press any key to continue...");
        Console.WriteLine();
        Console.ReadKey();
        mySerialPort.Close();
    }


    
         
 
    private static void DataReceivedHandler(
                        object sender,
                        SerialDataReceivedEventArgs e)
    {
        SerialPort sp = (SerialPort)sender;
        string indata = sp.ReadExisting();
        //Console.WriteLine("Data Received:");
        //Console.Write(indata); 
        callmyfunction(indata);
    }

    private static void callmyfunction(string data)
    {
        //....
        Console.WriteLine("i think we have received the data : {0}",data);
    }
}
 
 