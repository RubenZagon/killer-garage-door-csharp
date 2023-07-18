using static Killer.Garage.Door.Constants;

namespace Killer.Garage.Door.States;

public class Pause : State
{
    private readonly GarageDoor _garageDoor;

    public Pause(GarageDoor _garageDoor)
    {
        this._garageDoor = _garageDoor;
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

        if (isButtonPressed && _garageDoor.position < FullyOpened)
        {
            _garageDoor.ChangeState(new Opening(_garageDoor));
            return _garageDoor.ProcessEvents(events[1..]);
        }

        if (events.Length == 1)
        {
            return _garageDoor.position.ToString();
        }

        return _garageDoor.position + ProcessEvent(events[1..]);
    }
}