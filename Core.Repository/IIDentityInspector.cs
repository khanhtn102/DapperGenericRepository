using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IIDentityInspector<TEntity> where TEntity : class
    {
        string GetColumnsIdentityForType();
    }
}
