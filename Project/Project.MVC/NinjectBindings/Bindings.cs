using AutoMapper;
using Ninject.Modules;
using Project.Service;

namespace Project.MVC.NinjectBindings
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IVehicleService>().To<VehicleService>().InTransientScope();
        }
    }
}
