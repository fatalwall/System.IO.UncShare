## Overview
.Net library for accessing UNC Shares with an alternative identity without the need to manage mapping and unmapping. This was created to simplify picking up output files from legacy industrial equipment for data analytics.

[![NuGet](https://img.shields.io/nuget/v/vshed.IO.UncShare.svg)](https://www.nuget.org/packages/vshed.IO.UncShare)
[![GitHub release](https://img.shields.io/github/release/fatalwall/vshed.IO.UncShare.svg?label=GitHub%20release)](https://github.com/fatalwall/vshed.IO.UncShare/releases)

## Example Usage
```csharp            
using vshed.IO.UncShare;

...

try
{
    using (var share = new UncShare(@"\\hostName\networkShareName"
                                    , "userAccount"
                                    , "password"))
    {
        if (share.GetFiles().Length >= 1)
            Console.WriteLine("Files found");
        else
            Console.WriteLine("No files found");
    }
}
catch (System.ComponentModel.Win32Exception ex)
{
    if (ex.NativeErrorCode == 53)
        Console.WriteLine("The specified UNC Path could not be found");
    else if ((ex.NativeErrorCode == 86 | ex.NativeErrorCode == 1326)
        Console.WriteLine("The specified Username or Password is invalid");
    else
        Console.WriteLine(ex.Message);
}
```

## Win32Exception
For a complete list of possible exceptions please reference [Microsoft documentation](https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/18d8fbe8-a967-4f1c-ae50-99ca8e491d2d).
