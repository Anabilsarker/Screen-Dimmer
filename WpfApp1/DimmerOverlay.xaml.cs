using System.Windows;

namespace ScreenDimmer
{
    /// <summary>
    /// Interaction logic for DimmerOverlay.xaml
    /// </summary>
    public partial class DimmerOverlay : Window
    {
        public DimmerOverlay()
        {
            InitializeComponent();
            this.Height = SystemParameters.WorkArea.Height;
            this.Width = SystemParameters.WorkArea.Width;
        }
    }
}
