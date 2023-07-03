namespace Killer.Garage.Door.States;

public interface State
{
    int ProcessEvent(int position);
    void Handle(GarageDoor garageDoor, char @event);
    string ProcessEvent(string events);
}