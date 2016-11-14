using Castle.DynamicProxy;
using System;
using System.ComponentModel.Composition;
using System.Text;

namespace DependencyInjectionDemo.Interceptors
{
    [Export(typeof(ExceptionInterceptor))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ExceptionInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var method = invocation.GetConcreteMethod();
            //var targetType = GetNestedType(invocation.InvocationTarget);
            var targetType = GetNestedType(invocation.Proxy) ?? GetNestedType(invocation.InvocationTarget);


            try
            {
                System.Diagnostics.Debug.WriteLine("Exception interceptor {0}.{1}.{2}", targetType.Assembly.GetName().Name, targetType.Name, method.Name);

                invocation.Proceed();
            }
            catch(Exception ex)
            {
                var args = String.Join(", ", invocation.Arguments);
                var msg = new StringBuilder();

                msg.AppendFormat("Exception occurred in {0}.{1}.{2}", targetType.Assembly.GetName().Name, targetType.Name, method.Name).AppendLine();
                msg.AppendFormat("With the following arguments: {0}", args).AppendLine();
                msg.AppendLine(ex.ToString());

                System.Diagnostics.Debug.WriteLine(msg.ToString());

                throw ex;
            }
        }

        private Type GetNestedType(object accessor)
        {
            IProxyTargetAccessor nested = accessor as IProxyTargetAccessor;

            if (nested == null)
            {
                return accessor.GetType();
            }

            nested = GetNestedAccessor(nested);

            return nested.DynProxyGetTarget().GetType();
        }

        private IProxyTargetAccessor GetNestedAccessor(IProxyTargetAccessor accessor)
        {
            var nested = accessor.DynProxyGetTarget() as IProxyTargetAccessor;

            if (nested != null)
            {
                return GetNestedAccessor(nested);
            }

            return accessor;
        }
    }
}
