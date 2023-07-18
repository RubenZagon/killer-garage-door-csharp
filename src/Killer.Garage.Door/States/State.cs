namespace Killer.Garage.Door.States;

public interface State
{
    string ProcessEvent(string events);
}