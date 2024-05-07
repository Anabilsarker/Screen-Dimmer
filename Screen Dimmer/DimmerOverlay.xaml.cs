using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

namespace ScreenDimmer
{
    /// <summary>
    /// Interaction logic for DimmerOverlay.xaml
    /// </summary>
    public partial class DimmerOverlay : Window
    {
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll")]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        const int GWL_EXSTYLE = -20;
        const int WS_EX_TRANSPARENT = 0x00000020;

        private NotifyIcon _notifyIcon;

        public DimmerOverlay()
        {
            InitializeComponent();
            InitializeNotifyIcon();
            this.Height = SystemParameters.WorkArea.Height;
            this.Width = SystemParameters.WorkArea.Width;
            this.Loaded += new RoutedEventHandler(Window_Loaded);
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IntPtr hWnd = new WindowInteropHelper(this).Handle;
            int extendedStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
            SetWindowLong(hWnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
        }

        private void InitializeNotifyIcon()
        {
            _notifyIcon = new NotifyIcon
            {
                Icon = new Icon("icon.ico"),
                Visible = true,
                Text = "Screen Dimmer"
            };

            //_notifyIcon.DoubleClick += (s, args) => this.Show();
            _notifyIcon.ContextMenuStrip = GetContextMenu();
        }

        private ContextMenuStrip GetContextMenu()
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Open", null, (s, e) => this.Show());
            contextMenu.Items.Add("Exit", null, (s, e) => Environment.Exit(0));
            return contextMenu;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _notifyIcon.Dispose(); // Clean up the icon when the application closes
        }
    }
}
