using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Counter.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Counter.Controllers
{

    [ApiController]
    public class CounterController : ControllerBase
    {
        [Route("api/GetCounter")]
        [HttpGet]
        public CounterResult ReadCounter()
        {
            GCHandle handle = GCHandle.Alloc(GlobalVariables.Counter, GCHandleType.Pinned | GCHandleType.WeakTrackResurrection);
            try
            {
                IntPtr pointer = GCHandle.ToIntPtr(handle);
                return new CounterResult { CounterValue = GlobalVariables.Counter, IncreasmentCalls = GlobalVariables.NumberOfCalls, MemoryAddress = "0x" + pointer.ToString("X") };
            }
            finally
            {
                handle.Free();
            }
            /*
             * this is another way to find an address of pointer variable, but this time project should be build in unsafe mode. 
             * What is not preferable.
            unsafe
            {
                int Value = GlobalVariables.Counter;   
                int* Address = &Value;   
                return new CounterResult { CounterValue = GlobalVariables.Counter, IncreasmentCalls = GlobalVariables.NumberOfCalls, MemoryAddress = "0x" + ((int)Address).ToString("X") };

            }
            */

        }
        [Route("api/SetCounter")]
        [HttpPost]
        public HttpResponseMessage SetCounter()
        {
            /*
                * It was at this point that no further instructions were made available. 
                * It opened way to the most possible three options
                 1. First Would be; after second call counter is increased always by two times in 

                     0 + 1 = 1 increasment by 1
                     1 + 2 = 3 increasment by two times in a row
                     3 + 6 = 9 again increasment by two times in a row
                     9 + 18 = 27  again increasment by two times in a row
                     .....
                     .....
                     .....
                     so pattern should go as 
                     1, 3, 9, 27, 81, 243 .... 3^x
                     so the calculation would be
            
             *****************************************************************************************************
                     GlobalVariables.Counter += GlobalVariables.Counter == 0 ? 1 : GlobalVariables.Counter * 2;
                     // Or
                     GlobalVariables.Counter = Convert.ToInt32(Math.Pow(3, GlobalVariables.NumberOfCalls));


                    GlobalVariables.NumberOfCalls++;
             *****************************************************************************************************
             
            2. Another way is to calculate the value after the second call and follow the same pattern from the beginning:
                0 + 1 = 1 increasment by 1
                1 + 2 = 3 increasment by two times in a row
                3 + 1 = 4 increasment by 1
                4 + 8 = 12 increasment by two times in a row
                12 + 1 = 13 increasment by 1
                .....
                .....
                .....
                so pattern should go as 
                1, 3, 4, 12, 13, 39, 40 .... pattern
                so the calculation would be

                This approach would be the most obvious to adopt, but it is a 100% guaranteed choice:-(
            */

            GlobalVariables.NumberOfCalls++;
            GlobalVariables.Counter += GlobalVariables.Counter == 0 ? 1 : GlobalVariables.NumberOfCalls % 2 != 0 ? 1 : (GlobalVariables.Counter * 2);

            /*
             3. And last possibility may be that it always increase by one time more in a row
                2 times in a row
                3 times in a row
                4 times in a row
                5 times in a row
                or
                0 + 1 = 1 increasment by 1
                1 + 2 = 3 increasment by two times in a row
                3 + 9 = 12 increasment by three times in a row 
                12 + 48 = 60 increasment by four times in a row 
                60 + 300 = 360 increasment by five times in a row 
                And calculation should be as 
            
            *****************************************************************************************************

                GlobalVariables.NumberOfCalls++;
                GlobalVariables.Counter += GlobalVariables.Counter == 0 ? 1 : (GlobalVariables.NumberOfCalls * GlobalVariables.Counter);

            *****************************************************************************************************
            * 
             */


            return new HttpResponseMessage(HttpStatusCode.OK);
        }
        [Route("api/ResetCounter")]
        [HttpPost]
        public HttpResponseMessage ResetCounter()
        {
            GlobalVariables.NumberOfCalls = 0;
            GlobalVariables.Counter = 0;
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
