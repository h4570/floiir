using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.BusinessLogic.Builders.Email;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.BusinessLogic.Builders.Email.Tests
{
    [TestClass()]
    public class BlueGrayEmailBuilderTests
    {
        private  BlueGrayEmailBuilder _bgEmailBuilder;

        #region Reinitialize

        [TestMethod("Reinitialize_NewBuilder")]
        public void Reinitialize_NewBuilder_ReturnHtmlWithBeginning()
        {
            // arrange
            _bgEmailBuilder= new BlueGrayEmailBuilder();

            // act
            _bgEmailBuilder.Reinitialize();
            var result = _bgEmailBuilder.GetHTML();

            // assert
            Assert.IsTrue(result.Contains("<head>"));
        }

        [TestMethod("Reinitialize_BuilderWithHeader")]
        public void Reinitialize_BuilderWithHeader_ReturnHtmlWithoutHeader()
        {
            // arrange
            _bgEmailBuilder = new BlueGrayEmailBuilder();
            _bgEmailBuilder.AddHeader("TestHeader#@!");

            // act
            _bgEmailBuilder.Reinitialize();
            var result = _bgEmailBuilder.GetHTML();

            // assert
            Assert.IsTrue(!result.Contains("TestHeader#@!"));
        }

        #endregion

        #region AddHeader

        [TestMethod("AddHeader_NewBuilder")]
        public void AddHeader_NewBuilder_ReturnHtmlWithHeader()
        {
            // arrange
            _bgEmailBuilder = new BlueGrayEmailBuilder();

            // act
            _bgEmailBuilder.AddHeader("TestHeader#@!");
            var result = _bgEmailBuilder.GetHTML();

            // assert
            Assert.IsTrue(result.Contains("TestHeader#@!"));
        }

        [TestMethod("AddHeader_WithHeader")]
        public void AddHeader_WithHeader_ReturnHtmlWithBothHeaders()
        {
            // arrange
            _bgEmailBuilder = new BlueGrayEmailBuilder();
            _bgEmailBuilder.AddHeader("TestHeader#@!1");

            // act
            _bgEmailBuilder.AddHeader("TestHeader#@!2");
            var result = _bgEmailBuilder.GetHTML();

            // assert
            Assert.IsTrue(result.Contains("TestHeader#@!1") && result.Contains("TestHeader#@!2"));
        }

        #endregion

        #region AddText 

        [TestMethod("AddText_NewBuilder")]
        public void AddText_NewBuilder_ReturnHtmlWithText()
        {
            // arrange
            _bgEmailBuilder = new BlueGrayEmailBuilder();

            // act
            _bgEmailBuilder.AddText("TestText#@!");
            var result = _bgEmailBuilder.GetHTML();

            // assert
            Assert.IsTrue(result.Contains("TestText#@!"));
        }

        #endregion

        #region AddButton 

        [TestMethod("AddButton_NewBuilder")]
        public void AddButton_NewBuilder_ReturnHtmlWithButton()
        {
            // arrange
            _bgEmailBuilder = new BlueGrayEmailBuilder();

            // act
            _bgEmailBuilder.AddButton("TestBtn#@!",null);
            var result = _bgEmailBuilder.GetHTML();

            // assert
            Assert.IsTrue(result.Contains("TestBtn#@!"));
        }

        #endregion

        #region AddCenteredImage 

        [TestMethod("AddCenteredImage_NewBuilder")]
        public void AddCenteredImage_NewBuilder_ReturnHtmlWithImage()
        {
            // arrange
            _bgEmailBuilder = new BlueGrayEmailBuilder();

            // act
            _bgEmailBuilder.AddCenteredImage("TestUrl#@!");
            var result = _bgEmailBuilder.GetHTML();

            // assert
            Assert.IsTrue(result.Contains("TestUrl#@!"));
        }

        #endregion

        #region AddSpacer

        [TestMethod("AddCenteredImage_NewBuilder")]
        public void AddSpacer_NewBuilder_ReturnHtmlWithSpacer()
        {
            // arrange
            _bgEmailBuilder = new BlueGrayEmailBuilder();

            // act
            _bgEmailBuilder.AddSpacer();
            var result = _bgEmailBuilder.GetHTML();

            // assert
            Assert.IsTrue(result.Contains("<!-- SPACER -->"));
        }

        #endregion
    }
}