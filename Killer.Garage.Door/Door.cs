namespace TestProject1;


public interface DoorState
{
    public string ProcessEvents(string events, int position);
}

public class Opening : DoorState
{
    public string ProcessEvents(string events, int position)
    {
        if (position == 5)
        {
            return position.ToString()[0].ToString();
        }

        return (position + 1).ToString()[0].ToString();
    }
}

public class Closing : DoorState
{
    public string ProcessEvents(string events, int position)
    {
        if (position == 0)
        {
            return position.ToString()[0].ToString();
        }

        return (position - 1).ToString()[0].ToString();
    }
}


public class Door
{
    private DoorState _state = new Closing();
    private const int FullyOpened = 5;
    private const int FullyClosed = 0;
    private int _position = 0;
    private bool _isOpening = false;
    
    private void changeState(DoorState state)
    {
        _state = state;
    }

    public string ProcessEvents(string events)//  "...P....P" -> "[.][.][.][P][.][.][.][.][P]"
    {
        
        // Patrón Estado
        
        return new string(
            events.ToCharArray()
            .Select(@event =>
            {
                if (@event == 'P' &&  _state is Opening)
                {
                    changeState(new Closing());
                }
                else if (@event == 'P' &&  _state is Closing)
                {
                    changeState(new Opening());
                }
                // {
                //     _isOpening = !_isOpening;
                // }
                
                
                
                return _state.ProcessEvents(events, _position);
                
            })
            .ToArray()
        );
    }
    
    public string ProcessEvents(string events, bool new_way)
    {
        return new string(
            events.ToCharArray()
                .Select(@event =>
                {

                    SwitchBehaviourByBottomPressed(@event);
                    
                    if (_isOpening)
                    {
                        if (_position == FullyOpened)
                        {
                            return _position.ToString()[0];
                        }

                        _position++;
                    }
                    else
                    {
                        if (_position == FullyClosed)
                        {
                            return _position.ToString()[0];
                        }

                        _position--;
                    }
                    
                    return _position.ToString()[0];
                })
                .ToArray()
        );
    }

    

    private void SwitchBehaviourByBottomPressed(char @event)
    {
        if (@event == 'P')
        {
            _isOpening = !_isOpening;
        }
    }
}