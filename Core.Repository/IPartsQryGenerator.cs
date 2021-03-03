using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IPartsQryGenerator<TEntity> where TEntity : class
    {
        string GenerateSelect();
        string GenerateSelect(object fieldsFilter);
        string GenerateInsert(string identityField = null);
        string GenerateDelete(object parameters);
        string GenerateUpdate(object pks);
    }
}
