﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyWeb
{
    public static class SD
    {
        public static string APIBaseUrl = "https://localhost:44387/";
        public static string NationalParkAPIPath = APIBaseUrl + "api/v1/nationalparks";
        public static string TrailApiPath = APIBaseUrl + "api/v1/trails";
    }
}
