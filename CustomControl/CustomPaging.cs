using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace CustomControl
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfWorkTest.customcontrol"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfWorkTest.customcontrol;assembly=WpfWorkTest.customcontrol"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误: 
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:CustomPaging/>
    ///
    /// </summary>
    public class CustomPaging : Control
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                return (int)GetValue(PageCountProperty);
            }
            set
            {
                SetValue(PageCountProperty, value);
                this.SetPageCount(value);
            }
        }

        /// <summary>
        /// 当前页数
        /// </summary>
        public int PageIndex
        {
            get
            {
                return (int)GetValue(PageIndexProperty);
            }
            private set
            {
                SetValue(PageIndexProperty, value);
            }
        }

        /// <summary>
        /// 当有翻页动作时，会触发此事件
        /// </summary>
        [Category("Behavior")]
        public event RoutedEventHandler OnPaging;

        #region 依赖属性
        /// <summary>
        /// 自定义依赖属性
        /// </summary>
        [CategoryAttribute("自定义属性"), DescriptionAttribute("总页数")]
        public static readonly DependencyProperty PageCountProperty = DependencyProperty.Register("PageCount", typeof(int), typeof(CustomPaging), new PropertyMetadata(-1, new PropertyChangedCallback(OnPageCountChanged)));
        /// <summary>
        /// 当自定义属性的值发生变化时，触发此事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnPageCountChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                CustomPaging cp = sender as CustomPaging;

                int newCount = (int)e.NewValue;
                if (cp != null)
                {
                    cp.PageCount = newCount;
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 自定义依赖属性
        /// </summary>
        [CategoryAttribute("自定义属性"), DescriptionAttribute("当前选中页数")]
        public static readonly DependencyProperty PageIndexProperty = DependencyProperty.Register("PageIndex", typeof(int), typeof(CustomPaging), new PropertyMetadata(-1));
        #endregion

        #region 界面存放的元素控件对象
        /// <summary>
        /// 首页
        /// </summary>
        private Button bt_first = null;
        /// <summary>
        /// 上一页
        /// </summary>
        private Button bt_pre = null;
        /// <summary>
        /// 第一个页面按钮
        /// </summary>
        private Button bt_1 = null;
        /// <summary>
        /// 第二个页面按钮
        /// </summary>
        private Button bt_2 = null;
        /// <summary>
        /// 第三个页面按钮
        /// </summary>
        private Button bt_3 = null;
        /// <summary>
        /// 省略号
        /// </summary>
        private Label lb_1 = null;
        /// <summary>
        /// 下一页
        /// </summary>
        private Button bt_next = null;
        /// <summary>
        /// 最后一页
        /// </summary>
        private Button bt_end = null;
        /// <summary>
        /// 上一次选中的数字按钮
        /// </summary>
        private Button preSelNumButton = null;
        #endregion
        /// <summary>
        /// 控件名字集合
        /// </summary>
        private string[] cNames = { "bt_first", "bt_pre", "bt_1", "bt_2", "bt_3", "lb_1", "bt_next", "bt_end" };
        /// <summary>
        /// 控件的column属性集合
        /// </summary>
        private readonly int[] cColumns = new int[8];

        private Brush cSelect_bg = new SolidColorBrush(Color.FromArgb(255, 39, 117, 180));
        private Brush cNoSelect_word = new SolidColorBrush(Color.FromArgb(255, 39, 117, 180));
        private Brush color_white = new SolidColorBrush(Colors.White);

        static CustomPaging()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomPaging), new FrameworkPropertyMetadata(typeof(CustomPaging)));
        }

        #region Override
        /// <summary>
        /// 缓存控件中所有元素控件到对象
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            bt_first = GetTemplateChild(cNames[0]) as Button;
            bt_pre = GetTemplateChild(cNames[1]) as Button;
            bt_1 = GetTemplateChild(cNames[2]) as Button;
            bt_2 = GetTemplateChild(cNames[3]) as Button;
            bt_3 = GetTemplateChild(cNames[4]) as Button;
            lb_1 = GetTemplateChild(cNames[5]) as Label;
            bt_next = GetTemplateChild(cNames[6]) as Button;
            bt_end = GetTemplateChild(cNames[7]) as Button;

            if (bt_first == null || bt_pre == null || bt_1 == null || bt_2 == null || bt_3 == null || lb_1 == null || bt_next == null || bt_end == null)
            {
                throw new ArgumentNullException("自定义翻页控件，内部元素初始化失败");
            }

            bt_first.Click += bt_Page_Click;
            bt_pre.Click += bt_Page_Click;
            bt_1.Click += bt_Page_Click;
            bt_2.Click += bt_Page_Click;
            bt_3.Click += bt_Page_Click;
            bt_next.Click += bt_Page_Click;
            bt_end.Click += bt_Page_Click;

            cColumns[0] = (int)bt_first.GetValue(Grid.ColumnProperty);
            cColumns[1] = (int)bt_pre.GetValue(Grid.ColumnProperty);
            cColumns[2] = (int)bt_1.GetValue(Grid.ColumnProperty);
            cColumns[3] = (int)bt_2.GetValue(Grid.ColumnProperty);
            cColumns[4] = (int)bt_3.GetValue(Grid.ColumnProperty);
            cColumns[5] = (int)lb_1.GetValue(Grid.ColumnProperty);
            cColumns[6] = (int)bt_next.GetValue(Grid.ColumnProperty);
            cColumns[7] = (int)bt_end.GetValue(Grid.ColumnProperty);

            this.bt_first.IsEnabled = false;
            this.bt_pre.IsEnabled = false;
            this.bt_1.IsEnabled = false;
            this.bt_2.IsEnabled = false;
            this.bt_3.IsEnabled = false;
            this.bt_next.IsEnabled = false;
            this.bt_end.IsEnabled = false;
        }

        /// <summary>
        /// 响应界面按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_Page_Click(object sender, RoutedEventArgs e)
        {
            Button bt = sender as Button;
            if (bt == null)
            {
                PagingEventArgs pea = new PagingEventArgs() { pageIndex = -1, ex = new ArgumentNullException("未找到点击按钮的对象") };
                OnPaging(this, pea);
                return;
            }

            /*判断点击了哪个按钮,点击数字按钮，不需要移动控件，点击翻页按钮，更换数字（可能涉及控件的移动）*/
            string name = bt.Name;
            switch (name)
            {
                case "bt_first":
                    {//点击了首页，此时默认选中了第一页
                        if (PageIndex <= 1)
                            return;

                        this.bt_1.Content = 1;
                        this.bt_1.Background = cSelect_bg;
                        this.bt_1.Foreground = color_white;
                        this.preSelNumButton = this.bt_1;

                        this.bt_2.Content = 2;
                        this.bt_2.Background = color_white;
                        this.bt_2.Foreground = cNoSelect_word;

                        this.bt_3.Content = 3;
                        this.bt_3.Background = color_white;
                        this.bt_3.Foreground = cNoSelect_word;

                        MoveToEnd();

                        this.PageIndex = 1;
                        PagingEventArgs pea = new PagingEventArgs() { pageIndex = this.PageIndex };
                        if (OnPaging != null)
                        {
                            OnPaging(this, pea);
                        }
                        return;
                    }
                case "bt_pre":
                    {
                        if (PageIndex <= 1)
                            return;

                        ClickBtPre();
                        return;
                    }
                case "bt_1":
                    {
                        ClickBt_1();

                        return;
                    }
                case "bt_2":
                    {
                        ClickBt_2();

                        return;
                    }
                case "bt_3":
                    {
                        ClickBt_3();

                        return;
                    }
                case "bt_next":
                    {
                        if (PageIndex >= this.PageCount)
                            return;

                        ClickBtNext();
                        return;
                    }
                case "bt_end":
                    {
                        if (PageIndex >= this.PageCount)
                            return;

                        /*末尾时考虑只有2页的情况，此时默认选中的是2不是3*/
                        this.PageIndex = this.PageCount;
                        if (this.PageIndex == 2)
                        {
                            this.bt_2.Content = this.PageIndex;
                            this.bt_2.Background = cSelect_bg;
                            this.bt_2.Foreground = color_white;
                            this.preSelNumButton = this.bt_2;

                            this.bt_1.Content = this.PageIndex - 1;
                            this.bt_1.Background = color_white;
                            this.bt_1.Foreground = cNoSelect_word;
                        }
                        else
                        {
                            this.bt_3.Content = this.PageIndex;
                            this.bt_3.Background = cSelect_bg;
                            this.bt_3.Foreground = color_white;
                            this.preSelNumButton = this.bt_3;

                            this.bt_1.Content = this.PageIndex - 2;
                            this.bt_1.Background = color_white;
                            this.bt_1.Foreground = cNoSelect_word;

                            this.bt_2.Content = this.PageIndex - 1;
                            this.bt_2.Background = color_white;
                            this.bt_2.Foreground = cNoSelect_word;
                        }

                        MoveToTop();

                        PagingEventArgs pea = new PagingEventArgs() { pageIndex = this.PageIndex };
                        if (OnPaging != null)
                        {
                            OnPaging(this, pea);
                        }

                        return;
                    }
                default:
                    {
                        PagingEventArgs pea = new PagingEventArgs() { pageIndex = -1, ex = new Exception("未知的点击对象，Name: " + name) };
                        OnPaging(this, pea);
                        break;
                    }
            }
        }
        /// <summary>
        /// 点击上一页
        /// 如果上一次点的不是bt_1
        /// </summary>
        private void ClickBtPre()
        {
            if (this.preSelNumButton == null)
                return;

            if (this.preSelNumButton == this.bt_1)
            {
                if (this.PageIndex <= 1)
                    return;

                this.PageIndex--;

                this.bt_1.Content = this.PageIndex;
                this.bt_1.Background = cSelect_bg;
                this.bt_1.Foreground = color_white;
                this.preSelNumButton = this.bt_1;

                this.bt_2.Content = this.PageIndex + 1;
                this.bt_2.Background = color_white;
                this.bt_2.Foreground = cNoSelect_word;

                this.bt_3.Content = this.PageIndex + 2;
                this.bt_3.Background = color_white;
                this.bt_3.Foreground = cNoSelect_word;

                PagingEventArgs pea = new PagingEventArgs() { pageIndex = this.PageIndex };
                if (OnPaging != null)
                {
                    OnPaging(this, pea);
                }
                return;
            }
            if (this.preSelNumButton == this.bt_2)
            {
                ClickBt_1();
                return;
            }
            if (this.preSelNumButton == this.bt_3)
            {
                ClickBt_2();
                return;
            }
        }

        private void ClickBtNext()
        {
            if (this.preSelNumButton == null)
                return;

            if (this.preSelNumButton == this.bt_1)
            {
                ClickBt_2();
                return;
            }
            if (this.preSelNumButton == this.bt_2)
            {
                ClickBt_3();
                return;
            }
            if (this.preSelNumButton == this.bt_3)
            {
                if (this.PageIndex >= this.PageCount)
                    return;

                this.PageIndex++;

                this.bt_3.Content = this.PageIndex;
                this.bt_3.Background = cSelect_bg;
                this.bt_3.Foreground = color_white;
                this.preSelNumButton = this.bt_3;

                this.bt_1.Content = this.PageIndex - 2;
                this.bt_1.Background = color_white;
                this.bt_1.Foreground = cNoSelect_word;

                this.bt_2.Content = this.PageIndex - 1;
                this.bt_2.Background = color_white;
                this.bt_2.Foreground = cNoSelect_word;

                PagingEventArgs pea = new PagingEventArgs() { pageIndex = this.PageIndex };
                if (OnPaging != null)
                {
                    OnPaging(this, pea);
                }
                return;
            }
        }

        private void ClickBt_1()
        {
            if (this.preSelNumButton == this.bt_1)
                return;

            this.PageIndex = int.Parse(this.bt_1.Content.ToString());

            /*前面没有了*/
            if (this.PageIndex <= 1)
            {
                this.bt_1.Background = cSelect_bg;
                this.bt_1.Foreground = color_white;
                this.preSelNumButton = this.bt_1;

                this.bt_2.Background = color_white;
                this.bt_2.Foreground = cNoSelect_word;

                this.bt_3.Background = color_white;
                this.bt_3.Foreground = cNoSelect_word;
            }
            else
            {
                this.bt_2.Content = this.PageIndex;
                this.bt_2.Background = cSelect_bg;
                this.bt_2.Foreground = color_white;
                this.preSelNumButton = this.bt_2;

                this.bt_1.Content = this.PageIndex - 1;
                this.bt_1.Background = color_white;
                this.bt_1.Foreground = cNoSelect_word;

                this.bt_3.Content = this.PageIndex + 1;
                this.bt_3.Background = color_white;
                this.bt_3.Foreground = cNoSelect_word;
            }

            if ((this.PageIndex + 1) < this.PageCount)
            {
                MoveToEnd();
            }
            PagingEventArgs pea = new PagingEventArgs() { pageIndex = this.PageIndex };
            if (OnPaging != null)
            {
                OnPaging(this, pea);
            }
        }

        private void ClickBt_2()
        {
            if (this.preSelNumButton == this.bt_2)
                return;

            this.bt_2.Background = cSelect_bg;
            this.bt_2.Foreground = color_white;
            this.preSelNumButton = this.bt_2;

            this.bt_1.Background = color_white;
            this.bt_1.Foreground = cNoSelect_word;

            this.bt_3.Background = color_white;
            this.bt_3.Foreground = cNoSelect_word;

            this.PageIndex = int.Parse(this.bt_2.Content.ToString());
            PagingEventArgs pea = new PagingEventArgs() { pageIndex = this.PageIndex };
            if (OnPaging != null)
            {
                OnPaging(this, pea);
            }
        }

        /// <summary>
        /// 点击3的时候，判断还有没有下一页，
        /// 如果有
        /// </summary>
        private void ClickBt_3()
        {
            if (this.preSelNumButton == this.bt_3)
                return;

            this.PageIndex = int.Parse(this.bt_3.Content.ToString());

            if (this.PageIndex >= this.PageCount)
            {
                this.bt_3.Background = cSelect_bg;
                this.bt_3.Foreground = color_white;
                this.preSelNumButton = this.bt_3;

                this.bt_1.Background = color_white;
                this.bt_1.Foreground = cNoSelect_word;

                this.bt_2.Background = color_white;
                this.bt_2.Foreground = cNoSelect_word;
            }
            else
            {
                this.bt_2.Content = this.PageIndex;
                this.bt_2.Background = cSelect_bg;
                this.bt_2.Foreground = color_white;
                this.preSelNumButton = this.bt_2;

                this.bt_1.Content = this.PageIndex - 1;
                this.bt_1.Background = color_white;
                this.bt_1.Foreground = cNoSelect_word;

                this.bt_3.Content = this.PageIndex + 1;
                this.bt_3.Background = color_white;
                this.bt_3.Foreground = cNoSelect_word;
            }

            if (this.PageIndex >= this.PageCount - 1 && this.PageCount > 3)
            {
                MoveToTop();
            }
            PagingEventArgs pea = new PagingEventArgs() { pageIndex = this.PageIndex };
            if (OnPaging != null)
            {
                OnPaging(this, pea);
            }
        }

        /// <summary>
        /// 把省略号移动到最前端
        /// </summary>
        private void MoveToTop()
        {
            if (this.PageCount > 3)
            {
                this.lb_1.SetValue(Grid.ColumnProperty, cColumns[2]);
                this.bt_1.SetValue(Grid.ColumnProperty, cColumns[3]);
                this.bt_2.SetValue(Grid.ColumnProperty, cColumns[4]);
                this.bt_3.SetValue(Grid.ColumnProperty, cColumns[5]);
            }
        }
        /// <summary>
        /// 把省略号移动到最后端
        /// </summary>
        private void MoveToEnd()
        {
            if (this.PageCount > 3)
            {
                this.bt_1.SetValue(Grid.ColumnProperty, cColumns[2]);
                this.bt_2.SetValue(Grid.ColumnProperty, cColumns[3]);
                this.bt_3.SetValue(Grid.ColumnProperty, cColumns[4]);
                this.lb_1.SetValue(Grid.ColumnProperty, cColumns[5]);
            }
        }
        #endregion

        /// <summary>
        /// 根据总页数，设置控件位置
        /// </summary>
        /// <param name="count"></param>
        private void SetPageCount(int count)
        {
            if (count < 0)
            {
                return;
            }

            if (count == 1)
            {
                this.bt_first.SetValue(Grid.ColumnProperty, cColumns[0]);
                this.bt_pre.SetValue(Grid.ColumnProperty, cColumns[1]);
                this.bt_1.SetValue(Grid.ColumnProperty, cColumns[2]);
                this.bt_1.Visibility = System.Windows.Visibility.Visible;
                this.bt_2.Visibility = System.Windows.Visibility.Hidden;
                this.bt_2.Visibility = System.Windows.Visibility.Visible;
                this.bt_3.Visibility = System.Windows.Visibility.Hidden;
                this.lb_1.Visibility = System.Windows.Visibility.Hidden;
                this.bt_next.SetValue(Grid.ColumnProperty, cColumns[3]);
                this.bt_end.SetValue(Grid.ColumnProperty, cColumns[4]);
            }
            else if (count == 2)
            {
                this.bt_first.SetValue(Grid.ColumnProperty, cColumns[0]);
                this.bt_pre.SetValue(Grid.ColumnProperty, cColumns[1]);
                this.bt_1.SetValue(Grid.ColumnProperty, cColumns[2]);
                this.bt_1.Visibility = System.Windows.Visibility.Visible;
                this.bt_2.SetValue(Grid.ColumnProperty, cColumns[3]);
                this.bt_2.Visibility = System.Windows.Visibility.Visible;
                this.bt_3.Visibility = System.Windows.Visibility.Hidden;
                this.lb_1.Visibility = System.Windows.Visibility.Hidden;
                this.bt_next.SetValue(Grid.ColumnProperty, cColumns[4]);
                this.bt_end.SetValue(Grid.ColumnProperty, cColumns[5]);
            }
            else if (count == 3)
            {
                this.bt_first.SetValue(Grid.ColumnProperty, cColumns[0]);
                this.bt_pre.SetValue(Grid.ColumnProperty, cColumns[1]);
                this.bt_1.SetValue(Grid.ColumnProperty, cColumns[2]);
                this.bt_1.Visibility = System.Windows.Visibility.Visible;
                this.bt_2.SetValue(Grid.ColumnProperty, cColumns[3]);
                this.bt_2.Visibility = System.Windows.Visibility.Visible;
                this.bt_3.SetValue(Grid.ColumnProperty, cColumns[4]);
                this.bt_3.Visibility = System.Windows.Visibility.Visible;
                this.lb_1.SetValue(Grid.ColumnProperty, cColumns[5]);
                this.lb_1.Visibility = System.Windows.Visibility.Hidden;
                this.bt_next.SetValue(Grid.ColumnProperty, cColumns[5]);
                this.bt_end.SetValue(Grid.ColumnProperty, cColumns[6]);
            }
            else if (count > 3)
            {
                this.bt_first.SetValue(Grid.ColumnProperty, cColumns[0]);
                this.bt_pre.SetValue(Grid.ColumnProperty, cColumns[1]);
                this.bt_1.SetValue(Grid.ColumnProperty, cColumns[2]);
                this.bt_1.Visibility = System.Windows.Visibility.Visible;
                this.bt_2.SetValue(Grid.ColumnProperty, cColumns[3]);
                this.bt_2.Visibility = System.Windows.Visibility.Visible;
                this.bt_3.SetValue(Grid.ColumnProperty, cColumns[4]);
                this.bt_3.Visibility = System.Windows.Visibility.Visible;
                this.lb_1.SetValue(Grid.ColumnProperty, cColumns[5]);
                this.lb_1.Visibility = System.Windows.Visibility.Visible;
                this.bt_next.SetValue(Grid.ColumnProperty, cColumns[6]);
                this.bt_end.SetValue(Grid.ColumnProperty, cColumns[7]);
            }

            this.bt_first.IsEnabled = true;
            this.bt_pre.IsEnabled = true;
            this.bt_1.IsEnabled = true;
            this.bt_2.IsEnabled = true;
            this.bt_3.IsEnabled = true;
            this.bt_next.IsEnabled = true;
            this.bt_end.IsEnabled = true;

            /*默认选中第一页*/
            this.bt_1.Content = 1;
            this.bt_1.Background = cSelect_bg;
            this.bt_1.Foreground = color_white;
            this.preSelNumButton = this.bt_1;

            this.bt_2.Content = 2;
            this.bt_2.Background = color_white;
            this.bt_2.Foreground = cNoSelect_word;

            this.bt_3.Content = 3;
            this.bt_3.Background = color_white;
            this.bt_3.Foreground = cNoSelect_word;
            this.PageIndex = 1;
        }
    }

    public delegate void PagingEventHandler(PagingEventArgs e);

    public class PagingEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// 当前翻到的页数
        /// </summary>
        public int pageIndex { get; set; }
        /// <summary>
        /// 当点击的时候控件内部产生异常时，异常放在此属性里
        /// </summary>
        public Exception ex { get; set; }
    }
}
