namespace TestProject1;

public class Door
{
    private const int FullyOpened = 5;
    private const int FullyClosed = 0;
    private int _position = 0;
    private bool _isOpening = false;
    private bool _pause =false;

    public string ProcessEvents(string events)
    {
        return ProcessEvents(events, true);
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


    public string ProcessEvents(string events, bool new_way)
    {
        return new string(
            events.ToCharArray()
                .Select(@event =>
                {
                    if ((@event == 'P' && _position == FullyOpened) || (@event == 'P' && _position == FullyClosed))
                    {
                        _isOpening = !_isOpening;
                    }

                    if ((@event == 'P' && _position > FullyClosed) || (@event == 'P' && _position < FullyOpened))
                    {
                        _pause = true;
                    }

                    if (_pause)
                    {
                        return _position.ToString()[0];
                    }

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