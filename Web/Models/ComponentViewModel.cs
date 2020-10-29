using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainClass.Models;

namespace Web.Models
{
    public class ComponentViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Packages> Packages { get; set; }
        public IEnumerable<Dooreha> Dooreh { get; set; }
    }
}