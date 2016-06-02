using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web.Http.Dependencies;
using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace DKW.AspNet.StructureMap
{
    public class StructureMapScope : IDependencyScope, IServiceLocator
    {
#if DEBUG
        private static Int32 _instanceCount;
        private static Int32 _disposedCount;
        private Int32 _instanceNumber;
#endif
        private Boolean _disposed;
        private IContainer _container;

        public StructureMapScope(IContainer container)
        {
#if DEBUG
            _instanceNumber = Interlocked.Increment(ref _instanceCount);
            if (_instanceNumber == 1)
            {
                Debug.WriteLine(container.WhatDidIScan());
                Debug.WriteLine(container.WhatDoIHave());
            }
            Debug.WriteLine($"StructureMapScope Instantiated - Instance: {_instanceNumber}  Created: {_instanceCount}  Disposed: {_disposedCount}");
#endif
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
            if (!_disposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _container.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                _disposed = true;
#if DEBUG
                Interlocked.Increment(ref _disposedCount);
                Debug.WriteLine($"StructureMapScope Disposed - Instance: {_instanceNumber}  Created: {_instanceCount}  Disposed: {_disposedCount}");
#endif
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Scope() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
    }
}
