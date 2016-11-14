using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionDemo.ConsoleApp
{
    class Program
    {
        private static DependencyResolver _dependencyResolver;

        static void Main(string[] args)
        {
            Init();

            var app = _dependencyResolver.Resolve<IApp>();

            app.Run();
        }

        private static void Init()
        {
            _dependencyResolver = new DependencyResolver();
        }
    }
}
