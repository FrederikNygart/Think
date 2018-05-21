using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ThinkRest.Controllers
{
    public class PredictController : ApiController
    {
        public double GetError() => Util.GetErrorRate();
    }
}
