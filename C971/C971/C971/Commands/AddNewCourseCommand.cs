using C971.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace C971.Commands
{
    public class AddNewCourseCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            var viewModel = parameter as TermDetailsViewModel;
            viewModel.AddNewCourse();
        }
    }
}
