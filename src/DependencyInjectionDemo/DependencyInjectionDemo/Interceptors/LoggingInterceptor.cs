using Castle.DynamicProxy;
using System;
using System.ComponentModel.Composition;

namespace DependencyInjectionDemo.Interceptors
{
    [Export(typeof(LoggingInterceptor))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LoggingInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var method = invocation.GetConcreteMethod();
            var targetType = GetNestedType(invocation.Proxy) ?? GetNestedType(invocation.InvocationTarget);

            System.Diagnostics.Debug.WriteLine("Before {0}.{1}", targetType.Name, method.Name);

            invocation.Proceed();

            System.Diagnostics.Debug.WriteLine("After {0}.{1}", targetType.Name, method.Name);
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
