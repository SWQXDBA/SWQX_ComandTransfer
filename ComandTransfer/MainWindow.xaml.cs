using UserControl = System.Windows.Controls.UserControl;


namespace ComandTransfer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : UserControl
    {
        private CTPlugin Plugin { get; }

        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(CTPlugin plugin) : this()
        {
            Plugin = plugin;
            DataContext = plugin.Config;
        }

      
    }
}
