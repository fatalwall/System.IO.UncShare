/* 
 *Copyright (C) 2019 Peter Varney - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of the MIT license, 
 *
 * You should have received a copy of the MIT license with
 * this file. If not, visit : https://github.com/fatalwall/vshed.IO.UncShare
 */
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vshed.IO;
using System.IO;

namespace UNC_Share_Test
{
    [TestClass]
    public class UsageTests
    {
        [TestMethod]
        public void Connection_Disconnect()
        {
            try
            {
                using (var share = new UncShare(@"\\nas01\BatMonTestFolder", "usrBatMonTest", "*=%$p$")) {}
            }
            catch (Exception unknown) { Assert.Fail(ExceptionFailMessage(unknown)); }
        }

        [TestMethod]
        public void EnumerateFiles()
        {
            try
            {
                using (var share = new UncShare(@"\\nas01\BatMonTestFolder", "usrBatMonTest", "*=%$p$"))
                {
                    var dir = new DirectoryInfo(share.Path);
                    Assert.IsTrue(dir.GetFiles().Length >= 1);
                }
            }
            catch (Exception unknown) { Assert.Fail(ExceptionFailMessage(unknown)); }
        }

        [TestMethod]
        public void Invalid_Path_ErrorHandling()
        {
            try
            {
                using (var share = new UncShare(@"\\nas01\NonExistantFolder", "usrBatMonTest", "*=%$p$")) { }
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                if (ex.Message == "The network path was not found")
                    Assert.IsTrue(true);
                else Assert.Fail(ExceptionFailMessage(ex));
            }
            catch (Exception unknown) { Assert.Fail(ExceptionFailMessage(unknown)); }
        }

        [TestMethod]
        public void Invalid_User_ErrorHandling()
        {
            try
            {
                using (var share = new UncShare(@"\\nas01\BatMonTestFolder", "wrongUser", "*=%$p$")) { }
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                if (ex.Message == "The user name or password is incorrect")
                    Assert.IsTrue(true);
                else Assert.Fail(ExceptionFailMessage(ex));
            }
            catch (Exception unknown) { Assert.Fail(ExceptionFailMessage(unknown)); }
        }

        [TestMethod]
        public void Invalid_Password_ErrorHandling()
        {
            try
            {
                using (var share = new UncShare(@"\\nas01\BatMonTestFolder", "usrBatMonTest", "wrongPassword")) { }
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                if (ex.Message == "The user name or password is incorrect")
                    Assert.IsTrue(true);
                else Assert.Fail(ExceptionFailMessage(ex));
            }
            catch (Exception unknown) { Assert.Fail(ExceptionFailMessage(unknown)); }
        }

        private string ExceptionFailMessage(Exception ex)
        {
            return "Excetpion was thrown of type:" + ex.GetType().ToString() + " with message:" + ex.Message;
        }


    }
}
