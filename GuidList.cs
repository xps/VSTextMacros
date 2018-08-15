// Guids.cs
// MUST match guids.h
using System;

namespace VSTextMacros
{
    static class GuidList
    {
        public const string guidVSTextMacrosPkgString = "6d613d99-e83f-4532-a1d8-e188e419460c";
        public const string guidMacrosCmdSetString = "15c6b4a6-7afb-4d55-97fe-c8191c3f5c47";
        public const string guidMacrosRunSavedCmdSetString = "b45e7f34-883c-4fb9-a3b1-dd902b9246bb";

        public static readonly Guid guidMacrosCmdSet = new Guid(guidMacrosCmdSetString);
        public static readonly Guid guidMacrosRunSavedCmdSet = new Guid(guidMacrosRunSavedCmdSetString);
    };
}