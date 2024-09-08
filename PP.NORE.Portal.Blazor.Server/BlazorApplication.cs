using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor;
using DevExpress.ExpressApp.Blazor.Templates;
using DevExpress.ExpressApp.MultiTenancy;

namespace PP.NORE.Portal.Blazor.Server;

public class PortalBlazorApplication : BlazorApplication {
    public PortalBlazorApplication() {
        ApplicationName = "NORE";
        CheckCompatibilityType = CheckCompatibilityType.DatabaseSchema;
        DatabaseVersionMismatch += PortalBlazorApplication_DatabaseVersionMismatch;
		CustomizeTemplate += PortalBlazorApplication_CustomizeTemplate;

	}

	private void PortalBlazorApplication_CustomizeTemplate(object sender, CustomizeTemplateEventArgs e)
	{
		if (e.Template is IPopupWindowTemplateSize size)
		{
			size.MaxWidth = "80vw";
			size.Width = "800vw";
			size.MaxHeight = "80vh";
			size.Height = "80vw";
		}
	}
	protected override void OnSetupStarted() {
        base.OnSetupStarted();
#if DEBUG
        if(System.Diagnostics.Debugger.IsAttached && CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema) {
            DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
        }
#endif
    }
    private void PortalBlazorApplication_DatabaseVersionMismatch(object sender, DatabaseVersionMismatchEventArgs e) {
#if EASYTEST
        e.Updater.Update();
        e.Handled = true;
#else
        if(System.Diagnostics.Debugger.IsAttached || TenantId != null) {
            e.Updater.Update();
            e.Handled = true;
        }
        else {
            string message = "The application cannot connect to the specified database, " +
                "because the database doesn't exist, its version is older " +
                "than that of the application or its schema does not match " +
                "the ORM data model structure. To avoid this error, use one " +
                "of the solutions from the https://www.devexpress.com/kb=T367835 KB Article.";

            if(e.CompatibilityError != null && e.CompatibilityError.Exception != null) {
                message += "\r\n\r\nInner exception: " + e.CompatibilityError.Exception.Message;
            }
            throw new InvalidOperationException(message);
        }
#endif
    }
    Guid? TenantId {
        get {
            return ServiceProvider?.GetService<ITenantProvider>()?.TenantId;
        }
    }
}
