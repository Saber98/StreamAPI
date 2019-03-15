using System.ComponentModel.Composition;
using System.IO;
using BusinessServices.Interfaces;
using Resolver;
using SecurityServices;

namespace BusinessServices.Utilities
{
    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IAddressTypeServices, AddressTypeServices>();
            registerComponent.RegisterType<IAddressServices, AddressServices>();
            registerComponent.RegisterType<IPhoneServices, PhoneServices>();
            registerComponent.RegisterType<IPhoneTypeServices, PhoneTypeServices>();
            registerComponent.RegisterType<IStateServices, StateServices>();
            registerComponent.RegisterType<IContactServices, ContactServices>();
            registerComponent.RegisterType<ICustomerServices, CustomerServices>();
            registerComponent.RegisterType<IUserServices, UserServices>();
            registerComponent.RegisterType<IUserSecurityServices, UserSecurityServices>();
            registerComponent.RegisterType<IRoleServices, RoleServices>();

            registerComponent.RegisterType<IAuthenticationServices, AuthenticationService>();
        }
    }
}
