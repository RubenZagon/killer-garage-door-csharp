namespace EasyExample.States;

public class Off : State
{
    private readonly LightSwitch _lightSwitch;

    public Off(LightSwitch lightSwitch)
    {
        _lightSwitch = lightSwitch;
    }

    public void Press()
    {
        _lightSwitch.ChangeState(new On(_lightSwitch));
    }
    
    public string Description()
    {
        return "The light is OFF";
    }
}