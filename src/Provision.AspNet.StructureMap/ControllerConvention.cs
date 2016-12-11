using System.Web.Http;
using System.Web.Mvc;
using StructureMap;
using StructureMap.Graph;
using StructureMap.Graph.Scanning;
using StructureMap.Pipeline;
using StructureMap.TypeRules;

namespace Provision.AspNet.StructureMap
{
	public class ControllerConvention : IRegistrationConvention
	{
		public void ScanTypes(TypeSet types, Registry registry)
		{
			foreach (var type in types.FindTypes(TypeClassification.Concretes)) {
				if (type.CanBeCastTo<Controller>() || type.CanBeCastTo<ApiController>() /* || type.CanBeCastTo<ODataController>() */) {
					registry.For(type).LifecycleIs(new UniquePerRequestLifecycle());
				}
			}
		}
	}
}
