using static Killer.Garage.Door.Constants;

namespace Killer.Garage.Door.States;

public class Opening : State
{
    private readonly GarageDoor _garageDoor;

    public Opening(GarageDoor garageDoor)
    {
        _garageDoor = garageDoor;
    }

    public Opening() { }

    public void Handle(GarageDoor garageDoor, char @event)
    {
        var isButtonPressed = @event == 'P';
        if (isButtonPressed || garageDoor.position == FullyOpened)
        {
            garageDoor.ChangeState(new Pause());
        }
        else
        {
            var isObstacleDetected = @event == 'O';
            if (isObstacleDetected)
            {
                garageDoor.ChangeState(new Closing());
                garageDoor.lastDirection = new Closing();
            }
            else
            {
                garageDoor.ChangeState(new Opening());
                garageDoor.lastDirection = new Opening();
            }
        }
    }
    
    public int ProcessEvent(int position, char @event)
    {
        return position + 1;
    }
    
    public char ProcessEvent(char @event)
    {
        _garageDoor.lastDirection = new Opening(_garageDoor);
        var isButtonPressed = @event == 'P';
        if (isButtonPressed || _garageDoor.position == FullyOpened)
        {
            _garageDoor.ChangeState(new Pause(_garageDoor));
            return _garageDoor.position.ToString()[0];
        }
        return (_garageDoor.position += 1).ToString()[0];
    }
}