namespace EasyExample.States;

public class On : State
{
    private readonly LightSwitch _lightSwitch;

    public On(LightSwitch lightSwitch)
    {
        _lightSwitch = lightSwitch;
    }

    public void Press()
    {
        _lightSwitch.ChangeState(new Off(_lightSwitch));
    }

    public string Description()
    {
        return "The light is ON";
    }
}