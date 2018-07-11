using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Common;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface ICommonService
    {
        Footer GetFooter();
        IEnumerable<Slide> GetSlides();
    }
    public class CommonService : ICommonService
    {
        ISlideRepository _slideRepository;
        IFooterRepository _footerRepository;
        IUnitOfWork _unitOfWork;
        public CommonService(IFooterRepository footerRepository,ISlideRepository slideRepository, IUnitOfWork unitOfWork)
        {
            this._slideRepository = slideRepository;
            this._footerRepository = footerRepository;
            this._unitOfWork = unitOfWork;
        }
        public Footer GetFooter()
        {
            return this._footerRepository.GetSingleByCondition(x => x.ID == CommonContants.DefaultFooterId);
        }

        public IEnumerable<Slide> GetSlides()
        {
            return this._slideRepository.GetMulti(x =>x.Status);
        }
    }
}
