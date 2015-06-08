using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfWorkTest.customcontrol;
using WpfWorkTest.viewModel;

namespace WpfWorkTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewTest viewTest = new ViewTest();
        public MainWindow()
        {
            InitializeComponent();
            this.page_test.OnPaging += Paging;
            this.g_grid.DataContext = viewTest;
            viewTest.PageCount = "50";
        }

        private void Paging(PagingEventArgs e)
        {
            this.tb_show.Text = "点击了第" + e.pageIndex + "页";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.g_grid.DataContext = viewTest;
            viewTest.PageCount = "80";
        }
    }
}
