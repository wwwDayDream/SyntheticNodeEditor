using System.Windows.Input;
using Nodify;

namespace NodeEditor.Models;

public class PendingConnectionViewModel {
    private readonly EditorViewModel _editor;
    private ConnectorViewModel _source;

    public PendingConnectionViewModel(EditorViewModel editor)
    {
        _editor = editor;
        StartCommand = new DelegateCommand<ConnectorViewModel>(model => _source = model);
        FinishCommand = new DelegateCommand<ConnectorViewModel>(target =>
        {
            if (target != null)
                _editor.Connect(_source, target);
        });
    }

    public ICommand StartCommand { get; }
    public ICommand FinishCommand { get; }
}