using System;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IPageService
    {
        Page GetByAlias(string alias);
    }

    public class PageService : IPageService
    {
        private IUnitOfWork _unitOfWork;
        private IPageRepository _pageRepository;

        public PageService(IUnitOfWork unitOfWork, IPageRepository pageRepository)
        {
            _unitOfWork = unitOfWork;
            _pageRepository = pageRepository;
        }

        public Page GetByAlias(string alias)
        {
            return _pageRepository.GetSingleByCondition(x => x.Alias == alias && x.Status);
        }
    }
}