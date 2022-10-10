using DevFramework.Core.DataAccess;
using DevFramework.Northwind.Entities.ComplexTypes;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        //IEntityRepository Ortak CRUD islemleri orada oldugu icin implemente yaptık. SOLID'e uygun bir sekilde kod yazmak icin
        //IProductDala özel kendine ait bir seyi varsa yazarız.

        List<ProductDetail> GetProductDetails();
    }
}
