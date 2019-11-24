using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using WebShopSOA.Controllers;
using Assert = Xunit.Assert;

namespace WebShopSOA.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index_Return_View()
        {
            var controller = new HomeController();

            var result = controller.Index();

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Contact_Return_View()
        {
            #region Arrange
            var controller = new HomeController();
            #endregion

            #region Action
            var result = controller.Contact();
            #endregion
            
            #region Assert
            Assert.IsType<ViewResult>(result);
            #endregion
        }

        [TestMethod]
        public void PageNotFound_Return_View()
        {
            var controller = new HomeController();

            var result = controller.PageNotFound();

            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void ErrorStatus_404_RedirectTo_PageNotFound()
        {
            var controller = new HomeController();
            var result = controller.ErrorStatus("404");
            var redirect_to_action = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirect_to_action.ControllerName);
            Assert.Equal(nameof(HomeController.PageNotFound), redirect_to_action.ActionName);
        }
    }
}
