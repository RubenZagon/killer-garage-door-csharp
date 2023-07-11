using static Killer.Garage.Door.Constants;

namespace Killer.Garage.Door.States;

public class Pause : State
{
    private readonly GarageDoor _garageDoor;

    public Pause(GarageDoor _garageDoor)
    {
        this._garageDoor = _garageDoor;
    }

    public Pause()
    {
    }

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

    public string ProcessEvent(string events)
    {
        var isButtonPressed = events[0] == 'P';
        if (isButtonPressed && _garageDoor.position == FullyClosed)
        {
            _garageDoor.ChangeState(new Opening(_garageDoor));
            return _garageDoor.ProcessEvents(events);
        }
        if (isButtonPressed && _garageDoor.position == FullyOpened)
        {
            _garageDoor.ChangeState(new Closing(_garageDoor));
            return _garageDoor.ProcessEvents(events);
        }
        
        if (events.Length == 1)
        {
            return _garageDoor.position.ToString();
        }

        return _garageDoor.position + ProcessEvent(events[1..]);
    }

    public int ProcessEvent(int position)
    {
        return position;
    }
}