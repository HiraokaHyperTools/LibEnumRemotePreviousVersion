# LibEnumRemotePreviousVersion

Previous Version = Snapshots

See also:

- [2.2.7.2.2.1 FSCTL_SRV_ENUMERATE_SNAPSHOTS Response](https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-smb/5a43eb29-50c8-46b6-8319-e793a11f6226)
- `volrest` utility in Windows Server 2003 Resource Kit Tools also seems to be useful. See [https://superuser.com/a/644759](https://superuser.com/a/644759)

Usage:

```cs
using LibEnumRemotePreviousVersion;
using System;

namespace TestApp
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.Error.WriteLine(@"TestApp \\server\share");
                return 1;
            }
            Console.WriteLine(
                string.Join(
                    Environment.NewLine,
                    PreviousVersionOnRemote.Enum(args[0])
                )
            );
            return 0;
        }
    }
}
```

## Print snapshots

```bat

D:\Proj\LibEnumRemotePreviousVersion>TestApp\bin\Debug\TestApp.exe U:\
@GMT-2019.09.03-03.00.37
@GMT-2019.09.02-22.00.11
@GMT-2019.09.02-03.00.57
@GMT-2019.09.01-22.00.13
@GMT-2019.08.30-03.00.33
@GMT-2019.08.29-22.00.13
@GMT-2019.08.29-03.00.33
@GMT-2019.08.28-22.00.11
@GMT-2019.08.28-03.00.36
@GMT-2019.08.27-22.00.10
@GMT-2019.08.27-03.00.33
@GMT-2019.08.26-22.00.11
@GMT-2019.08.26-03.00.31
@GMT-2019.08.25-22.00.07
@GMT-2019.08.23-03.00.34
@GMT-2019.08.22-22.00.11
@GMT-2019.08.22-03.00.32
@GMT-2019.08.21-22.00.09
@GMT-2019.08.21-03.00.31
@GMT-2019.08.20-22.00.18
@GMT-2019.08.20-03.00.34
@GMT-2019.08.19-22.00.18
@GMT-2019.08.19-03.00.34
@GMT-2019.08.18-22.00.18
@GMT-2019.08.16-03.00.39
@GMT-2019.08.15-22.00.16
@GMT-2019.08.15-03.01.06
@GMT-2019.08.14-22.00.24
@GMT-2019.08.14-03.00.56
@GMT-2019.08.13-22.00.10
@GMT-2019.08.13-03.00.33
@GMT-2019.08.12-22.00.45
@GMT-2019.08.12-03.00.56
@GMT-2019.08.11-22.00.10
@GMT-2019.08.09-03.00.40
@GMT-2019.08.08-22.00.43
@GMT-2019.08.08-03.01.05
@GMT-2019.08.07-22.00.09
@GMT-2019.08.07-03.00.35
@GMT-2019.08.06-22.00.43
@GMT-2019.08.06-03.00.53
@GMT-2019.08.05-22.00.13
@GMT-2019.08.05-03.00.32
@GMT-2019.08.04-22.00.11
@GMT-2019.08.02-03.00.57
@GMT-2019.08.01-22.00.19
@GMT-2019.08.01-03.00.31
@GMT-2019.07.31-22.00.14
@GMT-2019.07.31-03.00.55
@GMT-2019.07.30-22.00.12
@GMT-2019.07.30-03.00.32
@GMT-2019.07.29-22.00.47
@GMT-2019.07.29-03.00.54
@GMT-2019.07.28-22.00.08
@GMT-2019.07.26-03.00.32
@GMT-2019.07.25-22.00.43
@GMT-2019.07.25-03.00.54
@GMT-2019.07.24-22.00.09
@GMT-2019.07.24-03.00.32
@GMT-2019.07.23-22.00.42
@GMT-2019.07.23-03.00.55
@GMT-2019.07.22-22.00.08
@GMT-2019.07.22-03.00.31
@GMT-2019.07.21-22.00.42
```

## Access: snapshot, and then folder

```bat
D:\Proj\LibEnumRemotePreviousVersion>DIR U:\@GMT-2019.09.03-03.00.37\OWL-PCEXU3E2I2
 ドライブ U のボリューム ラベルは New ファイルサーバー です
 ボリューム シリアル番号は 7250-6805 です

 U:\@GMT-2019.09.03-03.00.37\OWL-PCEXU3E2I2 のディレクトリ

2019/08/09  12:52    <DIR>          .
2019/08/09  12:52    <DIR>          ..
2015/06/29  13:01               321 DriverInfo.txt
2015/07/03  16:13         1,949,142 OWL-PCEXU3E212取扱説明書.pdf
2019/08/09  12:52    <DIR>          Windows
               2 個のファイル           1,949,463 バイト
               3 個のディレクトリ  3,257,954,856,960 バイトの空き領域
```

## Access: folder, and then snapshot

```bat
D:\Proj\LibEnumRemotePreviousVersion>DIR U:\OWL-PCEXU3E2I2\@GMT-2019.09.03-03.00.37
 ドライブ U のボリューム ラベルは New ファイルサーバー です
 ボリューム シリアル番号は 7250-6805 です

 U:\OWL-PCEXU3E2I2\@GMT-2019.09.03-03.00.37 のディレクトリ

2019/08/09  12:52    <DIR>          .
2019/08/09  12:52    <DIR>          ..
2015/06/29  13:01               321 DriverInfo.txt
2015/07/03  16:13         1,949,142 OWL-PCEXU3E212取扱説明書.pdf
2019/08/09  12:52    <DIR>          Windows
               2 個のファイル           1,949,463 バイト
               3 個のディレクトリ  3,257,954,856,960 バイトの空き領域
```
