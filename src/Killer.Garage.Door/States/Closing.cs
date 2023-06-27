using static Killer.Garage.Door.Constants;

namespace Killer.Garage.Door.States;

public class Closing : State
{
    private readonly GarageDoor _garageDoor;

    public Closing(GarageDoor garageDoor)
    {
        _garageDoor = garageDoor;
    }
    
    public Closing() { }

    public void Handle(GarageDoor garageDoor, char @event)
    {
        var isButtonPressed = @event == 'P';
        var isObstacleDetected = @event == 'O';
        if (isButtonPressed || garageDoor.position == FullyClosed)
        {
            garageDoor.ChangeState(new Pause());
        }
        else if (isObstacleDetected)
        {
            garageDoor.ChangeState(new Opening());
            garageDoor.lastDirection = new Opening();
        }
        else
        {
            garageDoor.ChangeState(new Closing());
            garageDoor.lastDirection = new Closing();
        }
    }

    public int ProcessEvent(int position, char @event)
    {
        return position - 1;
    }
    
    public char ProcessEvent(char @event)
    {
        if (_garageDoor.position == FullyClosed)
        {
            _garageDoor.ChangeState(new Pause(_garageDoor));
            return _garageDoor.position.ToString()[0];
        }

        return (_garageDoor.position -= 1).ToString()[0];
    }
}