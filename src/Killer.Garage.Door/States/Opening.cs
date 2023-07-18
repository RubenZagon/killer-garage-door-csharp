using static Killer.Garage.Door.Constants;

namespace Killer.Garage.Door.States;

public class Opening : State
{
    private readonly GarageDoor _garageDoor;

    public Opening(GarageDoor _garageDoor)
    {
        this._garageDoor = _garageDoor;
    }
    
    public string ProcessEvent(string events)
    {
        if (events.Length == 1)
        {
            return (_garageDoor.position += 1).ToString();
        }
        /*var isButtonPressed = events[0] == 'P';

        if (_garageDoor.position is < FullyOpened and > FullyClosed && isButtonPressed)
        {
            _garageDoor.ChangeState(new Pause(_garageDoor));
            return _garageDoor.ProcessEvents(events);
        }

        if (_garageDoor.position == FullyOpened)
        {
            _garageDoor.ChangeState(new Pause(_garageDoor));
            return _garageDoor.ProcessEvents(events);
        }*/

        return (_garageDoor.position += 1) + ProcessEvent(events[1..]);
    }
}