using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Painter2
{
    /// <summary>
    /// Painter.xaml 的交互逻辑
    /// </summary>
    public partial class Painter : UserControl
    {
        private ColorBlock _block;
        public Painter(int hb, int width, Color initial, Color target)
        {
            Width = width;
            Height = width;
            InitializeComponent();
            var one = width / (hb + 1);
            var w = width - one;
            _block = new ColorBlock(hb,initial, target)
            {
                Width = w,
                Height = w
            };
            InnerCanvas.Children.Add(_block);
            for (var i = 0; i < hb; i++)
            {
                var rowButton = new Button()
                {
                    Width = one,
                    Height = one,
                    
                    Background = new SolidColorBrush(target)
                };
                if (one < 41)
                {
                    rowButton.Content = $"r{i}";
                    rowButton.Padding = new Thickness(0);
                }
                else
                {
                    rowButton.Content = new Image()
                    {
                        Source = new BitmapImage(new Uri("/dart.png", UriKind.Relative))
                    };
                    rowButton.Padding = new Thickness(10);
                }
                var index = i;
                rowButton.Click += (sender, args) =>
                {
                    _block.Paint(Direction.Row,index);
                };
                Canvas.SetRight(rowButton,0.0);
                Canvas.SetTop(rowButton,i*one*1.0);
                
                InnerCanvas.Children.Add(rowButton);
            }

            for (var j = 0; j < hb; j++)
            {
                var colButton = new Button()
                {
                    Width = one,
                    Height = one,
                    Background = new SolidColorBrush(initial)
                };
                if (one < 41)
                {
                    colButton.Content = $"c{j}";
                    colButton.Padding = new Thickness(5);
                }
                else
                {
                    colButton.Content = new Image()
                    {
                        Source = new BitmapImage(new Uri("/dart.png", UriKind.Relative))
                    };
                    colButton.Padding = new Thickness(10);
                }
                var index = j;
                colButton.Click += (sender, args) =>
                {
                    _block.Paint(Direction.Col,index);
                };
                Canvas.SetBottom(colButton,0.0);
                Canvas.SetLeft(colButton,j*one*1.0);
                
                InnerCanvas.Children.Add(colButton);
            }

        }
    }
}
