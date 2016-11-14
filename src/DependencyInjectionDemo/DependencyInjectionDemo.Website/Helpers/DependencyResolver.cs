using DependencyInjectionDemo.DryIoc;
using DryIoc;
using DryIoc.MefAttributedModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace DependencyInjectionDemo.Website.Helpers
{
    public class DependencyResolver: IDependencyResolver
    {
        #region Fields

        private IContainer _container;

        #endregion

        #region Static members

        private static IDependencyResolver _current;

        public static IDependencyResolver Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new DependencyResolver();
                }

                return _current;
            }

            set
            {
                _current = value;
            }
        }

        #endregion

        #region Constructor

        public DependencyResolver()
        {
            InitContainer();
        }

        #endregion

        #region IDependencyResolver methods

        public object GetService(Type serviceType)
        {
            if (_container.IsRegistered(serviceType))
            {
                return _container.Resolve(serviceType);
            }

            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.ResolveMany(serviceType);
        }

        #endregion

        #region Private methods

        private void InitContainer()
        {
            LoadAssemblies();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            _container = new Container(rules => rules.WithTrackingDisposableTransients()).WithMefAttributedModel();
            _container.RegisterExportsWithInterception(assemblies);
        }

        private void LoadAssemblies()
        {
            var path = HttpRuntime.BinDirectory;
            var pattern = "DependencyInjectionDemo*.dll";
            var files = new DirectoryInfo(path).GetFiles(pattern);
            var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var file in files)
            {
                var assemblyName = AssemblyName.GetAssemblyName(file.FullName);
                if (!currentAssemblies.Any(assembly => AssemblyName.ReferenceMatchesDefinition(assemblyName, assembly.GetName())))
                {
                    Assembly.Load(assemblyName);
                }
            }
        }

        #endregion

    }
}