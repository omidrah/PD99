﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceLayer.Models
{
    public class PackageTreeVM
    {
       public IEnumerable<PackageVM> PackagesVM { get; set; }
        public PackageVM Package { get; set; }
    }
}