namespace Killer.Garage.Door.States;

public interface State
{
    int ProcessEvent(int position, char @event);
    char ProcessEvent(char @event);
    void Handle(GarageDoor garageDoor, char @event);
}