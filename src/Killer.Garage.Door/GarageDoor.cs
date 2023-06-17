using Killer.Garage.Door.States;

namespace Killer.Garage.Door;

public class GarageDoor
{
    private State state;
    internal int position;
    internal State lastDirection;

    public GarageDoor()
    {
        lastDirection = new Pause();
        position = Constants.FullyClosed;
        state = new Pause();
    }

    public void ChangeState(State state)
    {
        this.state = state;
    }

    public string ProcessEvents(string events)
    {
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
}