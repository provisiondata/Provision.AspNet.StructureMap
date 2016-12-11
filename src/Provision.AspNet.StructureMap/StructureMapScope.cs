using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace Provision.AspNet.StructureMap
{
	public class StructureMapScope : IDependencyScope, IServiceLocator
	{
		private Boolean _disposed;
		private readonly IContainer _container;

		public StructureMapScope(IContainer container)
		{
			_container = container;
		}

		protected StructureMapScope GetChildScope()
		{
			return new StructureMapScope(_container.CreateChildContainer());
		}

		public IDependencyResolver GetResolver()
		{
			return new StructureMapResolver(_container);
		}

		public Object GetService(Type serviceType)
		{
			return _container.TryGetInstance(serviceType);
		}

		public IEnumerable<Object> GetServices(Type serviceType)
		{
			return _container.GetAllInstances(serviceType).Cast<Object>();
		}

		public Object GetInstance(Type serviceType)
		{
			return GetService(serviceType);
		}

		public Object GetInstance(Type serviceType, String key)
		{
			return _container.TryGetInstance(serviceType, key);
		}

		public TService GetInstance<TService>()
		{
			return _container.TryGetInstance<TService>();
		}

		public TService GetInstance<TService>(String key)
		{
			return _container.TryGetInstance<TService>(key);
		}

		public IEnumerable<Object> GetAllInstances(Type serviceType)
		{
			return GetServices(serviceType);
		}

		public IEnumerable<TService> GetAllInstances<TService>()
		{
			return _container.GetAllInstances<TService>();
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed) {
				if (disposing) {
					// Dispose managed state
					_container.Dispose();
				}

				// Free unmanaged resources
				// Set large fields to null.

				_disposed = true;
			}
		}

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
		}
	}
}
