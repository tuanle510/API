using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.MISAAttribute
{
    [AttributeUsage(AttributeTargets.Property|AttributeTargets.Field|AttributeTargets.Method)]
    public class PrimaryKey:Attribute
    {
    }
}
