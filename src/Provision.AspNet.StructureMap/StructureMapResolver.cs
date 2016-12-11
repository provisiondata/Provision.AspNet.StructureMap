using System.Web.Http.Dependencies;
using StructureMap;

namespace Provision.AspNet.StructureMap
{
	public class StructureMapResolver : StructureMapScope, IDependencyResolver
	{
		public StructureMapResolver(IContainer container) : base(container)
		{
		}

		public IDependencyScope BeginScope()
		{
			return GetChildScope();
		}
	}
}
