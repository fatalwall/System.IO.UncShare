# vshed.IO.UncShare
Library for accessing UNC Shares with an alternative identity

## Example Usage
            
    try
    {
        using (var share = new UncShare(@"\\hostName\networkShareName", "userAccount", "password"))
        {
            var dir = new DirectoryInfo(share.Path);
            Assert.IsTrue(dir.GetFiles().Length >= 1);
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
