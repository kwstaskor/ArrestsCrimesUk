#nullable disable
using System.Data.Common;
using Entities;

namespace RepositoryService;

public class ArrestsRepos : Repository<Arrest>
{
    public ArrestsRepos(DbTransaction transaction) : base(transaction, "Arrests")
    {
    }
}