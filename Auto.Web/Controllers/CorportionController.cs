using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace Auto.Web.Controllers
{
    public class CorportionController : BaseController
    {
        public IActionResult Creation()
        {
            return View();
        }

    }
}
