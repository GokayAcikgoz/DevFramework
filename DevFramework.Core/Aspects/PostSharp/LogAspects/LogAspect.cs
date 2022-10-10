using DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net;
using DevFramework.Core.CrossCuttingConcerns.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using PostSharp.Aspects;
using PostSharp.Extensibility;

namespace DevFramework.Core.Aspects.PostSharp.LogAspects
{
    [Serializable]
    [MulticastAttributeUsage(MulticastTargets.Method, TargetMemberAttributes = MulticastAttributes.Instance)] //classların en üstüne koyulursa constructora uygulamasın diye
    public class LogAspect : OnMethodBoundaryAspect
    {
        private Type _loggerType;
        private LoggerService _loggerService;

        public LogAspect(Type loggerType)
        {
            _loggerType = loggerType;
        }

        //Instance üretiyoruz. ConsoleLogger, FileLogger gibi bir sürü logger gelebilir o yüzden LoggerService oluşturup Instanceını üretiyoruz.
        public override void RuntimeInitialize(MethodBase method)
        {
            if (_loggerType.BaseType != typeof(LoggerService))
            {
                throw new Exception("Wrong logger type");
            }
            _loggerService = (LoggerService)Activator.CreateInstance(_loggerType);

            base.RuntimeInitialize(method);
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            
            if (!_loggerService.IsInfoEnabled)
            {
                return;
            }

            try
            {
                var logParameters = args.Method.GetParameters().Select((t, i) => new LogParameter
                {
                    Name = t.Name,
                    Type = t.ParameterType.Name,
                    Value = args.Arguments.GetArgument(i)
                }).ToList();

                var logDetail = new LogDetail
                {
                    FullName = args.Method.DeclaringType == null ? null : args.Method.DeclaringType.Name,
                    MethodName = args.Method.Name,
                    Parameters = logParameters
                };

                _loggerService.Info(logDetail);
            }
            catch (Exception)
            {


            }


        }
    }
}
