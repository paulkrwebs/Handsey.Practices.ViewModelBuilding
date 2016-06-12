namespace Handsey.Practices.ViewModelBuilding.Tests
{
    using Handsey.Handlers;
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Threading.Tasks;

    [TestFixture]
    public class ContentHandlerPipelineTests
    {
        private Mock<IApplicaton> _application;
        private ContentHandlerPipeline _contentHandlerPipeline;

        public void Setup()
        {
            _application = new Mock<IApplicaton>();
            _contentHandlerPipeline = new ContentHandlerPipeline(_application.Object);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Raise_Args_InvokeCalledOnHandseyApplicationNoHandersInvokedAndResultReturned(bool data)
        {
            // Arrange
            // The normal "Setup" attribute doesn't work with async methods
            Setup();

            _application.Setup(a => a.Invoke(It.IsAny<Action<IHandler<HandlerArgs>>>())).Returns(data);

            // Act
            bool result = _contentHandlerPipeline.Raise(new Mock<HandlerArgs>().Object);

            // Assert
            Assert.That(result, Is.EqualTo(data), "Invoke result should be returned");
            _application.Verify(a => a.Invoke(It.IsAny<Action<IHandler<HandlerArgs>>>()), Times.Once, "Handsey application was not called");
        }

        [TestCase(true)]
        [TestCase(false)]
        public async void RaiseAsync_Args_InvokeCalledOnHandseyApplicationNoHandlersInvokedAndResultReturned(bool data)
        {
            // Arrange
            // The normal "Setup" attribute doesn't work with async methods
            Setup();

            _application.Setup(a => a.InvokeAsync(It.IsAny<Func<IHandlerAsync<HandlerArgs>, Task>>())).ReturnsAsync(data);

            // Act
            bool result = await _contentHandlerPipeline.RaiseAsync(new Mock<HandlerArgs>().Object);

            // Assert
            Assert.That(result, Is.EqualTo(data), "Rsise should return true");
            _application.Verify(a => a.InvokeAsync(It.IsAny<Func<IHandlerAsync<HandlerArgs>, Task>>()), Times.Once, "Handsey application was not called");
        }

        #region // data

        public class MockHandlerArgs : HandlerArgs
        { }

        #endregion // data
    }
}