using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web.Http.Dependencies;
using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace Provision.AspNet.StructureMap
{
	public class StructureMapScope : IDependencyScope, IServiceLocator
	{
		private readonly IContainer _container;
		private Boolean _disposed;

		public StructureMapScope(IContainer container)
		{
			_container = container;
		}

		public IDependencyResolver GetResolver()
		{
			return new StructureMapResolver(_container);
		}

		public Object GetService(Type serviceType)
		{
			return GetServiceInternal(serviceType);
		}

		public IEnumerable<Object> GetServices(Type serviceType)
		{
			Debug.WriteLine($"Requested all instances of: {serviceType}");
			return _container.GetAllInstances(serviceType).Cast<Object>();
		}

		public Object GetInstance(Type serviceType)
		{
			return GetServiceInternal(serviceType);
		}

		public Object GetInstance(Type serviceType, String key)
		{
			return GetServiceInternal(serviceType, key);
		}

		public TService GetInstance<TService>()
		{
			return (TService)GetServiceInternal(typeof(TService));
		}

		public TService GetInstance<TService>(String key)
		{
			return (TService)GetServiceInternal(typeof(TService), key);
		}

		public IEnumerable<Object> GetAllInstances(Type serviceType)
		{
			Debug.WriteLine($"Requested all instances of: {serviceType}");
			return GetServices(serviceType);
		}

		public IEnumerable<TService> GetAllInstances<TService>()
		{
			Debug.WriteLine($"Requested all instances of: {typeof(TService)}");
			return _container.GetAllInstances<TService>();
		}

		public void Dispose()
		{
			Dispose(true);
		}

		protected StructureMapScope GetChildScope()
		{
			Debug.WriteLine($"Child scope created;");
			return new StructureMapScope(_container.CreateChildContainer());
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed) {
				if (disposing) {
					// Dispose managed resources
					_container.Dispose();
				}

				// Free unmanaged resources
				// Set large fields to null.

				_disposed = true;
			}
		}

		private Object GetServiceInternal(Type serviceType, String key = null)
		{
			if (String.IsNullOrWhiteSpace(key)) {
				var a = _container.TryGetInstance(serviceType);
				Debug.WriteLine($"Requested: {serviceType}  Provided: {a}");
				return a;
			}

			var b = _container.TryGetInstance(serviceType, key);
			Debug.WriteLine($"Requested: {serviceType}  With Key: {key}  Provided: {b}");
			return b;
		}
	}
}
