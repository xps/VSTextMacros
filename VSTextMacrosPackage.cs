using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using VSTextMacros.Model;

namespace VSTextMacros
{
	[PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
	[InstalledProductRegistration("#110", "#112", "1.16", IconResourceID = 400)]
	[ProvideMenuResource("Menus.ctmenu", 1)]
	[Guid(GuidList.guidVSTextMacrosPkgString)]
	[ProvideAutoLoad(VSConstants.UICONTEXT.NoSolution_string, PackageAutoLoadFlags.BackgroundLoad)]
	public sealed class VSTextMacrosPackage : AsyncPackage
	{
		public static VSTextMacrosPackage Current { get; private set; }

		public string MacroDirectory {
			get { return Path.Combine(this.UserLocalDataPath, "Macros"); }
		}

		public DTE2 DTE { get; private set; }

		public VSTextMacrosPackage()
		{
			Current = this;
		}

		protected override async System.Threading.Tasks.Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
		{
			if (!Directory.Exists(MacroDirectory))
				Directory.CreateDirectory(MacroDirectory);

			if (File.Exists(Path.Combine(MacroDirectory, "Current.xml")))
				Macro.CurrentMacro = Macro.LoadFromFile(Path.Combine(MacroDirectory, "Current.xml"));

			RecordableCommands.AddFromFile(Path.Combine(MacroDirectory, "Custom.xml"));

			DTE = (DTE2)await GetServiceAsync(typeof(DTE));

			await base.InitializeAsync(cancellationToken, progress);
		}
	}
}
