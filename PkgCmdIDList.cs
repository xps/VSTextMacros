// PkgCmdID.cs
// MUST match PkgCmdID.h
using System;

namespace VSTextMacros
{
    public static class PkgCmdIDList
    {
        public const uint idRecordMacro = 0x2001;
        public const uint idPlaybackMacro = 0x2002;
        public const uint idPlaybackMacroMultipleTimes = 0x2003;
        public const uint idSaveMacro = 0x2004;
        public const uint idOpenSavedMacros = 0x2005;
        public const uint isOpenMacrosDirectory = 0x2006;

        public const uint idRunSavedMacro1 = 0x2101;
        public const uint idRunSavedMacro2 = 0x2102;
        public const uint idRunSavedMacro3 = 0x2103;
        public const uint idRunSavedMacro4 = 0x2104;
        public const uint idRunSavedMacro5 = 0x2105;
    }
}