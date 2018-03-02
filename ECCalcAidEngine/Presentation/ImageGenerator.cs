using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ECCalcAidData.Elements;

namespace ECCalcAidEngine.Presentation
{
    public static class ImageGenerator
    {
        internal static UIElement generateImage(ECCABeam selectedBeam)
        {

            Line line = new Line();
            Thickness thickness = new Thickness(101, -11, 362, 250);
            line.Margin = thickness;
            line.Visibility = System.Windows.Visibility.Visible;
            line.StrokeThickness = 4;
            line.Stroke = System.Windows.Media.Brushes.Black;
            
            //StartPoint
            line.X1 = 10;
            line.Y1 = 70;

            //EndPoint
            line.X2 = selectedBeam.Length / 25;
            line.Y2 = 70;

            return line;


            Rectangle rect;

            rect = new System.Windows.Shapes.Rectangle();
            rect.Stroke = new SolidColorBrush(Colors.Black);
            rect.Fill = new SolidColorBrush(Colors.Black);
            rect.Width = 50;
            rect.Height = 50;
            Canvas.SetLeft(rect, 50);
            Canvas.SetTop(rect, 050);








            return rect;
        }

        internal static IList<UIElement> generateImage(Canvas myCanvas, ECCABeam selectedBeam)
        {
            IList<UIElement> result = new List<UIElement>();


            double canvasWidth = myCanvas.ActualWidth;

            double canvasHeight= myCanvas.ActualHeight;
            double beamLength = selectedBeam.Length;

            double scalingfactor = getScalingfactor(canvasWidth, beamLength);

            double beamCanvasScale = 5000 / (canvasWidth * 0.75);

            double beamCanvasLength = (beamLength / beamCanvasScale) * scalingfactor;


            Line line = new Line();
            Thickness thickness = new Thickness(101, -11, 362, 250);
            line.Margin = thickness;
            line.Visibility = System.Windows.Visibility.Visible;
            line.StrokeThickness = 3;
            line.Stroke = System.Windows.Media.Brushes.Black;

            //StartPoint
            line.X1 = (canvasWidth / 2)  - (beamCanvasLength / 2);
            line.Y1 = canvasHeight / 2;

            //EndPoint
            line.X2 = (canvasWidth / 2) + (beamCanvasLength / 2);
            line.Y2 = canvasHeight / 2;
            result.Add(line);

            return result;

        }

        private static double getScalingfactor(double canvasWidth, double beamLength)
        {
            //Based on y\ =\ 1-e^{-0.3x}
            double result = 1;

            double e = Math.E;
            double k = 0.3 * -1;
            double lengthInMetre = beamLength / 1000;

            double sub1 = k * lengthInMetre;
            double sub2 = Math.Pow(e, sub1);

            result = 1 - sub2;

            return result;
        }
    }
}
