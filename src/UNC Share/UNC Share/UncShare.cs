/* 
 *Copyright (C) 2019 Peter Varney - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of the MIT license, 
 *
 * You should have received a copy of the MIT license with
 * this file. If not, visit : https://github.com/fatalwall/System.IO.UncShare
 */
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace vshed.IO
{
    public class UncShare : IDisposable
    {
        public string Path { get; private set; }

        public UncShare(string uncPath, string userName, string password)
        {
            int result = WNetUseConnection(IntPtr.Zero
                                            , new NETRESOURCE { dwType = 0x00000001, lpRemoteName = uncPath }
                                            , password
                                            , userName
                                            , 0
                                            , null
                                            , null
                                            , null);
            if(result != 0) { throw new Win32Exception(result); }
            this.Path = uncPath;
        }

        public void Dispose()
        {
            if (!string.IsNullOrWhiteSpace(this.Path))
            {
                WNetCancelConnection2(this.Path, 0x00000001, false);
                this.Path = null;
            }
        }

        [DllImport("mpr.dll")]
        private static extern int WNetUseConnection(IntPtr hwndOwner, NETRESOURCE lpNetResource, string lpPassword, string lpUserID, int dwFlags, string lpAccessname, string lpBufferSize, string lpResult);

        [DllImport("mpr.dll")]
        private static extern int WNetCancelConnection2(string lpName, int dwFlags, bool fForce);

        [StructLayout(LayoutKind.Sequential)]
        private class NETRESOURCE
        {
            public int dwScope;
            public int dwType;
            public int dwDisplayType;
            public int dwUsage;
            public string lpLocalName;
            public string lpRemoteName;
            public string lpComment;
            public string lpProvider;

        }
    }
}
