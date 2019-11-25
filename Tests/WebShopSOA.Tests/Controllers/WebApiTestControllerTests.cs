using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopSOA.Controllers;
using WebShopSOA.Interfaces.Api;
using Assert = Xunit.Assert;

namespace WebShopSOA.Tests.Controllers
{
    [TestClass]
    public class WebApiTestControllerTests
    {
        [TestMethod]
        public async Task Index_Return_View_WithValues()
        {
            var expected_values = new[] { "1", "2", "3" };

            var value_service = new Mock<IValuesService>();
            value_service
                .Setup(service => service.GetAsync())
                .ReturnsAsync(expected_values);

            var controller = new WebApiTestController(value_service.Object);

            var result = await controller.Index();

            var view_result = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<string>>(view_result.Model);

            Assert.Equal(expected_values.Length, model.Count());

            value_service.Verify(service => service.GetAsync());
            value_service.VerifyNoOtherCalls();
        }
    }
}
