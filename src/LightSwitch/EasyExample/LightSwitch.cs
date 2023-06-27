using EasyExample.States;

namespace EasyExample;

public class LightSwitch
{
    private State state;

    public LightSwitch()
    {
        state = new Off(this);
    }
    
    public void ChangeState(State state)
    {
        this.state = state;
    }

    public void Press()
    {
        this.state.Press();
        Console.WriteLine(Description());
    }

    private string Description()
    {
        return this.state.Description();
    }
}