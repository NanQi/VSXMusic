// Guids.cs
// MUST match guids.h
using System;

namespace VSXMusic
{
    static class GuidList
    {
        public const string guidVSXMusicPkgString = "1449b295-ff93-4c18-8071-f5bb6c811f16";
        public const string guidVSXMusicCmdSetString = "ade76f79-3be2-4fe8-a733-b1a2fe12fb7f";

        public static readonly Guid guidVSXMusicCmdSet = new Guid(guidVSXMusicCmdSetString);
    };
}