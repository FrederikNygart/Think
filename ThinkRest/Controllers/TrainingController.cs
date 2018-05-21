using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ThinkRest.Controllers
{
    public class TrainingController : ApiController
    {
        public string Get()
        {
            try
            {

                for (var i = 0; i < 2; i++)
                {
                    Util.TrainEpoch();
                }
                return "ok";
            }
            catch
            {
                return "error";
            }
        }
    }
}
