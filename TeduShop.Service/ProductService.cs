using System.Collections.Generic;
using System.Linq;
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

        IEnumerable<Product> GetLastest(int top);

        IEnumerable<Product> GetHotProduct(int top);

        //IEnumerable<Product> GetAllByParentId(int parentId);
        IEnumerable<Product> GetListProductByCategoryIdPaging(int categoryId, int page, int pageSize, string sort, out int totalRow);

        IEnumerable<Product> GetListProductByName(string keyword, int page, int pageSize, string sort, out int totalRow);

        IEnumerable<Product> GetAll(string keyword);
        IEnumerable<Product> GetReatedProducts(int id, int top);


        Product GetById(int id);
        List<string> GetListProductByName(string keyword);
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

        public IEnumerable<Product> GetHotProduct(int top)
        {
            return this._productRepository.GetMulti(x => x.Status && x.HotFlag == true).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetLastest(int top)
        {
            return this._productRepository.GetMulti(x => x.Status).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetListProductByCategoryIdPaging(int categoryId, int page, int pageSize, string sort, out int totalRow)
        {
            var query = this._productRepository.GetMulti(x => x.Status && x.CategoryID == categoryId);
            switch (sort)
            {
                case "new": query.OrderByDescending(x => x.CreatedDate); break;
                case "popular": query.OrderByDescending(x => x.ViewCount); break;
                case "discount": query.OrderByDescending(x => x.PromotionPrice.HasValue); break;
                case "price": query.OrderBy(x => x.Price); break;

                default:
                    break;
            };
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
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

        public List<string> GetListProductByName(string keyword)
        {
            return this._productRepository.GetMulti(x => x.Status && x.Name.Contains(keyword)).Select(x => x.Name).ToList<string>();
        }

        public IEnumerable<Product> GetListProductByName(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            var query = this._productRepository.GetMulti(x => x.Status && x.Name.Contains(keyword));
            switch (sort)
            {
                case "new": query.OrderByDescending(x => x.CreatedDate); break;
                case "popular": query.OrderByDescending(x => x.ViewCount); break;
                case "discount": query.OrderByDescending(x => x.PromotionPrice.HasValue); break;
                case "price": query.OrderBy(x => x.Price); break;

                default:
                    break;
            };
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Product> GetReatedProducts(int id, int top)
        {
            var produc = _productRepository.GetSingleById(id);
            
            return _productRepository.GetMulti(x => x.Status&& x.ID != id  && x.CategoryID == produc.CategoryID).OrderByDescending(x => x.CreatedDate).Take(top);
        }
    }
}