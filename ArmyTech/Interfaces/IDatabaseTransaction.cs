using System;
using System.Collections.Generic;
using System.Text;

namespace ArmyTech
{
    public interface IDatabaseTransaction : IDisposable
    {
        void Commit();
        void Rollback();
        IDatabaseTransaction BeginTransaction();
    }
}
