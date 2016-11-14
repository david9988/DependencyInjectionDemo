using System;

namespace DependencyInjectionDemo.Interceptors
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class InterceptAttribute : Attribute
    {
        public InterceptAttribute(Type interceptorType, int order)
        {
            InterceptorType = interceptorType;
            Order = order;
        }

        public Type InterceptorType { get; private set; }
        public int Order { get; private set; }

        /*public Type TargetInterface { get; set; }*/
    }
}
