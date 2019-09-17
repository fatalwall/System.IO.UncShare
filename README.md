Library for accessing UNC Shares with an alternative identity

[![NuGet](https://img.shields.io/nuget/v/vshed.IO.UncShare.svg)](https://www.nuget.org/packages/vshed.IO.UncShare)
[![GitHub release](https://img.shields.io/github/release/fatalwall/vshed.IO.UncShare.svg?label=GitHub%20release)](https://github.com/fatalwall/vshed.IO.UncShare/releases)

## Example Usage
```csharp            
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
    if (ex.Message == "The network path was not found")
        Console.WriteLine("The specified UNC Path could not be found");
    else if (ex.Message == "The user name or password is incorrect")
        Console.WriteLine("The specified Username or Password is invalid");
    else
        Console.WriteLine(ex.Message);
}
```
