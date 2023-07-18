using Killer.Garage.Door.States;

namespace Killer.Garage.Door;

enum Direction
{
    TO_OPEN = 1,
    TO_CLOSE = 2,
}

public class GarageDoor
{
    private State state;
    internal int position;

    public GarageDoor()
    {
        state = new Closed(this);
    }

    public void ChangeState(State state)
    {
        this.state = state;
    }

    public string ProcessEvents(string events)
    {
        return state.ProcessEvent(events);
    }
}