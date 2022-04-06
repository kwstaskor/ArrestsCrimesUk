#nullable disable
using System.Data.Common;
using Entities;

namespace RepositoryService;

public class VictimsRepos : Repository<Victim>
{
    public VictimsRepos(DbTransaction transaction) : base(transaction, "Victims")
    {
    }
}