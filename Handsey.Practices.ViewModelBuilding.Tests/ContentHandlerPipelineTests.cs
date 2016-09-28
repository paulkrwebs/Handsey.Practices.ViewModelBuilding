namespace Handsey.Practices.ViewModelBuilding.Tests
{
    using BuildUp;
    using Handsey.Handlers;
    using Moq;
    using NUnit.Framework;
    using System;
    using System.Threading.Tasks;

    [TestFixture]
    public class ContentHandlerPipelineTests
    {
        private Mock<IApplicaton> _application;
        private HandseyContentHandlerPipeline _contentHandlerPipeline;

        public void Setup()
        {
            _application = new Mock<IApplicaton>();
            _contentHandlerPipeline = new HandseyContentHandlerPipeline(_application.Object);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Raise_Args_InvokeCalledOnHandseyApplicationNoHandersInvokedAndResultReturned(bool data)
        {
            // Arrange
            // The normal "Setup" attribute doesn't work with async methods
            Setup();

            _application.Setup(a => a.Invoke(It.IsAny<Action<IContentHandler<ContentHandlerArgs>>>())).Returns(data);

            // Act
            bool result = _contentHandlerPipeline.Raise(new Mock<ContentHandlerArgs>().Object);

            // Assert
            Assert.That(result, Is.EqualTo(data), "Invoke result should be returned");
            _application.Verify(a => a.Invoke(It.IsAny<Action<IContentHandler<ContentHandlerArgs>>>()), Times.Once, "Handsey application was not called");
        }

        [TestCase(true)]
        [TestCase(false)]
        public async void RaiseAsync_Args_InvokeCalledOnHandseyApplicationNoHandlersInvokedAndResultReturned(bool data)
        {
            // Arrange
            // The normal "Setup" attribute doesn't work with async methods
            Setup();

            _application.Setup(a => a.InvokeAsync(It.IsAny<Func<IContentHandlerAsync<ContentHandlerArgs>, Task>>())).ReturnsAsync(data);

            // Act
            bool result = await _contentHandlerPipeline.RaiseAsync(new Mock<ContentHandlerArgs>().Object);

            // Assert
            Assert.That(result, Is.EqualTo(data), "Rsise should return true");
            _application.Verify(a => a.InvokeAsync(It.IsAny<Func<IContentHandlerAsync<ContentHandlerArgs>, Task>>()), Times.Once, "Handsey application was not called");
        }

        #region // data

        public class MockHandlerArgs : ContentHandlerArgs
        { }

        #endregion // data
    }
}