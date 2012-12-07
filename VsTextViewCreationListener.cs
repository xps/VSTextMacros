using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using System.Diagnostics;

namespace VSTextMacros
{
    // This listener is loaded by MEF and adds a MacroCommandFilter to every new IVsTextView
    [Export(typeof(IVsTextViewCreationListener)), TextViewRole(PredefinedTextViewRoles.Editable), ContentType("text")]
    public class VsTextViewCreationListener : IVsTextViewCreationListener
    {
        [Import]
        internal IVsEditorAdaptersFactoryService AdaptersFactory = null;

        [Export]
        [Name("CommentAdornmentLayer")]
        [Order(After = PredefinedAdornmentLayers.Text)]
        public AdornmentLayerDefinition commentLayerDefinition;

        public void VsTextViewCreated(IVsTextView textView)
        {
            var wpfTextView = AdaptersFactory.GetWpfTextView(textView);
            if (wpfTextView == null)
            {
                Debug.Fail("Unable to get IWpfTextView from IVsTextView");
                return;
            }

            var adornmentManager = MacroAdornmentManager.Create(wpfTextView);
            var filter = new MacroCommandFilter(adornmentManager);

            IOleCommandTarget next;
            if (ErrorHandler.Succeeded(textView.AddCommandFilter(filter, out next)))
                filter.Next = next;
        }
    }
}
