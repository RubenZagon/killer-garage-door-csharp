namespace Killer.Garage.Door.States;

public interface State
{
    int ProcessEvents(int position);
    void Handle(GarageDoor garageDoor, char @event);
}