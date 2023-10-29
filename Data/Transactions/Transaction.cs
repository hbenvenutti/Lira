using System.Transactions;
using Lira.Application.Transactions;

namespace Lira.Data.Transactions;

public class Transaction : ITransaction
{
    private readonly TransactionScope _transaction =
        new(TransactionScopeAsyncFlowOption.Enabled);

    public void Commit()
    {
        _transaction.Complete();
    }

    public void Dispose()
    {
        _transaction.Dispose();
    }
}
