using System.Windows.Input;

namespace NodeEditor;
public class DelegateCommand<TType> : ICommand
{
    private readonly Predicate<TType> _canExecute;
    private readonly Action<TType> _execute;
 
    public event EventHandler CanExecuteChanged;
 
    public DelegateCommand(Action<TType> execute) 
        : this(execute, null)
    {
    }
 
    public DelegateCommand(Action<TType> execute, 
        Predicate<TType> canExecute)
    {
        _execute = execute;
        _canExecute = canExecute;
    }
 
    public bool CanExecute(object parameter)
    {
        if (_canExecute == null)
        {
            return true;
        }
 
        return _canExecute((TType)parameter);
    }
 
    public void Execute(object parameter)
    {
        _execute((TType)parameter);
    }
 
    public void RaiseCanExecuteChanged()
    {
        if( CanExecuteChanged != null )
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}
