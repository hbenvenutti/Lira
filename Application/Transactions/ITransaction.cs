namespace Lira.Application.Transactions;

public interface ITransaction
{
    public void Commit();
    public void Dispose();
}
