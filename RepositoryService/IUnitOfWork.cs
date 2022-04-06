#nullable disable
namespace RepositoryService;

public interface IUnitOfWork : IDisposable
{
    ArrestsRepos Arrests { get; } 
    VictimsRepos Victims { get; } 
    void Commit();
}