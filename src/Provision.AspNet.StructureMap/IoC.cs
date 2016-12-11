using System.Web.Http;
using System.Web.Mvc;
using StructureMap;

namespace Provision.AspNet.StructureMap
{
	public class IoC
	{
		/// <summary>
		/// Configures ASP.NET MVC and ASP.NET WebAPI to use StructureMap as the Dependency Resolver.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="config"></param>
		public static void Bootstrap<T>(HttpConfiguration config)
				where T : Registry, new()
		{
			Configure(config, new T());
		}

		/// <summary>
		/// Configures ASP.NET MVC and ASP.NET WebAPI to use StructureMap as the Dependency Resolver.
		/// </summary>
		/// <param name="config"></param>
		/// <param name="registry"></param>
		public static void Configure(HttpConfiguration config, Registry registry)
		{
			var container = new Container(registry);
			var scope = new StructureMapScope(container);

			DependencyResolver.SetResolver(scope);
			config.DependencyResolver = scope.GetResolver();
		}
	}
}
