namespace TestProject1;


public interface State
{
    char ProcessEvents(int position);
    void Handle(Door door, char @event);
}

public class Static : State
{
    private bool _buttonPressed = false;
    
    public void Handle(Door door, char @event)
    {
        if (@event == 'P')
        {
            _buttonPressed = true;
        }
        switch (_buttonPressed)
        {
            case true when door._position == 0:
                door.ChangeState(new Opening());
                break;
            case true when door._position == 5:
                door.ChangeState(new Closing());
                break;
            case true when door._position is > 0 and < 5:
                door.ChangeState(new Static());
                break;
            default:
                door.ChangeState(new Static());
                break;
        }
    }

    public char ProcessEvents(int position)
    {
        return position.ToString()[0];
    }

}

public class Opening : State
{
    private const int FullyOpened = 5;

    public void Handle(Door door, char @event)
    {
        door.ChangeState(new Static());
    }

    
    public char ProcessEvents(int position)
    {
        return position == FullyOpened 
            ? position.ToString()[0] 
            : (position + 1).ToString()[0];
    }

}

public class Closing : State
{ 
    private const int FullyClosed = 0;

    public void Handle(Door door, char @event)
    {
        door.ChangeState(new Static());
    }

    public char ProcessEvents(int position)
    {
        return position == FullyClosed 
            ? position.ToString()[0] 
            : (position - 1).ToString()[0];
    }

}


public class Door
{
    private State _state;


    internal int _position = 0;

    public Door()
    {
        _state = new Static();
    }

    public void ChangeState(State state)
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
                _state.Handle(this, @event);
                return _state.ProcessEvents(_position);
            })
            .ToArray()
        );
    }
}