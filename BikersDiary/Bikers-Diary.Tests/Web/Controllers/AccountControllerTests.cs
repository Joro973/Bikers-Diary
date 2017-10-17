using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BikersDiary.ForumSystem.Web.Controllers;

namespace Bikers_Diary.Tests.Web.Controllers
{
    [TestClass]
    public class AccountControllerTests
    {
        [TestMethod]
        public void Login_ShouldReturnViewNotNull()
        {
            //Arange
            AccountController accController = new AccountController();

            //Act
            ViewResult viewResult = accController.Login(It.IsAny<string>()) as ViewResult;

            //Assert
            Assert.IsNotNull(viewResult);
        }

        [TestMethod]
        public void Register_ShouldReturnViewNotNull()
        {
            //Arange
            AccountController accController = new AccountController();

            //Act
            ViewResult viewResult = accController.Register() as ViewResult;

            //Assert
            Assert.IsNotNull(viewResult);
        }
    }
}
