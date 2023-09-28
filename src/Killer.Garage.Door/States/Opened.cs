namespace Killer.Garage.Door.States;

public class Opened :State
{
    private GarageDoor _garageDoor;

    public Opened(GarageDoor garageDoor)
    {
        _garageDoor = garageDoor;
    }
    public string ProcessEvent(string events)
    {
        var processed = "5";
        if (events.Length == 1)
        {
            return processed;
        }

        if (events[0] == 'P')
        {
            _garageDoor.ChangeState(new Closing(_garageDoor));
            return _garageDoor.ProcessEvents(events);
        }

        return processed + _garageDoor.ProcessEvents(events[1..]);
    }
}