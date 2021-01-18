using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace And.Eticaret.Core.Model.Entity
{
    public class Category:EntityBase
    {
        public string Name { get; set; }
        public int ParentID { get; set; } = 0;
        public bool IsActive { get; set; }
       
    }
}
