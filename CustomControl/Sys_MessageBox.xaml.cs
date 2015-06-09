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
        public string Caption { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
        #endregion

        private Sys_MessageBox(string title, string message, bool? isSuccess, bool isOnlyOk)
        {
            InitializeComponent();

            this.DataContext = this;

            this.Caption = title;
            this.Message = message;

            //this.tb_title.Text = title;
            //this.tb_msg.Text = message;

            OnlyOk(isOnlyOk);
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
        /// <summary>
        /// 弹出普通的提示框
        /// </summary>
        /// <param name="owner">弹出框的父窗体，如果不需要可以传null</param>
        /// <param name="message">要显示的消息</param>
        /// <param name="caption">提示框的标题</param>
        /// <param name="isConfirm">是否需要确认</param>
        /// <returns></returns>
        public static bool? Show(Window owner, string message, string caption, bool isConfirm)
        {
            Sys_MessageBox sys_box;
            if (isConfirm)
            {
                sys_box = new Sys_MessageBox(caption, message, null, false);
            }
            else
            {
                sys_box = new Sys_MessageBox(caption, message, null, true);
            }
            sys_box.Width = 376;
            sys_box.Height = 168;
            if (owner != null)
            {
                sys_box.Owner = owner;
                sys_box.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }
            else
            {
                sys_box.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
            sys_box.ShowInTaskbar = false;
            return sys_box.ShowDialog();
        }
        /// <summary>
        /// 弹出成功提示框
        /// </summary>
        /// <param name="owner">弹出框的父窗体，如果不需要可以传null</param>
        /// <param name="message">要显示的消息</param>
        /// <param name="caption">提示框的标题</param>
        /// <param name="isConfirm">是否需要确认</param>
        /// <returns></returns>
        public static bool? ShowSuccess(Window owner, string message, string caption, bool isConfirm)
        {
            Sys_MessageBox sys_box;
            if (isConfirm)
            {
                sys_box = new Sys_MessageBox(caption, message, true, false);
            }
            else
            {
                sys_box = new Sys_MessageBox(caption, message, true, true);
            }
            sys_box.Width = 376;
            sys_box.Height = 168;
            if (owner != null)
            {
                sys_box.Owner = owner;
                sys_box.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }
            else
            {
                sys_box.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
            sys_box.ShowInTaskbar = false;
            return sys_box.ShowDialog();
        }
        /// <summary>
        /// 弹出失败提示框
        /// </summary>
        /// <param name="owner">弹出框的父窗体，如果不需要可以传null</param>
        /// <param name="message">要显示的消息</param>
        /// <param name="caption">提示框的标题</param>
        /// <param name="isConfirm">是否需要确认</param>
        /// <returns></returns>
        public static bool? ShowError(Window owner, string message, string caption, bool isConfirm)
        {
            Sys_MessageBox sys_box;
            if (isConfirm)
            {
                sys_box = new Sys_MessageBox(caption, message, true, false);
            }
            else
            {
                sys_box = new Sys_MessageBox(caption, message, true, true);
            }
            sys_box.Width = 376;
            sys_box.Height = 168;
            if (owner != null)
            {
                sys_box.Owner = owner;
                sys_box.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }
            else
            {
                sys_box.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
            sys_box.ShowInTaskbar = false;
            return sys_box.ShowDialog();
        }
        #endregion

        private const double MSG_NOPIC_WIDTH = 350;
        private const double MSG_PIC_WIDTH = 290;
        private const double MSG_NOPIC_LEFT = 16;
        private const double MSG_PIC_LEFT = 76;

        private Thickness BT_OK_MARGIN = new Thickness(244, 7, 76, 8);

        private void bt_ok_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void bt_cancle_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void bt_close_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
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
