using BusinessLogicalLayer.Helper;
using BusinessLogicalLayer.Interfaces;
using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer
{
    class SupplierService : BaseValidator<Supplier>, ISupplierService
    {
        public override Response Validate(Supplier supplier)
        {
            if (supplier.CNPJ_CPF.Length == 11)
            {
                
                AddError(supplier.CNPJ_CPF.AuthenticateCPF());
                AddError(supplier.RG.AuthenticateRG());
            }
            else if (supplier.CNPJ_CPF.Length == 14)
            {
                AddError(supplier.CNPJ_CPF.AuthenticateCNPJ());
            }
            else
            {
                AddError("Insira um CPF ou CNPJ valido");
            }
            return base.Validate(supplier);
        }
        public Task<Response> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<QueryResponse<Supplier>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<SingleResponse<Supplier>> GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response> Insert(Supplier item)
        {
            throw new NotImplementedException();
        }

        public Task<Response> Update(Supplier item)
        {
            throw new NotImplementedException();
        }
    }
}
