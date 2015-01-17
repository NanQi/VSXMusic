// Guids.cs
// MUST match guids.h
using System;

namespace Ancientomb.VS2013
{
    static class GuidList
    {
        public const string guidVS2013PkgString = "3a5a6347-784b-42bb-a0a2-5554eee04673";
        public const string guidVS2013CmdSetString = "cf05f13c-90c8-4bc9-b566-5ddeb7833763";

        public static readonly Guid guidVS2013CmdSet = new Guid(guidVS2013CmdSetString);
    };
}