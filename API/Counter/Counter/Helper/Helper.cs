using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Counter.Helper
{
    public static class GlobalVariables
    {
        public static string NullIF(this object obj)
        {
            if (obj == null)
                return "";
            else
                return obj.ToString().TrimStart().TrimEnd();
        }
        public static int Counter { get; set; }
        public static int NumberOfCalls { get; set; }
    }

    public class CounterResult
    {
        private int countervalue;
        private int increasmentcalls;
        private string memoaddress;
        public int CounterValue { get { return countervalue; } set { countervalue = value; } }
        public int IncreasmentCalls { get { return increasmentcalls; } set { increasmentcalls = value; } }
        public string MemoryAddress { get { return memoaddress.NullIF(); } set { memoaddress = value; } }
    }
}
