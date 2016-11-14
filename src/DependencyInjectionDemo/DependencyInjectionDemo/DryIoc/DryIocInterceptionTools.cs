using Castle.DynamicProxy;
using DependencyInjectionDemo.Interceptors;
using DryIoc;
using DryIoc.MefAttributedModel;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DependencyInjectionDemo.DryIoc
{
    public static class DryIocInterceptionTools
    {
        public static void RegisterInterfaceInterceptor<TService, TInterceptor>(this IRegistrator registrator)
               where TInterceptor : class, IInterceptor
        {
            var serviceType = typeof(TService);
            if (!serviceType.IsInterface)
                throw new ArgumentException(string.Format("Intercepted service type {0} is not an interface", serviceType));

            // Create proxy type for intercepted interface
            var proxyType = ProxyBuilder.Value.CreateInterfaceProxyTypeWithTargetInterface(
                serviceType, new Type[0], ProxyGenerationOptions.Default);

            // Register proxy as decorator
            registrator.Register(serviceType, proxyType,
                made: Parameters.Of.Type<IInterceptor[]>(typeof(TInterceptor[])),
                setup: Setup.Decorator);
        }

        public static void RegisterInterfaceInterceptor(this IRegistrator registrator, Type serviceType, Type interceptorType, int order, object serviceKey = null)
        {
            //if (!serviceType.IsInterface)
            //    throw new ArgumentException(string.Format("Intercepted service type {0} is not an interface", serviceType));

            Type proxyType;
            Type[] targetInterfaces = new Type[0];
            /*if (targetInterface== null)
            {
                targetInterfaces = new Type[0];
            }
            else
            {
                targetInterfaces = new Type[] { targetInterface };
            }*/
            if (serviceType.IsInterface)
            {
                proxyType = ProxyBuilder.Value.CreateInterfaceProxyTypeWithTargetInterface(serviceType, targetInterfaces, ProxyGenerationOptions.Default);
                //proxyType = ProxyBuilder.Value.CreateInterfaceProxyTypeWithTarget(serviceType, targetInterfaces, ProxyGenerationOptions.Default);
                //proxyType = ProxyBuilder.Value.CreateClassProxyType(serviceType, targetInterfaces, ProxyGenerationOptions.Default);
                //proxyType = ProxyBuilder.Value.CreateInterfaceProxyTypeWithTarget(serviceType, targetInterfaces, targetType, ProxyGenerationOptions.Default);
                //proxyType = ProxyBuilder.Value.CreateInterfaceProxyTypeWithoutTarget(serviceType, targetInterfaces, ProxyGenerationOptions.Default);
            }
            else
            {
                proxyType = ProxyBuilder.Value.CreateClassProxyType(serviceType, targetInterfaces, ProxyGenerationOptions.Default);
                //proxyType = ProxyBuilder.Value.CreateClassProxyTypeWithTarget(serviceType, targetInterfaces, ProxyGenerationOptions.Default);
            }
            var interceptorArrayType = Array.CreateInstance(interceptorType, 0).GetType();

            registrator.Register(serviceType, proxyType, made: Parameters.Of.Type(interceptorArrayType), setup: Setup.DecoratorWith(order: order, condition: SpecialCondition)/*, serviceKey: serviceKey*/);
        }

        private static bool SpecialCondition(RequestInfo info)
        {
            if (info.IsResolutionRoot)
            {
                return true;
            }

            return true;
        }

        public static void RegisterExportsWithInterception(this IRegistrator registrator, IEnumerable<Assembly> assemblies)
        {
            registrator.RegisterExportsWithInterception(AttributedModel.Scan(assemblies));
        }

        public static void RegisterExportsWithInterception(this IRegistrator registrator, IEnumerable<ExportedRegistrationInfo> infos)
        {
            foreach (var info in infos)
            {
                var serviceType = info.Exports[0].ServiceType;
                var serviceKey = info.Exports[0].ServiceKeyInfo.Key;
                registrator.RegisterInfo(info);

                var attributes = info.ImplementationType.GetCustomAttributes(typeof(InterceptAttribute), true);

                foreach (var attribute in attributes)
                {
                    var interceptAttribute = (InterceptAttribute)attribute;
                    registrator.RegisterInterfaceInterceptor(serviceType, interceptAttribute.InterceptorType, interceptAttribute.Order, serviceKey);
                }
            }
        }

        public static ParameterSelector Type(this ParameterSelector source, Type requiredServiceType = null, object serviceKey = null, IfUnresolved ifUnresolved = IfUnresolved.Throw, object defaultValue = null)
        {
            return source.Details((r, p) => !requiredServiceType.IsAssignableTo(p.ParameterType) ? null
                : ServiceDetails.Of(requiredServiceType, serviceKey, ifUnresolved, defaultValue));
        }

        private static readonly Lazy<DefaultProxyBuilder> ProxyBuilder = new Lazy<DefaultProxyBuilder>(() => new DefaultProxyBuilder());
    }
}
