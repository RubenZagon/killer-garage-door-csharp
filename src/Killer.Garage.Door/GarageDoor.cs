﻿using Killer.Garage.Door.States;

namespace Killer.Garage.Door;

public class GarageDoor
{
    private State state;
    internal int position;
    internal State lastDirection;

    public GarageDoor()
    {
        lastDirection = new Pause(this);
        position = Constants.FullyClosed;
        state = new Pause(this);
    }

    public void ChangeState(State state)
    {
        this.state = state;
    }

    public string ProcessEvents(string events)
    {

        return ProcessEvents(events, true);
        // Patrón Estado
        return new string(
            events.ToCharArray()
                .Select(@event =>
                {
                    state.Handle(this, @event);
                    position = state.ProcessEvent(position);
                    return position.ToString()[0];
                })
                .ToArray()
        );
    }
    
    public string ProcessEvents(string events, bool new_way)
    {
        // Patrón Estado
        return state.ProcessEvent(events);
    }

}