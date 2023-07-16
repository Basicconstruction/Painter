using System.Windows.Media;

namespace Painter2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            // var painter = new Painter(4, 400, Colors.Aquamarine, Colors.Coral);
            
            var painter = new Painter(20, 800, Colors.Aquamarine, Colors.Coral);
            InnerCanvas.Children.Add(painter);
        }
    }
}