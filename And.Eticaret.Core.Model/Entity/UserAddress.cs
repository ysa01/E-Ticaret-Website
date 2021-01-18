using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace And.Eticaret.Core.Model.Entity
{
  public  class UserAddress:EntityBase
    {
        public int UserID { get; set; }
        public User User { get; set; }  // İkisi birbirini keyledi UserID ilişki kurması gerektiği için bu satırdaki probu koyduk
        public string Title { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
    }
}
