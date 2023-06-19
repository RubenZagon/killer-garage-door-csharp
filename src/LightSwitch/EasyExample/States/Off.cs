namespace EasyExample.States;

public class Off : State
{
    public void Handle(LightSwitch lightSwitch)
    {
        lightSwitch.ChangeState(new On());
    }

    public string Description()
    {
        return "The light is OFF";
    }
}