using AutoMapper;
using Ninject.Modules;
using Project.Service;

namespace Project.MVC.NinjectBindings
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IVehicleModelService>().To<VehicleModelService>().InTransientScope();
            Bind<IVehicleMakeService>().To<VehicleMakeService>().InTransientScope();
        }
    }
}
