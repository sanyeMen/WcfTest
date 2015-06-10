using CustomControl;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfWorkTest.customcontrol;

namespace WpfWorkTest.viewModel
{
    public class ViewTest : BindableBase
    {
        public string PageCount { get; set; }

        public DelegateCommand<int?> PagingCommand { get; private set; }


        public ViewTest()
        {
            PagingCommand = new DelegateCommand<int?>(Do);
        }

        public void Do(int? e)
        {
            CustomControl.Sys_MessageBox.ShowSuccess(Application.Current.Windows[0], "提示信息", "标题：", true);
        }
    }
}
