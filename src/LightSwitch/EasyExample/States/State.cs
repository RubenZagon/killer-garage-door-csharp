namespace EasyExample.States;

public interface State
{
    void Handle(LightSwitch lightSwitch);
    string Description();
}