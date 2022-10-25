using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entites.Identity;

namespace Core.interfaces
{
    public interface ITokenServcice
    {
        string CreateToken(AppUser user);
    }
}