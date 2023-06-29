using Killer.Garage.Door.States;

namespace Killer.Garage.Door;

public class GarageDoor
{
    private State state;
    public int position;
    public State direction;

    public GarageDoor()
    {
        direction = new Pause(this);
        position = Constants.FullyClosed;
        state = new Pause(this);
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
                .Select(@event => state.ProcessEvent(@event))
                .ToArray()
        );
    }

}