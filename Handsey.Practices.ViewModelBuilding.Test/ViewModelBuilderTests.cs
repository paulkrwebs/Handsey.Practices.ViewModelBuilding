﻿namespace Handsey.Practices.ViewModelBuilding.Test
{
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class ViewModelBuilderTests
    {
        #region Fields

        private Mock<IContentHandlerPipeline> _contentHandlerPipeline;
        private Mock<IPropertyMapper> _propertyMapper;
        private IViewModelBuilder _viewModelBuilder;

        #endregion Fields

        #region Tests

        public void Setup()
        {
            _contentHandlerPipeline = new Mock<IContentHandlerPipeline>();
            _propertyMapper = new Mock<IPropertyMapper>();
            _viewModelBuilder = new ViewModelBuilder(_propertyMapper.Object, _contentHandlerPipeline.Object);
        }

        [Test]
        public async void Build_EpiServerModel_ModelBuiltButNotHandledSoDefaultMappingUsed()
        {
            // Arrange
            // The normal "Setup" attribute doesn't work with async methods
            Setup();

            _propertyMapper.Setup(
                x => x.Map(It.IsAny<EPiServerModel>(), It.IsAny<ViewModel>()));

            _contentHandlerPipeline.Setup(
                x => x.Raise(It.IsAny<HandlerArgs<EPiServerModel, ViewModel>>())).Returns(async () => false);

            // Act
            ViewModel viewModel = await _viewModelBuilder.BuildAsync<EPiServerModel, ViewModel>(new EPiServerModel() { Title = "MoFo" });

            // Assert
            Assert.That(viewModel, Is.Not.Null);
            _contentHandlerPipeline.Verify(c => c.Raise(It.IsAny<HandlerArgs<EPiServerModel, ViewModel>>()), Times.Once());
            _propertyMapper.Verify(x => x.Map(It.IsAny<EPiServerModel>(), It.IsAny<ViewModel>()), Times.AtLeastOnce());
        }

        [Test]
        public async void Build_EpiServerModelAndFormModel_ModelBuiltButAndHandledSoDefaultMappingUsed()
        {
            // Arrange
            // The normal "Setup" attribute doesn't work with async methods
            Setup();

            _propertyMapper.Setup(
                x => x.Map(It.IsAny<EPiServerModel>(), It.IsAny<ViewModel>()));

            _contentHandlerPipeline.Setup(
                x => x.Raise(It.IsAny<HandlerArgs<EPiServerModel, ViewModel>>())).Returns(async () => true);

            // Act
            ViewModel viewModel = await _viewModelBuilder.BuildAsync<FormModel, EPiServerModel, ViewModel>(new FormModel() { Step = 1 }, new EPiServerModel() { Title = "MoFo" });

            // Assert
            Assert.That(viewModel, Is.Not.Null);
            _contentHandlerPipeline.Verify(c => c.Raise(It.IsAny<HandlerArgs<EPiServerModel, ViewModel>>()), Times.Once());
            _propertyMapper.Verify(x => x.Map(It.IsAny<EPiServerModel>(), It.IsAny<ViewModel>()), Times.Never());
        }

        #endregion Tests

        #region Data

        private class EPiServerModel
        {
            public string Title { get; set; }
        }

        private class FormModel
        {
            public int Step { get; set; }
        }

        private class ViewModel
        {
            public string Title { get; set; }
        }

        #endregion Data
    }
}