using System.Collections.Generic;
using TeduShop.Common;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IProductService
    {
        Product Add(Product product);

        void Update(Product product);

        Product Delete(int id);

        IEnumerable<Product> GetAll();

        //IEnumerable<Product> GetAllByParentId(int parentId);

        IEnumerable<Product> GetAll(string keyword);

        Product GetById(int id);

        void SaveChanges();
    }

    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private IProductTagRepository _productTagRepository;
        private ITagRepository _tagRepository;
        private IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IProductTagRepository productTagRepository, ITagRepository tagRepository, IUnitOfWork unitOfWork)
        {
            this._productRepository = productRepository;
            this._productTagRepository = productTagRepository;
            this._tagRepository = tagRepository;
            this._unitOfWork = unitOfWork;
        }

        public Product Add(Product product)
        {
            var item = this._productRepository.Add(product);
            this._unitOfWork.Commit();
            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] tags = product.Tags.Split(',');
                for (int i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if (this._tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag()
                        {
                            ID = tagId,
                            Name = tags[i],
                            Type = CommonContants.ProductTag
                        };
                        this._tagRepository.Add(tag);
                    }
                    ProductTag productTag = new ProductTag()
                    {
                        TagID = tagId,
                        ProductID = product.ID
                    };
                    this._productTagRepository.Add(productTag);
                }
            }
            //this._unitOfWork.Commit();
            return item;
        }

        public Product Delete(int id)
        {
            return this._productRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return this._productRepository.GetAll();
        }

        public IEnumerable<Product> GetAll(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return this._productRepository.GetAll();
            }
            return this._productRepository.GetMulti(c => c.Name.Contains(keyword) || c.MetaDescription.Contains(keyword));
        }

        //public IEnumerable<Product> GetAllByParentId(int parentId)
        //{
        //    return this._productRepository.GetMulti(x => x.ParentID == parentId && x.Status);
        //}

        public Product GetById(int id)
        {
            return this._productRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            this._unitOfWork.Commit();
        }

        public void Update(Product product)
        {
            this._productRepository.Update(product);
            //var item = this._productRepository.Add(product);
            //this._unitOfWork.Commit();
            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] tags = product.Tags.Split(',');
                for (int i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if (this._tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag()
                        {
                            ID = tagId,
                            Name = tags[i],
                            Type = CommonContants.ProductTag
                        };
                        this._tagRepository.Add(tag);
                    }
                    this._productTagRepository.DeleteMulti(x => x.ProductID == product.ID);
                    ProductTag productTag = new ProductTag()
                    {
                        TagID = tagId,
                        ProductID = product.ID
                    };
                    this._productTagRepository.Add(productTag);
                }
            }
            //this._unitOfWork.Commit();
            //return item;
        }
    }
}