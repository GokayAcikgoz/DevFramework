using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using DevFramework.Core.CrossCuttingConcerns.Validation.FluentValidation;
using DevFramework.Northwind.Business.Abstract;
using DevFramework.Northwind.Business.ValidationRules.FluentValidation;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.Concrete;
using DevFramework.Core.Aspects.PostSharp;
using DevFramework.Core.Aspects.PostSharp.AuthorizationAspects;
using DevFramework.Core.Aspects.PostSharp.CacheAspects;
using DevFramework.Core.Aspects.PostSharp.LogAspects;
using DevFramework.Core.Aspects.PostSharp.PerformanceAspects;
using DevFramework.Core.Aspects.PostSharp.TransactionAspects;
using DevFramework.Core.Aspects.PostSharp.ValidationAspects;
using DevFramework.Core.CrossCuttingConcerns.Caching.Microsoft;
using DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using DevFramework.Core.DataAccess;
using DevFramework.Core.Utilities.Mappings;
using PostSharp.Aspects.Dependencies;



namespace DevFramework.Northwind.Business.Concrete.Managers
{
    //[LogAspect(typeof(DatabaseLogger))]
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
        private readonly IMapper _mapper;

        public ProductManager(IProductDal productDal, IMapper mapper)
        {
            _productDal = productDal;
            _mapper = mapper;
        }


        [FluentValidationAspect(typeof(ProductValidatior))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        //[LogAspect(typeof(DatabaseLogger))]
        public Product Add(Product product)
        {
            //aspect oriented programming kullanacağımız için alttaki şekilde kullanmıyoruz üstekini yazıyoruz. AOP
            //ValidatorTool.FluentValidate((IValidator)new ProductValidator(), product);
            return _productDal.Add(product);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        //[PerformanceCounterAspect(2)]
        //[SecuredOperation(Roles = "Admin")]
        public List<Product> GetAll()
        {
            //Thread.Sleep(3000);
            //return _productDal.GetList().Select(p => new Product
            //{
            //    CategoryId = p.CategoryId,
            //    ProductId = p.ProductId,
            //    ProductName = p.ProductName,
            //    QuantityPerUnit = p.QuantityPerUnit,
            //    UnitPrice = p.UnitPrice
            //}).ToList();

            var products = _mapper.Map<List<Product>>(_productDal.GetList());
            return products;
        }

        

        public Product GetById(int id)
        {
            return _productDal.Get(p => p.ProductId == id);
        }

        [FluentValidationAspect(typeof(ProductValidatior))]
        [TransactionScopeAspect]
        public void TransactionalOperation(Product product1, Product product2)
        {
            _productDal.Add(product1);
            //Business Codes
            _productDal.Update(product2);
        }

        [FluentValidationAspect(typeof(ProductValidatior))]
        public Product Update(Product product)
        {
            //ValidatorTool.FluentValidate((IValidator)new ProductValidator(), product);
            return _productDal.Update(product);
        }
    }
}
