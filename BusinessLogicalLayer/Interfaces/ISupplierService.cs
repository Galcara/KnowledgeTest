using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Interfaces
{
    public interface ISupplierService : IEntityCRUD<Supplier>
    {
        Task<QueryResponse<Supplier>> GetByName(string name);
        Task<QueryResponse<Supplier>> GetByCpfOrCnpj(string cpfOuCnpj);
        Task<QueryResponse<Supplier>> GetByRegisterDate(DateTime dateTime);

    }
}
