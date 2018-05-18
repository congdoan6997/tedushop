using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    public interface IVisitirStatisticRepository
    {
    }

    public class VisitorStatisticRepository : RepositoryBase<VisitorStatistic>, IVisitirStatisticRepository
    {
        public VisitorStatisticRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}