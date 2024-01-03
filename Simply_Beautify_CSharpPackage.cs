global using System;
global using Microsoft.VisualStudio.Shell;
global using Community.VisualStudio.Toolkit;
global using Task = System.Threading.Tasks.Task;
using System.Threading;
using System.Runtime.InteropServices;


namespace Simply_Beautify_CSharp
{
	[PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
	[InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
	[ProvideMenuResource("Menus.ctmenu", 1)]
	[Guid(PackageGuids.Simply_Beautify_CSharpString)]
	public sealed class Simply_Beautify_CSharpPackage : ToolkitPackage
	{
		protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
		{
			await this.RegisterCommandsAsync();
		}
	}
}