//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Windows.Input;

//namespace ChessUI.Command
//{
//    class Command : ICommand
//    {
//        private Action action;
//        private Action<object> parameterizedAction;

//        private bool canExecute = false;
//        public bool CanExecute
//        {
//            get { return canExecute; }
//            set
//            {
//                if (canExecute != value)
//                {
//                    canExecute = value;
//                    EventHandler canExecuteChanged = CanExecuteChanged;
//                    if (canExecuteChanged != null)
//                        canExecuteChanged(this, EventArgs.Empty);
//                }
//            }
//        }

//        public event EventHandler CanExecuteChanged;

//        public Command(Action action, bool canExecute = true)
//        {
//            this.action = action;
//            this.canExecute = canExecute;
//        }

//        public Command(Action<object> parameterizedAction, bool canExecute = true)
//        {
//            this.parameterizedAction = parameterizedAction;
//            this.canExecute = canExecute;
//        }

//        void ICommand.Execute(object parameter)
//        {
//            this.DoExecute(parameter);
//        }

//        bool ICommand.CanExecute(object parameter)
//        {
//            return CanExecute;
//        }

//        protected void InvokeAction(object param)
//        {
//            Action theAction = action;
//            Action<object> theParameterizedAction = parameterizedAction;
//            if (theAction != null)
//                theAction();
//            else if (theParameterizedAction != null)
//                theParameterizedAction(param);
//        }

//        protected void InvokeExecuted
//    }
//}
