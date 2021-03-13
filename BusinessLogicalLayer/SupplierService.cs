using BusinessLogicalLayer.Helper;
using BusinessLogicalLayer.Interfaces;
using Common;
using DataAccessLayer;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer
{
    class SupplierService : BaseValidator<Supplier>, ISupplierService
    {
        public override Response Validate(Supplier supplier)
        {
            AddError(supplier.PersonResponsible.AuthenticateName());
            AddError(supplier.Telephone.AuthenticatePhoneNumber());
            if (!string.IsNullOrWhiteSpace(supplier.Telephone2))
            {
                AddError(supplier.Telephone2.AuthenticatePhoneNumber());
            }
            supplier.CNPJ_CPF = supplier.CNPJ_CPF.RemoveMask();
            // Verifica se é CPF ou CNPJ pelo tamanho e roda a validação especifica de cada
            if (supplier.CNPJ_CPF.Length == 11)
            {
                AddError(supplier.CNPJ_CPF.AuthenticateCPF());
                AddError(supplier.RG.AuthenticateRG());
                int baseyear = DateTime.Now.Year - 18;
                foreach (Company item in supplier.Companies)
                {
                    if (item.State == Entities.Enums.UF.PR)
                    {
                        if (baseyear < supplier.BirthDate.Year)
                        {
                            AddError("O fornecedor pessoa fisica não pode ser menor de idade neste estado");
                        }
                        if (baseyear == supplier.BirthDate.Year)
                        {
                            if (DateTime.Now.Month < supplier.BirthDate.Month)
                            {
                                AddError("O fornecedor pessoa física não pode ser menor de idade neste estado");
                            }
                            if (DateTime.Now.Month == supplier.BirthDate.Month)
                            {
                                if (DateTime.Now.Day < supplier.BirthDate.Day)
                                {
                                    AddError("O fornecedor pessoa física não pode ser menor de idade neste estado");
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                AddError(supplier.CNPJ_CPF.AuthenticateCNPJ());
            }
            return base.Validate(supplier);
        }

        public async Task<Response> Insert(Supplier supplier)
        {
            Response response = Validate(supplier);
            if (response.Success)
            {
                supplier.CNPJ_CPF = supplier.CNPJ_CPF.RemoveMask();
                supplier.Telephone = supplier.Telephone.RemoveMaskPhoneNumber();
                supplier.DateRegistration = DateTime.Now;
                supplier.Active = true;
                if (!string.IsNullOrWhiteSpace(supplier.Telephone2))
                {
                    supplier.Telephone2.RemoveMaskPhoneNumber();
                }
                if (supplier.CNPJ_CPF.Length == 11)
                {
                    supplier.RG = supplier.RG.RemoveMask();

                }
                try
                {
                    using (KnowledgeTestDB dataBase = new KnowledgeTestDB())
                    {
                        dataBase.Companies.AttachRange(supplier.Companies);
                        dataBase.Suppliers.Add(supplier);
                        await dataBase.SaveChangesAsync();
                        return ResponseMessage.CreateSuccessResponse();
                    }
                }
                catch (Exception ex)
                {
                    return ResponseMessage.CreateErrorResponse(ex);
                }
            }
            return ResponseMessage.CreateErrorResponse(response.Exception);
        }

        public async Task<QueryResponse<Supplier>> GetAll()
        {
            try
            {
                using (KnowledgeTestDB dataBase = new KnowledgeTestDB())
                {
                    List<Supplier> supplier = await dataBase.Suppliers.Where(c => c.Active == true).Include(c => c.Companies).ToListAsync(); ;
                    return ResponseMessage.QuerySuccessResponse(supplier);
                }
            }
            catch (Exception ex)
            {
                return ResponseMessage.QueryErrorResponse<Supplier>(ex);
            }
        }

        public async Task<Response> Update(Supplier supplier)
        {
            Response Response = Validate(supplier);
            if (Response.Success)
            {
                try
                {
                    using (KnowledgeTestDB dataBase = new KnowledgeTestDB())
                    {
                        dataBase.Entry(supplier).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        await dataBase.SaveChangesAsync();
                    }
                    return ResponseMessage.CreateSuccessResponse();
                }
                catch (Exception ex)
                {
                    return ResponseMessage.CreateErrorResponse(ex);
                }
            }
            return ResponseMessage.CreateErrorResponse(Response.Exception);
        }

        public async Task<SingleResponse<Supplier>> GetByID(int id)
        {
            try
            {
                using (KnowledgeTestDB dataBase = new KnowledgeTestDB())
                {
                    Supplier supplier = await dataBase.Suppliers.Include(p => p.Companies).FirstOrDefaultAsync(p => p.ID == id);
                    if (supplier == null)
                    {
                        return ResponseMessage.NotFoundData<Supplier>();
                    }
                    return ResponseMessage.SingleSuccessResponse(supplier);
                }
            }
            catch (Exception ex)
            {
                SingleResponse<Supplier> supplier = ResponseMessage.ErrorFoundingData<Supplier>(ex);
                return supplier;
            }
        }

        public async Task<Response> Delete(int id)
        {
            try
            {
                Supplier supplier = new Supplier();
                supplier.ID = id;
                using (KnowledgeTestDB dataBase = new KnowledgeTestDB())
                {
                    dataBase.Entry(supplier).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                    await dataBase.SaveChangesAsync();
                }
                return ResponseMessage.CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseMessage.CreateErrorResponse(ex);
            }
        }

        public async Task<QueryResponse<Supplier>> GetByName(string name)
        {
            try
            {
                using (KnowledgeTestDB dataBase = new KnowledgeTestDB())
                {
                    List<Supplier> supplier = await dataBase.Suppliers.Include(p => p.Companies).Where(p=>p.Active == true).Where(p => p.PersonResponsible.Contains(name)).ToListAsync();
                    if (supplier == null)
                    {
                        return ResponseMessage.QueryErrorResponse<Supplier>(null);
                    }
                    return ResponseMessage.QuerySuccessResponse(supplier);
                }
            }
            catch (Exception ex)
            {
                return ResponseMessage.QueryErrorResponse<Supplier>(ex);
            }
        }

        public async Task<QueryResponse<Supplier>> GetByCpfOrCnpj(string cpfOuCnpj)
        {
            try
            {
                using (KnowledgeTestDB dataBase = new KnowledgeTestDB())
                {
                    List<Supplier> supplier = await dataBase.Suppliers.Include(p => p.Companies).Where(p => p.Active == true).Where(p => p.CNPJ_CPF == cpfOuCnpj).ToListAsync();
                    if (supplier == null)
                    {
                        return ResponseMessage.QueryErrorResponse<Supplier>(null);
                    }
                    return ResponseMessage.QuerySuccessResponse(supplier);
                }
            }
            catch (Exception ex)
            {
                return ResponseMessage.QueryErrorResponse<Supplier>(ex);
            }
        }

        public async Task<QueryResponse<Supplier>> GetByRegisterDate(DateTime dateTime)
        {
            try
            {
                using (KnowledgeTestDB dataBase = new KnowledgeTestDB())
                {
                    List<Supplier> supplier = await dataBase.Suppliers.Include(p => p.Companies).Where(p => p.Active == true).Where(p => p.DateRegistration == dateTime).ToListAsync();
                    if (supplier == null)
                    {
                        return ResponseMessage.QueryErrorResponse<Supplier>(null);
                    }
                    return ResponseMessage.QuerySuccessResponse(supplier);
                }
            }
            catch (Exception ex)
            {
                return ResponseMessage.QueryErrorResponse<Supplier>(ex);
            }
        }
    }
}
