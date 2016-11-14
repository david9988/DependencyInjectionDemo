using DryIoc;
using DryIoc.MefAttributedModel;
using System;
using System.IO;
using System.Reflection;
using System.Linq;
using DependencyInjectionDemo.DryIoc;

namespace DependencyInjectionDemo.ConsoleApp
{
    public class DependencyResolver
    {
        private IContainer _container;

        public DependencyResolver()
        {
            InitContainer();
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        private void InitContainer()
        {
            LoadAllAssemblies();

            _container = new Container(rules => rules.WithTrackingDisposableTransients()).WithMefAttributedModel();

            _container.RegisterExportsWithInterception(AppDomain.CurrentDomain.GetAssemblies());
            //_container.RegisterExports(AppDomain.CurrentDomain.GetAssemblies());
        }

        private void LoadAllAssemblies()
        {
            var files = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).GetFiles("DependencyInjectionDemo*.dll");

            foreach (var file in files)
            {
                var assemblyName = AssemblyName.GetAssemblyName(file.FullName);
                if (!AppDomain.CurrentDomain.GetAssemblies().Any(assembly => AssemblyName.ReferenceMatchesDefinition(assemblyName, assembly.GetName())))
                {
                    Assembly.Load(assemblyName);
                }
            }
        }
    }
}
