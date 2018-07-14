using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IContactDetailService
    {
        ContactDetail GetContactDetail();
    }

    public class ContactDetailService:IContactDetailService
    {
        IContactDetailRepository _contactDetailRepository;
        IUnitOfWork _unitOfWork;

        public ContactDetailService(IUnitOfWork unitOfWork, IContactDetailRepository contactDetailRepository)
        {
            this._contactDetailRepository = contactDetailRepository;
            _unitOfWork = unitOfWork;
        }

        public ContactDetail GetContactDetail()
        {
            return _contactDetailRepository.GetSingleByCondition(x => x.Status);
        }
    }
}