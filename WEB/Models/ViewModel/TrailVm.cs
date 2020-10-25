using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace WEB.Models.ViewModel
{
    public class TrailVm
    {
        public IEnumerable<SelectListItem> NationaParkList { get; set; }

        public Trail Trail { get; set; }

    }
}
