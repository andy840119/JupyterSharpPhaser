﻿using System;
using System.Collections.Generic;
using System.Text;
using JupyterSharpPhaser.Renderers.Html;
using JupyterSharpPhaser.Syntax;
using JupyterSharpPhaser.Test.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JupyterSharpPhaser.Test
{
    [TestClass]
    public class TestJupyterHtmlRenderer
    {
        #region Utilities

        protected void ConvertDocumentToHtmlFile(JupyterDocument document, string fileName , bool openFile = false)
        {
            using (var writer = FileHelper.CreateStreamWriter(fileName))
            {
                var renderer = new HtmlRenderer(writer);
                renderer.Render(document);
            }

            if (openFile)
                FileHelper.ProcessFile(fileName);
        }

        #endregion

        #region MarkdownCell


        #endregion

        #region Code Cell


        #endregion

        #region RawCell


        #endregion

        #region MetaData



        #endregion

        #region Other Document

        [TestMethod]
        public void TestReadingJpyterDocument2()
        {
            var jupyterText = JupyterDocumentHelper.GetFileStringByFileName("01-Python Crash Course.ipynb");
            var document = Jupyter.Parse(jupyterText);

            //convert to html and open
            ConvertDocumentToHtmlFile(document, "01-Python Crash Course.html",false);

        }

        #endregion
    }
}
