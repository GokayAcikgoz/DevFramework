using DevFramework.Core.DataAccess.NHibernate;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace DevFramework.Northwind.DataAccess.Concrete.NHibernate.Helpers
{
    public class SqlServerHelper : NHibernateHelper
    {
        protected override ISessionFactory InitializeFactory()
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                .ConnectionString(@"Data source = (localdb)\MSSQLLocalDB; initial catalog = Northwind; Integrated Security = true"))
                .Mappings(t => t.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly())).BuildSessionFactory();
        }
    }
}
