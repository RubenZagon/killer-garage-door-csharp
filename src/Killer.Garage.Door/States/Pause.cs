using static Killer.Garage.Door.Constants;

namespace Killer.Garage.Door.States;

public class Pause : State
{
    private readonly GarageDoor _garageDoor;

    public Pause(GarageDoor garageDoor)
    {
        _garageDoor = garageDoor;
    }
    
    public Pause() { }

    public void Handle(GarageDoor garageDoor, char @event)
    {
        var isButtonPressed = @event == 'P';
        switch (isButtonPressed)
        {
            case true when garageDoor.position == FullyClosed:
                garageDoor.ChangeState(new Opening());
                break;
            case true when garageDoor.position == FullyOpened:
                garageDoor.ChangeState(new Closing());
                break;
            case true when garageDoor.position is > FullyClosed and < FullyOpened:
                if (garageDoor.lastDirection is Opening)
                {
                    garageDoor.ChangeState(new Opening());
                }
                else
                {
                    garageDoor.ChangeState(new Closing());
                }
                break;
            default:
                garageDoor.ChangeState(new Pause());
                break;
        }
    }

    public int ProcessEvent(int position, char @event)
    {
        return position;
    }
    
    public char ProcessEvent(char @event)
    {
        var isButtonPressed = @event == 'P';
        if (isButtonPressed && _garageDoor.position == FullyClosed)
        {
            _garageDoor.ChangeState(new Opening(_garageDoor));
            _garageDoor.position = FullyClosed + 1;
        }
        else if (isButtonPressed && _garageDoor.position == FullyOpened)
        {
            _garageDoor.ChangeState(new Closing(_garageDoor));
            _garageDoor.position = FullyOpened - 1;
        }
        
        
        return _garageDoor.position.ToString()[0];
    }
}