﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenPro.WebClient.Models
{
    public class CityModel
    {
        public int Id { get; set; }
        public int StateId { get; set; }
        public string CityName { get; set; }
    }


    public class GarageSearchModel
    {
        public int GarageId { get; set; }
        public string Garage_Name { get; set; }
    }
}