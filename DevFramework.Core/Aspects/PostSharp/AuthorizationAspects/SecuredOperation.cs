using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.Aspects.PostSharp.AuthorizationAspects
{
    [Serializable]
    public class SecuredOperation : OnMethodBoundaryAspect
    {
        public string Roles { get; set; }

        public override void OnEntry(MethodExecutionArgs args)
        {
            //admin, kullanici1, kullanici2 gibi virgülle ayıracağımız için bu şekilde split yaptık.
            string[] roles = Roles.Split(',');
            bool isAuthorized = false;

            //bütün rolleri geziyoruz.
            for (int i = 0; i < roles.Length; i++)
            {
                //mevcur kullanıcı rollerinde admin var mı ? varsa true
                if (System.Threading.Thread.CurrentPrincipal.IsInRole(roles[i]))
                {
                    isAuthorized = true;
                }
            }

            //yoksa false
            if (isAuthorized == false)
            {
                throw new SecurityException("You are not authorized!");
            }

            
        }
    }
}
