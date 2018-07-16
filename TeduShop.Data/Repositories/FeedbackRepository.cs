using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    public interface IFeedbackRepository:IRepository<Model.Models.Feedback>
    {

    }
    public class FeedbackRepository : RepositoryBase<Model.Models.Feedback>,IFeedbackRepository 
    {
        public FeedbackRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
