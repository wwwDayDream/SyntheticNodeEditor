namespace NodeEditor.Models;

public class ConnectionViewModel
{
	public ConnectionViewModel()
	{
	}

	public ConnectionViewModel(ConnectorViewModel source, ConnectorViewModel target)
    {
        Source = source;
        Target = target;

        Source.IsConnected = true;
        Target.IsConnected = true;
    }

    public ConnectorViewModel Source { get; set; }
    public ConnectorViewModel Target { get; set; }
}