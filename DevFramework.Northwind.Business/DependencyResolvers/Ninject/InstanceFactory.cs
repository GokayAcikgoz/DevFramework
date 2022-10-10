﻿using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Northwind.Business.DependencyResolvers.Ninject
{
    public class InstanceFactory
    {
        public static T GetInstace<T>()
        {
            var kernel = new StandardKernel(new BusinessModule(), new AutoMapperModule());

            return kernel.Get<T>();
        }
    }
}
