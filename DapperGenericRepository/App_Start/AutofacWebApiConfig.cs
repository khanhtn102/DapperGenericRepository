using Autofac;
using Autofac.Integration.WebApi;
using Core.Repository;
using Infrastructure;
using Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace DapperGenericRepository.App_Start
{
	public class AutofacWebApiConfig
	{
		public static void Initialize(HttpConfiguration config)
		{
			Initialize(config, RegisterServices());
		}

		private static void Initialize(HttpConfiguration config, IContainer container)
		{
			config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
		}

		private static IContainer RegisterServices()
		{
			//Register your Web API controllers.
			AutofacResolution.Instance.Builder().RegisterApiControllers(Assembly.GetExecutingAssembly());

			var listAllAssemblies = AppDomain.CurrentDomain.GetAssemblies();
			RegisterInfrastructureLayer(listAllAssemblies.Where(p => p.FullName.Contains("Infrastructure")));

			AutofacResolution.Instance.Build();

			return AutofacResolution.Instance.Container();
		}

		#region Register Infrastructure Layer

		private static bool IsRepositoryInterface(Type type)
		{
			var listParentInterfaceTypes = type.GetInterfaces();
			return listParentInterfaceTypes.Any(x => x == typeof(IGenericRepository) || (x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IGenericRepository<>)));
		}

		private static void RegisterInfrastructureLayer(IEnumerable<Assembly> listAssemblies)
		{
			AutofacResolution.Instance.RegisterPerLifetimeScope<IDbConnection>();
			AutofacResolution.Instance.Builder().RegisterGeneric(typeof(PartsQryGenerator<>)).As(typeof(IPartsQryGenerator<>)).InstancePerDependency();
			AutofacResolution.Instance.Builder().RegisterGeneric(typeof(IDentityInspector<>)).As(typeof(IIDentityInspector<>)).InstancePerDependency();

			var listRepositoryTypes = new List<Type>();
			foreach (var assembly in listAssemblies)
			{
				listRepositoryTypes.AddRange(assembly.GetTypes().Where(x => !x.IsAbstract && x.GetInterfaces().Any(y => IsRepositoryInterface(y))));
			}

			foreach (var implementationType in listRepositoryTypes)
			{
				var interfaceType = implementationType.GetInterfaces().Single(x => IsRepositoryInterface(x));
				AutofacResolution.Instance.RegisterPerDependency(interfaceType, implementationType);
			}
		}

		#endregion Register Infrastructure Layer
	}
}