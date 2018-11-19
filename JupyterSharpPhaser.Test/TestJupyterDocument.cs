using JupyterSharpPhaser.Syntax.Cell;
using JupyterSharpPhaser.Syntax.Cell.Output;
using JupyterSharpPhaser.Test.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace JupyterSharpPhaser.Test
{
    [TestClass]
    public class TestJupyterDocument
    {
        #region Markdown Cell

        [TestMethod]
        public void TestMarkdownCell()
        {
            var jupyterText = @"{
  ""cells"": [
    {
      ""cell_type"": ""markdown"",
      ""metadata"": {
        
      },
      ""source"": [
        ""___\n"",
        ""\n"",
        ""<a href='http://www.pieriandata.com'> <img src='../Pierian_Data_Logo.png' /></a>\n"",
        ""___\n"",
        ""# Python Crash Course\n"",
      ]
    }]
}";
            var document = Jupyter.Parse(jupyterText);
            var markdownCell = document.Cells.FirstOrDefault() as MarkdownCell;

            Assert.AreEqual(CellType.Markdown, markdownCell.CellType);//Type
            Assert.AreEqual(typeof(Newtonsoft.Json.Linq.JObject), markdownCell.Metadata.GetType());//Metadata
            Assert.AreEqual(5, markdownCell.Source.Count);//Count
            Assert.AreEqual("# Python Crash Course\n", markdownCell.Source.LastOrDefault());//Text
        }

        #endregion

        #region Code Cell

        [TestMethod]
        public void TestCodeCell()
        {
            var jupyterText = @"{
  ""cells"": [
    {
        ""cell_type"": ""code"",
        ""execution_count"": 7,
        ""metadata"": {},
        ""outputs"": [
        {
            ""data"": {
                ""text/plain"": [
                ""3""
                ]
            },
            ""execution_count"": 7,
            ""metadata"": {},
            ""output_type"": ""execute_result""
        }
        ],
        ""source"": [
        ""1 * 3""
        ]
    }]
}";
            var document = Jupyter.Parse(jupyterText);
            var codeCell = document.Cells.FirstOrDefault() as CodeCell;

            Assert.AreEqual(CellType.Code, codeCell.CellType);//Type
            Assert.AreEqual(7, codeCell.ExecutionCount);//ExecutionCount
            Assert.AreEqual(false, codeCell.MetaData.Collapsed);//MetaData
            Assert.AreEqual(false, codeCell.MetaData.AutoScroll);//MetaData
            Assert.AreEqual(1, codeCell.Source.Count());//Source
            Assert.AreEqual("1 * 3", codeCell.Source.LastOrDefault());//Source
        }

        [TestMethod]
        public void TestCodeCellStreamOutput()
        {
            var jupyterText = @"{
  ""cells"": [
    {
   ""cell_type"": ""code"",
   ""execution_count"": 22,
   ""metadata"": {},
   ""outputs"": [
    {
     ""name"": ""stdout"",
     ""output_type"": ""stream"",
     ""text"": [
      ""hello\n""
     ]
    }
   ],
   ""source"": [
    ""print(x)""
   ]
  }
 ]
}";
            var document = Jupyter.Parse(jupyterText);
            var codeCell = document.Cells.FirstOrDefault() as CodeCell;
            var streamOutput = codeCell.Outputs.FirstOrDefault() as StreamOutput;

            Assert.AreEqual(OutputType.Stream, streamOutput.OutputType);//Type
            Assert.AreEqual("stdout", streamOutput.Name);//Name
            Assert.AreEqual(1, streamOutput.Text.Count());//Text
            Assert.AreEqual("hello\n", streamOutput.Text.LastOrDefault());//Text
        }

        [TestMethod]
        public void TestCodeCellDisplayDataOutput()
        {
            var jupyterText = @"{
  ""cells"": [
    {
        ""cell_type"": ""code"",
        ""execution_count"": 7,
        ""metadata"": {
            ""scrolled"": true,
            ""collapsed"": false
        },
        ""outputs"": [
        {
            ""output_type"": ""display_data"",
            ""data"": {
                ""image/png"": ""iVBORw0KGgoAAAANSUhEUgAAAT4AAAIXCAYAAAAFczJTAAAABHNCSVQICAgIfAhkiAAAAAlwSFlz\nAAALEgAACxIB0t1..."",
                ""text/plain"": ""<matplotlib.figure.Figure at 0x7f0ce3512320>""
            },
            ""metadata"": {}
        }
        ],
        ""source"": [
        ""1 * 3""
        ]
    }]
}";
            var document = Jupyter.Parse(jupyterText);
            var codeCell = document.Cells.FirstOrDefault() as CodeCell;
            var displayDataOutput = codeCell.Outputs.FirstOrDefault() as DisplayDataOutput;

            Assert.AreEqual(OutputType.DisplayData, displayDataOutput.OutputType);//Type
            Assert.AreEqual("iVBORw0KGgoAAAANSUhEUgAAAT4AAAIXCAYAAAAFczJTAAAABHNCSVQICAgIfAhkiAAAAAlwSFlz\nAAALEgAACxIB0t1...", displayDataOutput.Data.ImageData);//Data
            Assert.AreEqual(1, displayDataOutput.Data.Text.Count());//Data
            Assert.AreEqual("<matplotlib.figure.Figure at 0x7f0ce3512320>", displayDataOutput.Data.Text.LastOrDefault());//Data
            Assert.AreEqual(null, displayDataOutput.MetaData.Image);//MetaData
        }

        [TestMethod]
        public void TestCodeCellExecuteOutput()
        {
            var jupyterText = @"{
  ""cells"": [
    {
        ""cell_type"": ""code"",
        ""execution_count"": 7,
        ""metadata"": {},
        ""outputs"": [
        {
            ""data"": {
                ""text/plain"": [
                ""3""
                ]
            },
            ""execution_count"": 7,
            ""metadata"": {},
            ""output_type"": ""execute_result""
        }
        ],
        ""source"": [
        ""1 * 3""
        ]
    }]
}";
            var document = Jupyter.Parse(jupyterText);
            var codeCell = document.Cells.FirstOrDefault() as CodeCell;
            var codeOutput = codeCell.Outputs.FirstOrDefault() as ExecuteResultOutput;

            Assert.AreEqual(OutputType.ExecuteResult, codeOutput.OutputType);//Type
            Assert.AreEqual(7, codeOutput.ExecutionCount);//ExecutionCount
            Assert.AreEqual(null, codeOutput.MetaData.Image);//MetaData
            Assert.AreEqual(1, codeOutput.Data.Text.Count());//Data
            Assert.AreEqual("3", codeOutput.Data.Text.LastOrDefault());//Source
        }

        [TestMethod]
        public void TestCodeCellErrorOutput()
        {
        }

        #endregion

        #region RawCell

        [TestMethod]
        public void TestRawCell()
        {

        }

        #endregion

        #region MetaData

        [TestMethod]
        public void TestMetadata()
        {

        }

        #endregion

        #region File

        [TestMethod]
        public void TestReadingJpyterDocument1()
        {
            var jupyterText = JupyterDocumentHelper.GetFileStringByFileName("Discover Sentiments in Tweets.ipynb");
            var document = Jupyter.Parse(jupyterText);

            Assert.IsTrue(document!=null);//Phase success
        }

        #endregion
    }
}
