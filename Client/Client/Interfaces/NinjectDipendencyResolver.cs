using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Client.Interfaces;
using Client.Services;

namespace Client.Infrustructure
{
  public class NinjectDependencyResolver : IDependencyResolver
  {
    private IKernel kernel;
    public NinjectDependencyResolver(IKernel kernelParam)
    {
      kernel = kernelParam;
      AddBindings();
    }

    public object GetService(Type serviceType)
    {
      return kernel.TryGet(serviceType);
    }
    public IEnumerable<object> GetServices(Type serviceType)
    {
      return kernel.GetAll(serviceType);
    }
    private void AddBindings()
    {
      kernel.Bind<IRequestManager>().To<ProxyRequestManager>();
      kernel.Bind<IUserService>().To<ClientUserService>();
    }

    public IDependencyScope BeginScope()
    {
      throw new NotImplementedException();
    }

    public void Dispose()
    {
      IDisposable disposable = kernel as IDisposable;
      if (disposable != null)
        disposable.Dispose();
      kernel = null;
    }
  }
}