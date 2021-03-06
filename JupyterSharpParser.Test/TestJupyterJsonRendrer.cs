﻿using System.IO;
using JupyterSharpParser.Renderers.Json;
using JupyterSharpParser.Syntax;
using JupyterSharpParser.Test.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JupyterSharpParser.Test
{
    [TestClass]
    public class TestJupyterJsonRendrer
    {
        #region Utilities

        protected string ConvertDocumentToJson(JupyterDocument document)
        {
            var writer = new StringWriter();
            var renderer = new JsonRenderer(writer);
            renderer.Render(document);
            writer.Flush();
            return writer.ToString();
        }

        #endregion

        #region File

        [TestMethod]
        public void TestReadingJpyterDocument1()
        {
            var jupyterText = JupyterDocumentHelper.GetFileStringByFileName("Discover Sentiments in Tweets.ipynb");
            var document = Jupyter.Parse(jupyterText);

            //convert to json
            var documentJson = ConvertDocumentToJson(document);

            //convert to document again
            var document2 = Jupyter.Parse(documentJson);

            //Two class's property should be equal
            Assert.IsTrue(CompareHelper.CompareWithJsonFormat(document, document2));

            //Change name
            document2.Metadata.KernelInfo.Name = "AAAA";

            //Should not equal
            Assert.IsFalse(CompareHelper.CompareWithJsonFormat(document, document2));
        }

        [TestMethod]
        public void TestReadingJpyterDocument2()
        {
            var jupyterText = JupyterDocumentHelper.GetFileStringByFileName("01-Python Crash Course.ipynb");
            var document = Jupyter.Parse(jupyterText);

            //convert to json
            var documentJson = ConvertDocumentToJson(document);

            //convert to document again
            var document2 = Jupyter.Parse(documentJson);

            //Two class's property should be equal
            Assert.IsTrue(CompareHelper.CompareWithJsonFormat(document, document2));
        }

        #endregion
    }
}