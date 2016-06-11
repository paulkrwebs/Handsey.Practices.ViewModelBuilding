namespace Handsey.Practices.ViewModelBuilding.Test
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

        [SetUp]
        public void Setup()
        {
            _contentHandlerPipeline = new Mock<IContentHandlerPipeline>();
            _propertyMapper = new Mock<IPropertyMapper>();
            _viewModelBuilder = new ViewModelBuilder(_propertyMapper.Object, _contentHandlerPipeline.Object);
        }

        [Test]
        public void Build_EpiServerModel_ModelMappedAndBuilt()
        {
            // Arrange
            _propertyMapper.Setup(
                x => x.Map<EPiServerModel, ViewModel>(It.IsAny<EPiServerModel>(), It.IsAny<ViewModel>()));

            // Act
            ViewModel viewModel = _viewModelBuilder.Build<EPiServerModel, ViewModel>(new EPiServerModel() { Title = "MoFo" });

            // Assert
            Assert.That(viewModel, Is.Not.Null);
            _contentHandlerPipeline.Verify(c => c.Raise(It.IsAny<HandlerArgs<EPiServerModel, ViewModel>>()), Times.Once());
            _propertyMapper.Verify(x => x.Map(It.IsAny<EPiServerModel>(), It.IsAny<ViewModel>()), Times.AtLeastOnce());
        }

        [Test]
        public void Build_EpiServerModel_ModelMappingFalseSoBuiltAndNotMapped()
        {
            // Arrange
            _propertyMapper.Setup(
                x => x.Map<EPiServerModel, ViewModel>(It.IsAny<EPiServerModel>(), It.IsAny<ViewModel>()));

            // Act
            ViewModel viewModel = _viewModelBuilder.Build<EPiServerModel, ViewModel>(new EPiServerModel() { Title = "MoFo" }, false);

            // Assert
            Assert.That(viewModel, Is.Not.Null);
            _contentHandlerPipeline.Verify(c => c.Raise(It.IsAny<HandlerArgs<EPiServerModel, ViewModel>>()), Times.Once());
            _propertyMapper.Verify(x => x.Map(It.IsAny<EPiServerModel>(), It.IsAny<ViewModel>()), Times.Never);
        }

        [Test]
        public void Build_EpiServerModelAndFormModel_ModelMappingTrueSoBuiltAndMapped()
        {
            // Arrange
            _propertyMapper.Setup(
                x => x.Map<EPiServerModel, ViewModel>(It.IsAny<EPiServerModel>(), It.IsAny<ViewModel>()));

            // Act
            ViewModel viewModel = _viewModelBuilder.Build<FormModel, EPiServerModel, ViewModel>(new FormModel() { Step = 1 }, new EPiServerModel() { Title = "MoFo" });

            // Assert
            Assert.That(viewModel, Is.Not.Null);
            _contentHandlerPipeline.Verify(c => c.Raise(It.IsAny<HandlerArgs<EPiServerModel, ViewModel>>()), Times.Once());
            _propertyMapper.Verify(x => x.Map(It.IsAny<EPiServerModel>(), It.IsAny<ViewModel>()), Times.AtLeastOnce());
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