//
// Futurelight PLB 280 + OpenDMX test app
//
// Copyright (c) 2017 Martin Cvengros
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
// based on
// Linked2 Software (www.Linked2Software.com) Building Lighting Control Systems DMX-512 Test Application
// Written by Richard A. Blackwell with example source provided by Enttec.com
//
// Windows OpenDMX drivers from http://www.ftdichip.com/Drivers/CDM/CDM21218_Setup.zip
//
using System;
using System.Runtime.InteropServices;
using System.Threading;

public class OpenDMX
{
    static byte[] buffer = new byte[512];
    static uint handle;
    static int bytesWritten = 0;
    static FT_STATUS status;

    const byte BITS_8 = 8;
    const byte STOP_BITS_2 = 2;
    const byte PARITY_NONE = 0;
    const UInt16 FLOW_NONE = 0;
    const byte PURGE_RX = 1;
    const byte PURGE_TX = 2;


    [DllImport("FTD2XX.dll")]
    static extern FT_STATUS FT_Open(UInt32 uiPort, ref uint ftHandle);
    [DllImport("FTD2XX.dll")]
    static extern FT_STATUS FT_Close(uint ftHandle);
    [DllImport("FTD2XX.dll")]
    static extern FT_STATUS FT_Read(uint ftHandle, IntPtr lpBuffer, UInt32 dwBytesToRead, ref UInt32 lpdwBytesReturned);
    [DllImport("FTD2XX.dll")]
    static extern FT_STATUS FT_Write(uint ftHandle, IntPtr lpBuffer, UInt32 dwBytesToRead, ref UInt32 lpdwBytesWritten);
    [DllImport("FTD2XX.dll")]
    static extern FT_STATUS FT_SetDataCharacteristics(uint ftHandle, byte uWordLength, byte uStopBits, byte uParity);
    [DllImport("FTD2XX.dll")]
    static extern FT_STATUS FT_SetFlowControl(uint ftHandle, char usFlowControl, byte uXon, byte uXoff);
    [DllImport("FTD2XX.dll")]
    static extern FT_STATUS FT_GetModemStatus(uint ftHandle, ref UInt32 lpdwModemStatus);
    [DllImport("FTD2XX.dll")]
    static extern FT_STATUS FT_Purge(uint ftHandle, UInt32 dwMask);
    [DllImport("FTD2XX.dll")]
    static extern FT_STATUS FT_ClrRts(uint ftHandle);
    [DllImport("FTD2XX.dll")]
    static extern FT_STATUS FT_SetBreakOn(uint ftHandle);
    [DllImport("FTD2XX.dll")]
    static extern FT_STATUS FT_SetBreakOff(uint ftHandle);
    [DllImport("FTD2XX.dll")]
    static extern FT_STATUS FT_GetStatus(uint ftHandle, ref UInt32 lpdwAmountInRxQueue, ref UInt32 lpdwAmountInTxQueue, ref UInt32 lpdwEventStatus);
    [DllImport("FTD2XX.dll")]
    static extern FT_STATUS FT_ResetDevice(uint ftHandle);
    [DllImport("FTD2XX.dll")]
    static extern FT_STATUS FT_SetDivisor(uint ftHandle, char usDivisor);

    static Thread writer = null;

    /// <summary>
    /// Optional call
    /// </summary>
    public static void Start()
    {
        handle = 0;
        status = FT_Open(0, ref handle);

        if (OpenDMX.status == FT_STATUS.FT_OK)
        {
            InitOpenDMX();

            writer = (new Thread(new ThreadStart(WriteData)));
            writer.Start();
        }
    }

    /// <summary>
    /// Call Stop to abort the writer thread
    /// </summary>
    public static void Stop()
    {
        writer.Abort();
    }

    /// <summary>
    /// Channel + value - nothing extraordinary going on.
    /// Calls Start if needed.
    /// </summary>
    /// <param name="channel"></param>
    /// <param name="value"></param>
    public static void SetDmxValue(int channel, byte value)
    {
        if (handle == 0)
            Start();

        if (buffer != null)
        {
            buffer[channel] = value;
        }
    }

    static void WriteData()
    {
        for (;;)
        {
            if (OpenDMX.status == FT_STATUS.FT_OK)
            {
                status = FT_SetBreakOn(handle);
                status = FT_SetBreakOff(handle);
                bytesWritten = Write(handle, buffer, buffer.Length);

                // TODO: error handling
                if (bytesWritten != buffer.Length)
                    Console.WriteLine("Communication error");
            }

            Thread.Sleep(50);
        }
    }

    static int Write(uint handle, byte[] data, int length)
    {
        IntPtr ptr = Marshal.AllocHGlobal((int)length);
        Marshal.Copy(data, 0, ptr, (int)length);
        uint bytesWritten = 0;
        status = FT_Write(handle, ptr, (uint)length, ref bytesWritten);
        return (int)bytesWritten;
    }

    static void InitOpenDMX()
    {
        status = FT_ResetDevice(handle);
        status = FT_SetDivisor(handle, (char)12);  // set baud rate
        status = FT_SetDataCharacteristics(handle, BITS_8, STOP_BITS_2, PARITY_NONE);
        status = FT_SetFlowControl(handle, (char)FLOW_NONE, 0, 0);
        status = FT_ClrRts(handle);
        status = FT_Purge(handle, PURGE_TX);
        status = FT_Purge(handle, PURGE_RX);
    }
}

/// <summary>
/// DLL functions return status.
/// </summary>
enum FT_STATUS
{
    FT_OK = 0,
    FT_INVALID_HANDLE,
    FT_DEVICE_NOT_FOUND,
    FT_DEVICE_NOT_OPENED,
    FT_IO_ERROR,
    FT_INSUFFICIENT_RESOURCES,
    FT_INVALID_PARAMETER,
    FT_INVALID_BAUD_RATE,
    FT_DEVICE_NOT_OPENED_FOR_ERASE,
    FT_DEVICE_NOT_OPENED_FOR_WRITE,
    FT_FAILED_TO_WRITE_DEVICE,
    FT_EEPROM_READ_FAILED,
    FT_EEPROM_WRITE_FAILED,
    FT_EEPROM_ERASE_FAILED,
    FT_EEPROM_NOT_PRESENT,
    FT_EEPROM_NOT_PROGRAMMED,
    FT_INVALID_ARGS,
    FT_OTHER_ERROR
};
