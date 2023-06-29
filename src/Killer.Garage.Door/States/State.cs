namespace Killer.Garage.Door.States;

public interface State
{
    char ProcessEvent(char @event);
}