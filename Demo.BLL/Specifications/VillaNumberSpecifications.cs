using Demo.DAL.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Specifications
{
    class VillaNumberSpecifications : BaseSpecifications<VillaNumber>
    {
        public VillaNumberSpecifications()
        {
            Includes.Add(VN => VN.Villa);
        }

        public VillaNumberSpecifications(int id) : base(VN => VN.Id == id)
        {
            Includes.Add(VN => VN.Villa);
        }
    }
}
