using static Killer.Garage.Door.Constants;

namespace Killer.Garage.Door.States;

public class Closing : State
{
    private readonly GarageDoor _garageDoor;

    public Closing(GarageDoor _garageDoor)
    {
        this._garageDoor = _garageDoor;
    }

    public string ProcessEvent(string events)
    {
        if (events.Length == 1)
        {
            return (_garageDoor.position -= 1).ToString();
        }
        
        if (_garageDoor.position == FullyClosed)
        {
            _garageDoor.ChangeState(new Pause(_garageDoor));
            return _garageDoor.ProcessEvents(events);
        }

        return (_garageDoor.position -= 1) + ProcessEvent(events[1..]);
    }
}