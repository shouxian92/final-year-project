using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FYP_SEAT_NUMBER_CHECKER
{
    class SvcAddr
    {
        public static string HOST = "http://192.168.1.8/FYP/svc/";
        public static string HALLS = HOST + "halls";
        public static string SEAT_NUMBER = HOST + "seatQuery";
    }
}
