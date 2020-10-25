using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB.Models.ViewModel
{
    public class Index_VM
    {
        public IEnumerable<NationalPark> NationalParkList { get; set; }
        public IEnumerable<Trail> Trails { get; set; }

    }
}
