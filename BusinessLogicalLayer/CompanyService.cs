﻿using BusinessLogicalLayer.Helper;
using BusinessLogicalLayer.Interfaces;
using Common;
using Entities;
using System;
using System.Threading.Tasks;
using Entities.Enums;
using DataAccessLayer;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BusinessLogicalLayer
{
    public class CompanyService : BaseValidator<Company>, ICompanyService
    {
        public override Response Validate(Company company)
        {
            AddError(company.CommercialName.AuthenticateName());
            AddError(company.CPNJ.AuthenticateCNPJ());
            
            return base.Validate(company);
        }

        public async Task<Response> Insert(Company company)
        {
            Response response = Validate(company);
            if (response.Success)
            {
                company.CPNJ = company.CPNJ.RemoveMask();
                company.Active = true;
                try
                {
                    using (KnowledgeTestDB dataBase = new KnowledgeTestDB())
                    {

                        dataBase.Companies.Add(company);
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

        public async Task<QueryResponse<Company>> GetAll()
        {
            try
            {
                using (KnowledgeTestDB dataBase = new KnowledgeTestDB())
                {
                    List<Company> companies = await dataBase.Companies.Where(c => c.Active == true).Include(c => c.Supplier).ToListAsync(); ;
                    return ResponseMessage.QuerySuccessResponse<Company>(companies);
                }
            }
            catch (Exception ex)
            {
                QueryResponse<Company> subject = (QueryResponse<Company>)ResponseMessage.QueryErrorResponse<Company>(ex);
                return subject;
            }
        }
        
        public async Task<Response> Update(Company company)
        {
            Response Response = Validate(company);
            if (Response.Success)
            {
                try
                {
                    using (KnowledgeTestDB dataBase = new KnowledgeTestDB())
                    {
                        dataBase.Entry(company).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

        public async Task<SingleResponse<Company>> GetByID(int id)
        {
            try
            {
                using (KnowledgeTestDB dataBase = new KnowledgeTestDB())
                {
                    Company company = await dataBase.Companies.FirstOrDefaultAsync(p => p.ID == id);
                    if (company == null)
                    {
                        return ResponseMessage.SingleSuccessResponse<Company>(company);
                    }
                    return ResponseMessage.NotFoundData<Company>();
                }
            }
            catch (Exception ex)
            {
                SingleResponse<Company> company = (SingleResponse<Company>)ResponseMessage.ErrorFoundingData<Company>(ex);
                return company;
            }
        }

    }
}
