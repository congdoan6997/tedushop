using System.Collections.Generic;
using System.Linq;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IProductCategoryService
    {
        ProductCategory Add(ProductCategory productCategory);

        void Update(ProductCategory productCategory);

        ProductCategory Delete(int id);

        IEnumerable<ProductCategory> GetAll();

        IEnumerable<ProductCategory> GetAllByParentId(int parentId);

        IEnumerable<ProductCategory> GetAll(string keyword);

        ProductCategory GetById(int id);

            

        void SaveChanges();
    }

    public class ProductCategoryService : IProductCategoryService
    {
        private IProductCategoryRepository _productCategoryRepository;
        private IUnitOfWork _unitOfWork;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository, IUnitOfWork unitOfWork)
        {
            this._productCategoryRepository = productCategoryRepository;
            this._unitOfWork = unitOfWork;
        }

        public ProductCategory Add(ProductCategory productCategory)
        {
            return this._productCategoryRepository.Add(productCategory);
        }

        public ProductCategory Delete(int id)
        {
            return this._productCategoryRepository.Delete(id);
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            return this._productCategoryRepository.GetAll();
        }

        public IEnumerable<ProductCategory> GetAll(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return this._productCategoryRepository.GetAll();
            }
            return this._productCategoryRepository.GetMulti(c => c.Name.Contains(keyword) || c.MetaDescription.Contains(keyword));
        }

        public IEnumerable<ProductCategory> GetAllByParentId(int parentId)
        {
            return this._productCategoryRepository.GetMulti(x => x.ParentID == parentId && x.Status);
        }

        public ProductCategory GetById(int id)
        {
            return this._productCategoryRepository.GetSingleById(id);
        }



        public void SaveChanges()
        {
            this._unitOfWork.Commit();
        }

        public void Update(ProductCategory productCategory)
        {
            this._productCategoryRepository.Update(productCategory);
        }
    }
}