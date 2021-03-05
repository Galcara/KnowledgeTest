using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModelViewController.Models;

namespace ModelViewController.Data
{
    public class KnowledgeTestDB : DbContext
    {
        public KnowledgeTestDB (DbContextOptions<KnowledgeTestDB> options)
            : base(options)
        {
        }

        public DbSet<ModelViewController.Models.CompanyQueryViewModel> CompanyQueryViewModel { get; set; }
    }
}
