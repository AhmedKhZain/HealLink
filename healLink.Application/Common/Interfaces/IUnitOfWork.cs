namespace healLink.Application.Common.Interfaces;

public interface IUnitOfWork
{
    Task CommitChangesAsync();
}