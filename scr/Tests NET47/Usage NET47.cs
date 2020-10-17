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
                    Assert.IsTrue(share.GetFiles().Length >= 1);
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
                //Win32Exception Error Codes
                //https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/18d8fbe8-a967-4f1c-ae50-99ca8e491d2d
                //Message = "The network path was not found"
                if (ex.NativeErrorCode == 53)
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
                //Win32Exception Error Codes
                //https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/18d8fbe8-a967-4f1c-ae50-99ca8e491d2d
                //NativeErrorCode = 86      Message = "The specified network password is not correct"
                //NativeErrorCode = 1326    Message = "The user name or password is incorrect"
                if (ex.NativeErrorCode == 86 | ex.NativeErrorCode == 1326)
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
                //Win32Exception Error Codes
                //https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-erref/18d8fbe8-a967-4f1c-ae50-99ca8e491d2d
                //NativeErrorCode = 86      Message = "The specified network password is not correct"
                //NativeErrorCode = 1326    Message = "The user name or password is incorrect"
                if (ex.NativeErrorCode == 86 | ex.NativeErrorCode == 1326)
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
