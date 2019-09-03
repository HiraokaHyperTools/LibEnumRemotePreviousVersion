using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace LibEnumRemotePreviousVersion
{
    public static class PreviousVersionOnRemote
    {
        const FileAttributes FILE_FLAG_BACKUP_SEMANTICS = (FileAttributes)0x02000000;
        const int FSCTL_SRV_ENUMERATE_SNAPSHOTS = 0x00144064;

        public static string[] Enum(string uncShareRoot)
        {
            IntPtr dirHandle = CreateFileW(
                uncShareRoot,
                FileAccess.Read,
                FileShare.ReadWrite | FileShare.Delete,
                IntPtr.Zero,
                FileMode.Open,
                FILE_FLAG_BACKUP_SEMANTICS,
                IntPtr.Zero
            );
            if (dirHandle.ToInt64() == -1)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            try
            {
                var ioStatus = new IO_STATUS_BLOCK();
                var buff = new byte[16];
                int status = NtFsControlFile(
                    dirHandle,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    ref ioStatus,
                    FSCTL_SRV_ENUMERATE_SNAPSHOTS,
                    IntPtr.Zero,
                    0,
                    buff,
                    buff.Length
                );
                if (status != 0)
                {
                    throw new Win32Exception(status);
                }
                var numVolumes = BitConverter.ToInt32(buff, 0);
                var numVolumesReturned = BitConverter.ToInt32(buff, 4);
                var numBodyBytes = BitConverter.ToInt32(buff, 8);

                buff = new byte[12 + numBodyBytes];
                status = NtFsControlFile(
                    dirHandle,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    ref ioStatus,
                    FSCTL_SRV_ENUMERATE_SNAPSHOTS,
                    IntPtr.Zero,
                    0,
                    buff,
                    buff.Length
                );
                if (status != 0)
                {
                    throw new Win32Exception(status);
                }

                return Encoding.Unicode.GetString(
                    buff,
                    12,
                    buff.Length - 12
                )
                    .Split(
                        new char[] { '\0' },
                        StringSplitOptions.RemoveEmptyEntries
                    );
            }
            finally
            {
                CloseHandle(dirHandle);
            }
        }

        [DllImport("ntdll.dll")]
        private static extern int NtFsControlFile(
          IntPtr FileHandle,
          IntPtr Event,
          IntPtr ApcRoutine,
          IntPtr ApcContext,
          ref IO_STATUS_BLOCK IoStatusBlock,
          int FsControlCode,
          IntPtr InputBuffer,
          int InputBufferLength,
          byte[] OutputBuffer,
          int OutputBufferLength
        );

        private struct IO_STATUS_BLOCK
        {
            IntPtr StatusOrPointer;
            IntPtr Information;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr CreateFileW(
            [MarshalAs(UnmanagedType.LPWStr)] string filename,
            [MarshalAs(UnmanagedType.U4)] FileAccess access,
            [MarshalAs(UnmanagedType.U4)] FileShare share,
            IntPtr securityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            [MarshalAs(UnmanagedType.U4)] FileAttributes flagsAndAttributes,
            IntPtr templateFile
        );
    }
}
