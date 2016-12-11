using System.Web.Http;
using System.Web.Mvc;
using StructureMap;

namespace Provision.AspNet.StructureMap
{
	public static class IoC
	{
		private static IContainer _container;

		public static IContainer Container => _container;

		/// <summary>
		/// Configures ASP.NET MVC and ASP.NET WebAPI to use StructureMap as the Dependency Resolver.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="config"></param>
		public static void Configure<T>(this HttpConfiguration config)
						where T : Registry, new()
		{
			Configure(config, new T());
		}

		/// <summary>
		/// Configures ASP.NET MVC and ASP.NET WebAPI to use StructureMap as the Dependency Resolver.
		/// </summary>
		/// <param name="config"></param>
		/// <param name="registry"></param>
		public static void Configure(this HttpConfiguration config, Registry registry)
		{
			_container = new Container(registry);
			var scope = new StructureMapScope(_container);

			DependencyResolver.SetResolver(scope);
			config.DependencyResolver = scope.GetResolver();
		}
	}
}
