using DataModel.Shared;
using System.Collections.Generic;

namespace DataModel.Shared
{
    /// <summary>
    /// Compnent Chart Options
    /// </summary>
    public class ComponentChartOptions
    {
        #region Setup
        public ComponentChartOptions()
        {
            Title = "";
            Width = 400;
            Height = 300;
            LegendTextColor = "#ffffff";
            LegendTextFontName = "Arial";
            LegendTextFontSize = 10;
            LegendTextFontBold = false;
            LegendTextFontItalic = false;
            LegendLocation = LegendLocationValue.Top;
            LegendAlignment = LegendAlignmentValue.Center;

        }
        #endregion

        /// <summary>
        /// String, Chart Title
        /// Default = ''
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Boolean, Determines the width of the chart
        /// Default = 400
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Int, Determines the height of the chart
        /// Default = 300
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// String, Determines the hex color of the chart background
        /// Default = #000000 (White)
        /// </summary>
        public string BackgroundColor { get; set; }

        /// <summary>
        /// String, Determines the hex color of the chart background stoke
        /// Default = #000000 (White)
        /// </summary>
        public string BackgroundStrokeColor { get; set; }

        /// <summary>
        /// String, Determines the width of the chart background stroke
        /// Default = #000000 (White)
        /// </summary>
        public string BackgroundStrokeWidth { get; set; }

        /// <summary>
        /// String, Determines the hex color of the text in the legend
        /// Default = #ffffff (Black)
        /// </summary>
        public string LegendTextColor { get; set; }

        /// <summary>
        /// String, Determines the font of the text in the legend
        /// Default = Arial
        /// </summary>
        public string LegendTextFontName { get; set; }

        /// <summary>
        /// Int, Determines the font size of the text in the legend
        /// Default = 10
        /// </summary>
        public int LegendTextFontSize { get; set; }

        /// <summary>
        /// Boolean, Determines if the font of the text in the legend is bolded
        /// Default = false
        /// </summary>
        public bool LegendTextFontBold { get; set; }

        /// <summary>
        /// Boolean, Determines if the font of the text in the legend is italicized
        /// Default = false
        /// </summary>
        public bool LegendTextFontItalic { get; set; }

        /// <summary>
        /// LegendLocationValue, Determines where the legend is located.
        /// Default = Top
        /// </summary>
        public LegendLocationValue LegendLocation { get; set; }

        /// <summary>
        /// LegendLocationValue, Determines where the legend is aligned.
        /// Default = Top
        /// </summary>
        public LegendAlignmentValue LegendAlignment { get; set; }

        public enum LegendLocationValue { Top, Bottom, Left, Right, None, Labeled }
        public enum LegendAlignmentValue { Start, Center, End }
    }
}
