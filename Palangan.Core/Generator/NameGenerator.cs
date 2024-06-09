using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palangan.Core.Generator
{
    public  class NameGenerator
    {
        public static string GenerateUniqeName()
        {
            return Guid.NewGuid().ToString().Replace("_","");
        }
    }
}
