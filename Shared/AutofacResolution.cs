using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class AutofacResolution
    {
        private static AutofacResolution _instance;
        private IContainer _container;
        private ContainerBuilder _containerBuilder;

        public static AutofacResolution Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AutofacResolution();

                return _instance;
            }
        }

        public AutofacResolution()
        {
            _containerBuilder = new ContainerBuilder();
        }

        public IContainer Container()
        {
            return _container;
        }

        public ContainerBuilder Builder()
        {
            return _containerBuilder;
        }

        public void Build()
        {
            _container = _containerBuilder.Build();
        }

        public TService Resolve<TService>()
        {
            return _container.Resolve<TService>();
        }

        public TService Resolve<TService>(NamedParameter namedParameter)
        {
            return _container.Resolve<TService>();
        }

        public TService Resolve<TService>(string name)
        {
            return _container.ResolveNamed<TService>(name);
        }

        public void RegisterSingleton<TService>()
        {
            _containerBuilder.RegisterType<TService>().SingleInstance();
        }

        public void RegisterSingleton<TInterface, TImplementation>()
        {
            _containerBuilder.RegisterType<TImplementation>().As<TInterface>().SingleInstance();
        }

        public void RegisterPerLifetimeScope<TService>()
        {
            _containerBuilder.RegisterType<TService>().InstancePerLifetimeScope();
        }

        public void RegisterPerLifetimeScope<TInterface, TImplementation>()
        {
            _containerBuilder.RegisterType<TImplementation>().As<TInterface>().InstancePerLifetimeScope();
        }

        public void RegisterPerDependency<TService>()
        {
            _containerBuilder.RegisterType<TService>().InstancePerDependency();
        }

        public void RegisterPerDependency<TService>(string name)
        {
            _containerBuilder.RegisterType<TService>().Named<TService>(name).InstancePerDependency();
        }

        public void RegisterPerDependency<TInterface, TImplementation>()
        {
            _containerBuilder.RegisterType<TImplementation>().As<TInterface>().InstancePerDependency();
        }

        public void RegisterPerDependency<TInterface, TImplementation>(string name)
        {
            _containerBuilder.RegisterType<TImplementation>().Named<TInterface>(name).InstancePerDependency();
        }

        public void RegisterPerDependency(Type Interface, Type Implementation)
        {
            _containerBuilder.RegisterType(Implementation).As(Interface).InstancePerDependency();
        }
    }
}
