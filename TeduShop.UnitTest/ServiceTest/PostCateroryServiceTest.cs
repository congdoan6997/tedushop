using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;
using TeduShop.Service;

namespace TeduShop.UnitTest.ServiceTest
{
    [TestClass]
   public  class PostCateroryServiceTest
    {
        private Mock<IPostCategoryRepository> _mockPostCategoryRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IPostCategoryService _postCategoryService;
        private List<PostCategory> listCategory;

        [TestInitialize]
        public void Initializa()
        {
            this._mockPostCategoryRepository = new Mock<IPostCategoryRepository>();
            this._mockUnitOfWork = new Mock<IUnitOfWork>();
            this._postCategoryService = new PostCategoryService(_mockPostCategoryRepository.Object, _mockUnitOfWork.Object);
            listCategory = new List<PostCategory>()
            {
                new PostCategory(){ID=1,Name="DM1",Status= true},
                new PostCategory(){ID=2,Name="DM2",Status= true},
                new PostCategory(){ID=3,Name="DM3",Status= true}

            };
        }
        [TestMethod]
        public void PostCategory_Service_GetAll()
        {
            _mockPostCategoryRepository.Setup(m => m.GetAll(null)).Returns(listCategory);

            var result = this._postCategoryService.GetAll() as List<PostCategory>;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void PostCategory_Service_Create()
        {
            PostCategory postCategory = new PostCategory()
            {
                Name = "test",
                Alias = "test",
                Status = true
            };
            _mockPostCategoryRepository.Setup(m => m.Add(postCategory)).Returns((PostCategory p) =>
            {
                p.ID = 1;
                return p;
            });

            var result = this._postCategoryService.Add(postCategory);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }
    }
}
