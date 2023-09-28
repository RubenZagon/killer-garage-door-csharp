﻿namespace Killer.Garage.Door.States;

public class Closed: State
{
    private GarageDoor _garageDoor;

    public Closed(GarageDoor garageDoor)
    {
        _garageDoor = garageDoor;
    }

    public string ProcessEvent(string events)
    {
        var processed = "0";
        if (events.Length == 1)
        {
            return processed;
        }

        if (events[0] == 'P')
        {
            _garageDoor.ChangeState(new Opening(_garageDoor));
            return _garageDoor.ProcessEvents(events);
        }

        return processed + _garageDoor.ProcessEvents(events[1..]);
    }
}