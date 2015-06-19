using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DataProvider.Centaline;

namespace DataProvider
{
    public class InteractiveData : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Gatherer gatherer = new Gatherer();
            gatherer.Start("http://www.statelyhome.com.hk/en/");
        }

        public event EventHandler CanExecuteChanged;
    }
}
