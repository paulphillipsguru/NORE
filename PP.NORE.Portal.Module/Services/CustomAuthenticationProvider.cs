using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using PP.NORE.Portal.Module.BusinessObjects;

namespace PP.NORE.Portal.Module;

public class CustomAuthenticationProvider : IAuthenticationProviderV2 {
    private readonly UserManager userManager;

    public CustomAuthenticationProvider(UserManager userManager) {
        this.userManager = userManager;
    }

    public object Authenticate(IObjectSpace objectSpace) {
        /// <summary>
        // When a user successfully logs in with an OAuth provider, you can get their unique user key.
        // The following code finds an ApplicationUser object associated with this key.
        // This code also creates a new ApplicationUser object for this key automatically.
        // For more information, see the following topic: https://docs.devexpress.com/eXpressAppFramework/402197
        // If this behavior meets your requirements, comment out the line below.
        /// </summary>

        var currentPrincipal = userManager.GetCurrentPrincipal();
        if(currentPrincipal?.Identity?.IsAuthenticated ?? false) {
            var user = userManager.FindUserByPrincipal<ApplicationUser>(objectSpace, currentPrincipal);
            if(user != null) {
                return new UserResult<ApplicationUser>(user);
            }

        // The code below creates users for testing purposes only.
#if !RELEASE
            bool autoCreateUser = true;
            if(autoCreateUser) {
                var userResult = userManager.CreateUser<ApplicationUser>(objectSpace, currentPrincipal, user => {
                    user.Roles.Add(objectSpace.FirstOrDefault<PermissionPolicyRole>(role => role.Name == "Default"));
                });
                if(!userResult.Succeeded) {
                    //throw userResult.Error;
                }
                return userResult;
            }
#endif
        }

        return null;
    }
}
