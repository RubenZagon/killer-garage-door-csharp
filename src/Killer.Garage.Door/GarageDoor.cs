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
    internal State lastDirection;
    internal Direction direction;

    public GarageDoor()
    {
        lastDirection = new Pause(this);
        position = Constants.FullyClosed;
        state = new Pause(this);
        direction = Direction.TO_OPEN;
    }

    public GarageDoor(bool new_constructor)
    {
        state = new Closed(this);
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
        string processEvents = state.ProcessEvent(events);
        return processEvents;
    }

}