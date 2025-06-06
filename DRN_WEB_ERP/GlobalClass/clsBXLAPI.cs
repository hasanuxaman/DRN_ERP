﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;


namespace DRN_WEB_ERP.GlobalClass
{
    public class clsBXLAPI
    {
        //[DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //public static extern bool IsWow64Process([In] IntPtr hProcess, [Out] out bool lpSystemInfo);
        ////C# 64bit OS runtime check
        //public static bool Is64Bit()
        //{
        //    bool retVal;

        //    IsWow64Process(System.Diagnostics.Process.GetCurrentProcess().Handle, out retVal);

        //    return retVal;
        //}
        public static bool Is64Bit()
        {
            bool retVal = true;
            if (System.Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE") == "x86")
                retVal = false;

            return retVal;
        }

        /////////////////////////////////////////////////////////////////
        //  Constant List

        //	Rotation
        public const int ROTATE_0   = 0;
        public const int ROTATE_90  = 1;
        public const int ROTATE_180 = 2;
        public const int ROTATE_270 = 3;


        //
        public delegate int BXLCallBackDelegate(int status);
        
        //////////////////////////////////////////////////////////////////////
        //  Function List
        public static bool BidiOpenMonPrinter(string szPrinterName)
        {
            if (Is64Bit())
                return clsBXLAPI_x64.BidiOpenMonPrinter(szPrinterName);
            else
                return clsBXLAPI_x86.BidiOpenMonPrinter(szPrinterName);
        }

        public static bool BidiCloseMonPrinter()
        {
            if (Is64Bit())
                return clsBXLAPI_x64.BidiCloseMonPrinter();
            else
                return clsBXLAPI_x86.BidiCloseMonPrinter();
        }

        public static bool BidiSetStatusBackFunction(BXLCallBackDelegate cbd)
        {
            if (Is64Bit())
                return clsBXLAPI_x64.BidiSetStatusBackFunction(cbd);
            else
                return clsBXLAPI_x86.BidiSetStatusBackFunction(cbd);
        }

        public static int BidiGetStatus()
        {
            if (Is64Bit())
                return clsBXLAPI_x64.BidiGetStatus();
            else
                return clsBXLAPI_x86.BidiGetStatus();
        }

        public static bool BidiCancelStatusBack()
        {
            if (Is64Bit())
                return clsBXLAPI_x64.BidiCancelStatusBack();
            else
                return clsBXLAPI_x86.BidiCancelStatusBack();
        }

        public static bool ConnectPrinter(string szPrinterName)
        {
            if (Is64Bit())
            {
                //MessageBox.Show("x64");
                return clsBXLAPI_x64.ConnectPrinter(szPrinterName);
            }
            else
            {
                //MessageBox.Show("x86");
                return clsBXLAPI_x86.ConnectPrinter(szPrinterName);
            }
        }

        public static void DisconnectPrinter()
        {
            if (Is64Bit())
            {
                //MessageBox.Show("x64");
                clsBXLAPI_x64.DisconnectPrinter();
            }
            else
            {
                //MessageBox.Show("x86");
                clsBXLAPI_x86.DisconnectPrinter();
            }
        }

        public static bool Start_Doc(string szDocName)
        {
            if (Is64Bit())
                return clsBXLAPI_x64.Start_Doc(szDocName);
            else
                return clsBXLAPI_x86.Start_Doc(szDocName);
        }

        public static void End_Doc()
        {
            if (Is64Bit())
                clsBXLAPI_x64.End_Doc();
            else
                clsBXLAPI_x86.End_Doc();
        }

        public static bool Start_Page()
        {
            if (Is64Bit())
                return clsBXLAPI_x64.Start_Page();
            else
                return clsBXLAPI_x86.Start_Page();
        }

        public static void End_Page()
        {
            if (Is64Bit())
                clsBXLAPI_x64.End_Page();
            else
                clsBXLAPI_x86.End_Page();
        }

        public static int PrintDeviceFont(int nPositionX, int nPositionY, string szFontName, int nFontSize, string szData)
        {
            if (Is64Bit())
                return clsBXLAPI_x64.PrintDeviceFont(nPositionX, nPositionY, szFontName, nFontSize, szData);
            else
                return clsBXLAPI_x86.PrintDeviceFont(nPositionX, nPositionY, szFontName, nFontSize, szData);
        }

        public static int PrintTrueFont(int nPositionX, int nPositionY, string szFontName, int nFontSize, string szData,
                                        bool bBold, int nRotation, bool bItalic, bool bUnderline)
        {
            if (Is64Bit())
                return clsBXLAPI_x64.PrintTrueFont(nPositionX, nPositionY, szFontName, nFontSize, szData,
                                                bBold, nRotation, bItalic, bUnderline);
            else
                return clsBXLAPI_x86.PrintTrueFont(nPositionX, nPositionY, szFontName, nFontSize, szData,
                                                bBold, nRotation, bItalic, bUnderline);
        }

        public static int PrintBitmap(int nPositionX, int nPositionY, string bitmapFile)
        {
            if (Is64Bit())
                return clsBXLAPI_x64.PrintBitmap(nPositionX, nPositionY, bitmapFile);
            else
                return clsBXLAPI_x86.PrintBitmap(nPositionX, nPositionY, bitmapFile);
        }
    }
}
