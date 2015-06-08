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

namespace WpfWorkTest.viewModel
{
    public class ViewTest : BindableBase, IInteractionRequestAware
    {
        public string PageCount { get; set; }

        private readonly DelegateCommand btCommand;

        public ViewTest()
        {
            btCommand = new DelegateCommand(new Action(Do));
        }

        public ICommand ButtonCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    MessageBox.Show("Button");
                });
            }
        }

        public void Do()
        {
            string s = "";
        }

        Action IInteractionRequestAware.FinishInteraction
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        INotification IInteractionRequestAware.Notification
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
