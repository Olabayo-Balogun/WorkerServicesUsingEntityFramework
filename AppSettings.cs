﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerServicesUsingEntityFramework
{
    //We're making this static so we can use it anywhere
    public static class AppSettings
    {
        //The properties of static classes should also be static which is what we're obeying below
        public static IConfiguration Configuration { get; set; }
    }
}
