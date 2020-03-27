using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Api.Controllers;
using FluentAssertions;
using Xunit;

namespace Tests
{
    public class DefaultControllerTests
    {
        private readonly DefaultController _controller;

        public DefaultControllerTests()
        {
            _controller = new DefaultController();
        }

        [Fact]
        public void ShouldPassGetValues()
        {
            var values = _controller.Get();
            values.Should().NotBeNullOrEmpty();
            values.Count().Should().Be(2);
        }
    }
}
