using DataModel.Shared;
using System.Collections.Generic;

namespace DataModel.Shared
{
    /// <summary>
    /// Compnent Chart Options
    /// </summary>
    public class ComponentPieChartOptions : ComponentChartOptions
    {
        #region Setup
        public ComponentPieChartOptions()
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
            Is3D = true;
            EnableInteractivity = true;
            PieHole = 0.0M;
        }

        public ComponentPieChartOptions(string title, LegendLocationValue legendLocation, LegendAlignmentValue legendAlignment, bool is3D = false, bool enableInteractivity = true, decimal pieHole = 0.0M)
        {
            new ComponentPieChartOptions(
                title,
                legendLocation,
                legendAlignment,
                is3D,
                enableInteractivity,
                pieHole,
                400,
                300
                );
        }
        public ComponentPieChartOptions(string title, LegendLocationValue legendLocation, LegendAlignmentValue legendAlignment, bool is3D = false, bool enableInteractivity = true, decimal pieHole = 0.0M, int width = 400, int height = 300)
        {
            Title = title;
            Width = width;
            Height = height;
            LegendLocation = legendLocation;
            LegendAlignment = legendAlignment;
            Is3D = is3D;
            EnableInteractivity = enableInteractivity;
            PieHole = pieHole;
            Width = width;
            Height = height;
        }
        #endregion

        /// <summary>
        /// bool, Determines whether the chart renders in 3D
        /// Default = True
        /// </summary>
        public bool Is3D { get; set; }

        /// <summary>
        /// bool, Determines whether the chart throws user-based events or reacts to user interaction
        /// Default = True
        /// </summary>
        public bool EnableInteractivity { get; set; }

        /// <summary>
        /// decimal, Determines whether the chart shows as a pie or donut
        /// valid values are any decimal between 0-1 although 0.4-0.6 look best
        /// Default = 0 (Pie)
        /// </summary>
        public decimal PieHole { get; set; }
    }
}
