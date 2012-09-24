using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace XPS.VSTextMacros
{
    // This listener is loaded by MEF and adds a MacroCommandFilter to every new IVsTextView

    [Export(typeof(IVsTextViewCreationListener)), TextViewRole(PredefinedTextViewRoles.Editable), ContentType("text")]
    public class VsTextViewCreationListener : IVsTextViewCreationListener
    {
        [Import]
        internal IVsEditorAdaptersFactoryService AdapterService = null;

        public void VsTextViewCreated(IVsTextView textViewAdapter)
        {
            var filter = new MacroCommandFilter();

            IOleCommandTarget next;
            if (ErrorHandler.Succeeded(textViewAdapter.AddCommandFilter(filter, out next)))
                filter.Next = next;
        }
    }
}
