﻿using Couchbase.Extensions.DependencyInjection.Internal;
using Moq;
using Xunit;

namespace Couchbase.Extensions.DependencyInjection.UnitTests.Internal
{
    public class NamedBucketProxyGeneratorTests
    {
        #region GetProxy

        [Fact]
        public void GetProxy_GoodInterface_ReturnsProxy()
        {
            //  Arange

            var bucketProvider = new Mock<IBucketProvider>();

            var generator = new NamedBucketProxyGenerator();

            // Act

            var proxy = generator.GetProxy<ITestBucketProvider>(bucketProvider.Object, "test", null);

            // Assert

            Assert.NotNull(proxy);
        }

        public void GetProxy_TwoInterfaces_ReturnsTwoProxies()
        {
            //  Arange

            var bucketProvider = new Mock<IBucketProvider>();

            var generator = new NamedBucketProxyGenerator();

            // Act

            var proxy = generator.GetProxy<ITestBucketProvider>(bucketProvider.Object, "test", null);
            var proxy2 = generator.GetProxy<ITestBucketProvider2>(bucketProvider.Object, "test2", null);

            // Assert

            Assert.NotNull(proxy);
            Assert.NotNull(proxy2);
            Assert.NotEqual(proxy.GetType(), proxy2.GetType());
        }

        [Fact]
        public void GetProxy_TwiceWithSameInterface_ReturnsSameProxyType()
        {
            //  Arange

            var bucketProvider = new Mock<IBucketProvider>();

            var generator = new NamedBucketProxyGenerator();

            // Act

            var proxy = generator.GetProxy<ITestBucketProvider>(bucketProvider.Object, "test", null);
            var proxy2 = generator.GetProxy<ITestBucketProvider>(bucketProvider.Object, "test", null);

            // Assert

            Assert.NotNull(proxy);
            Assert.NotNull(proxy2);
            Assert.Equal(proxy.GetType(), proxy2.GetType());
        }

        #endregion

        #region Helpers

        public interface ITestBucketProvider : INamedBucketProvider
        {
        }

        public interface ITestBucketProvider2 : INamedBucketProvider
        {
        }

        #endregion
    }
}
