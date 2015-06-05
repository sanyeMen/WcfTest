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

namespace WpfWorkTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.page_test.OnPaging += Paging;
        }

        private void Paging(PagingEventArgs e)
        {
            this.tb_1_Copy.Text = "点击了第" + e.pageIndex + "页";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.page_test.SetPageCount(int.Parse(this.tb_1.Text));
        }
    }
}
