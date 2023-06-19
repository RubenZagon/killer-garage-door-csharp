using EasyExample.States;

namespace EasyExample;

public class LightSwitch
{
    private State state;

    public LightSwitch()
    {
        state = new Off();
    }

    public void ChangeState(State state)
    {
        this.state = state;
    }

    public void Press()
    {
        state.Handle(this);
        Console.WriteLine(state.Description());
    }
}