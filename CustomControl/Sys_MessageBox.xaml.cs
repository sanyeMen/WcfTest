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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomControl
{
    /// <summary>
    /// Sys_MessageBox.xaml 的交互逻辑
    /// </summary>
    public partial class Sys_MessageBox : Window
    {

        #region 属性
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
        #endregion

        private Sys_MessageBox(string title, string message, bool? isSuccess, bool isOkCancel)
        {
            InitializeComponent();

            this.Title = title;
            this.Message = message;

            this.tb_title.Text = title;
            this.tb_msg.Text = message;

            OnlyOk(isOkCancel);
            if (isSuccess.HasValue)
            {
                Pic(true, (bool)isSuccess);
            }
            else
            {
                Pic(false, false);
            }
        }

        #region 静态函数
        public static MessageBoxResult Show(Window owner, string message)
        {
            //Sys_MessageBox box = new Sys_MessageBox();

            return MessageBoxResult.OK;
        }
        public static MessageBoxResult Show(Window owner, string message, string title)
        {
            Sys_MessageBox sys_box = new Sys_MessageBox(title, message, null, false);
            sys_box.Width = 376;
            sys_box.Height = 168;
            sys_box.ShowDialog();
            sys_box.WindowStartupLocation = WindowStartupLocation.CenterOwner;


            HwndSource winformWindow =
            (System.Windows.Interop.HwndSource.FromDependencyObject(owner) as System.Windows.Interop.HwndSource);
            if (winformWindow != null)
                new WindowInteropHelper(sys_box) { Owner = winformWindow.Handle };

            sys_box.Show();

            return MessageBoxResult.OK;
        }

        public static MessageBoxResult Show(string message, string title, bool isSuccess)
        {
            return MessageBoxResult.OK;
        }

        public static MessageBoxResult Show(string message, bool isSuccess)
        {
            return MessageBoxResult.OK;
        }
        #endregion

        private const double MSG_NOPIC_WIDTH = 350;
        private const double MSG_PIC_WIDTH = 290;
        private const double MSG_NOPIC_LEFT = 16;
        private const double MSG_PIC_LEFT = 76;

        private Thickness BT_OK_MARGIN = new Thickness(244, 7, 76, 8);

        private void bt_ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void bt_cancle_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void bt_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnlyOk(bool onlyOk)
        {
            if (onlyOk)
            {
                this.bt_cancle.Visibility = System.Windows.Visibility.Hidden;
                this.bt_ok.Margin = this.bt_cancle.Margin;
            }
            else
            {
                this.bt_cancle.Visibility = System.Windows.Visibility.Visible;
                this.bt_ok.Margin = BT_OK_MARGIN;
            }
        }

        private void Pic(bool isHasPic, bool isSuccess)
        {
            if (!isHasPic)
            {
                this.cav_success.Visibility = System.Windows.Visibility.Hidden;
                this.cav_fail.Visibility = System.Windows.Visibility.Hidden;
                this.tb_msg.Width = MSG_NOPIC_WIDTH;
                this.tb_msg.SetValue(Canvas.LeftProperty, MSG_NOPIC_LEFT);
            }
            else
            {
                if (isSuccess)
                {
                    this.cav_success.Visibility = System.Windows.Visibility.Visible;
                    this.cav_fail.Visibility = System.Windows.Visibility.Hidden;
                    this.tb_msg.Width = MSG_PIC_WIDTH;
                    this.tb_msg.SetValue(Canvas.LeftProperty, MSG_PIC_LEFT);
                }
                else
                {
                    this.cav_fail.Visibility = System.Windows.Visibility.Visible;
                    this.cav_success.Visibility = System.Windows.Visibility.Hidden;
                    this.tb_msg.Width = MSG_PIC_WIDTH;
                    this.tb_msg.SetValue(Canvas.LeftProperty, MSG_PIC_LEFT);
                }
            }
        }
    }
}
