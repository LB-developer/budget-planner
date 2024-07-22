using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Collections.Generic;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using LiveChartsCore.SkiaSharpView.VisualElements;
using LiveChartsCore.SkiaSharpView.Extensions;

namespace BudgetPlanner.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<ISeries> Series { get; set; } 
            = new[]
            {
                new PieSeries<int> { Values = new[]{ 2 } },
                new PieSeries<int> { Values = new[]{ 4 } }, 
                new PieSeries<int> { Values = new[]{ 1 } }, 
                new PieSeries<int> { Values = new[]{ 4 } }, 
                new PieSeries<int> { Values = new[]{ 3 } }, 
            };

        public LabelVisual Title { get; set; } =
        new LabelVisual
        {
            Text = "Income v.s. Expenses",
            TextSize = 25,
            Padding = new LiveChartsCore.Drawing.Padding(10),
            Paint = new SolidColorPaint(SKColors.DarkSlateGray)
        };
    } 
}
