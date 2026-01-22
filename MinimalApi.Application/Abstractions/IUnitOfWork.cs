using System;
using System.Collections.Generic;
using System.Text;

namespace MinimalApi.Application.Abstractions
{
    public interface IUnitOfWork
    {
        // Returns number of state entries written to the database
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}