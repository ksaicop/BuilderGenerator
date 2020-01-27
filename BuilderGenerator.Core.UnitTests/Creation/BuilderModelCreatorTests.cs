using BuilderGenerator.Core.Analysis;
using BuilderGenerator.Core.Creation;
using System;
using Xunit;

namespace BuilderGenerator.Core.UnitTests.Creation
{
    public class BuilderModelCreatorTests
    {
        private readonly BuilderModelCreator _builderModelCreator;

        public BuilderModelCreatorTests()
        {
            _builderModelCreator = new BuilderModelCreator();
        }

        [Fact]
        public void AnalysisResultIsNull_ArgumentNullExceptionThrown()
        {
            // Arrange
            
            AnalysisResult analysisResult = null;

            // Act
            Action action = () => _builderModelCreator.Create(analysisResult);

            // Assert
            Assert.Throws<ArgumentNullException>("analysisResult", action);
        }

        [Fact]
        public void NoProperties_FieldsEmpty()
        {
            // Arrange
            var analysisResult = new AnalysisResult("ClassName", Array.Empty<ClassMember>());

            // Act
            var builderModel = _builderModelCreator.Create(analysisResult);

            // Assert
            Assert.NotNull(builderModel);
            Assert.Empty(builderModel.Properties);
        }

        [Fact]
        public void NoProperties_OriginalClassNameAsExpected()
        {
            // Arrange
            var analysisResult = new AnalysisResult("ClassName", Array.Empty<ClassMember>());

            // Act
            var builderModel = _builderModelCreator.Create(analysisResult);

            // Assert
            Assert.NotNull(builderModel);
            Assert.Equal("ClassName", builderModel.OriginalClassName);
        }

        [Fact]
        public void NoProperties_BuilderClassNameAsExpected()
        {
            // Arrange
            var analysisResult = new AnalysisResult("ClassName", Array.Empty<ClassMember>());

            // Act
            var builderModel = _builderModelCreator.Create(analysisResult);

            // Assert
            Assert.NotNull(builderModel);
            Assert.Equal("ClassNameBuilder", builderModel.BuilderClassName);
        }

        [Fact]
        public void SingleProperty_SingleBuilderProperty()
        {
            // Arrange
            var property = new ClassMember("string", "Property");
            var analysisResult = new AnalysisResult("ClassName", new[] { property });

            // Act
            var builderModel = _builderModelCreator.Create(analysisResult);

            // Assert
            Assert.NotNull(builderModel);
            Assert.Single(builderModel.Properties);
        }

        [Fact]
        public void SinglePropertyWithTypeString_BuilderPropertyTypeAsExpected()
        {
            // Arrange
            var property = new ClassMember("string", "Property");
            var analysisResult = new AnalysisResult("ClassName", new[] { property });

            // Act
            var builderModel = _builderModelCreator.Create(analysisResult);

            // Assert
            Assert.NotNull(builderModel);
            var builderProperty = Assert.Single(builderModel.Properties);
            Assert.Equal("string", builderProperty.Type);
        }

        [Fact]
        public void SinglePropertyWithSimpleName_BuilderPropertyPropertyNameAsExpected()
        {
            // Arrange
            var property = new ClassMember("string", "Property");
            var analysisResult = new AnalysisResult("ClassName", new[] { property });

            // Act
            var builderModel = _builderModelCreator.Create(analysisResult);

            // Assert
            Assert.NotNull(builderModel);
            var builderProperty = Assert.Single(builderModel.Properties);
            Assert.Equal("Property", builderProperty.PropertyName);
        }

        [Fact]
        public void SinglePropertyWithSimpleName_BuilderPropertyFieldNameAsExpected()
        {
            // Arrange
            var property = new ClassMember("string", "Property");
            var analysisResult = new AnalysisResult("ClassName", new[] { property });

            // Act
            var builderModel = _builderModelCreator.Create(analysisResult);

            // Assert
            Assert.NotNull(builderModel);
            var builderProperty = Assert.Single(builderModel.Properties);
            Assert.Equal("_property", builderProperty.FieldName);
        }

        [Fact]
        public void SinglePropertyWithTypeString_BuilderPropertyFieldValueAsExpected()
        {
            // Arrange
            var property = new ClassMember("string", "Property");
            var analysisResult = new AnalysisResult("ClassName", new[] { property });

            // Act
            var builderModel = _builderModelCreator.Create(analysisResult);

            // Assert
            Assert.NotNull(builderModel);
            var builderProperty = Assert.Single(builderModel.Properties);
            Assert.Equal("\"Property\"", builderProperty.FieldValue);
        }

        [Fact]
        public void SingleProperty_BuilderPropertyParameterNameAsExpected()
        {
            // Arrange
            var property = new ClassMember("string", "Property");
            var analysisResult = new AnalysisResult("ClassName", new[] { property });

            // Act
            var builderModel = _builderModelCreator.Create(analysisResult);

            // Assert
            Assert.NotNull(builderModel);
            var builderProperty = Assert.Single(builderModel.Properties);
            Assert.Equal("property", builderProperty.ParameterName);
        }
    }
}
