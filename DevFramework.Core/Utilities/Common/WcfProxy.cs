using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Utilities.Common
{
    public class WcfProxy<T>
    {
        public static T CreateChannel()
        {
            string baseAddress = ConfigurationManager.AppSettings["ServiceAddress"];
            string adress = string.Format(baseAddress, typeof(T).Name.Substring(1));

            var binding = new BasicHttpBinding();
            var channel = new ChannelFactory<T>(binding, adress);

            return channel.CreateChannel();
        }
    }
}
