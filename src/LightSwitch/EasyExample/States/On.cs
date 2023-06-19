namespace EasyExample.States;

public class On : State
{
    public void Handle(LightSwitch lightSwitch)
    {
        lightSwitch.ChangeState(new Off());
    }

    public string Description()
    {
        return "The light is ON";
    }
}