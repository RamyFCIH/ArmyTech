using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArmyTech.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;


namespace ArmyTech
{
    public class DatabaseTransaction : IDatabaseTransaction
    {
        private readonly ArmyTechTaskContext _context;
        private readonly IDbContextTransaction _transaction;


        public DatabaseTransaction(ArmyTechTaskContext context)
        {
            _context = context;
            _transaction = context.Database.BeginTransaction();
        }

        public IDatabaseTransaction BeginTransaction()
        {
            return new DatabaseTransaction(_context);
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }
    }
}
