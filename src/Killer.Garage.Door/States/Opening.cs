using static Killer.Garage.Door.Constants;

namespace Killer.Garage.Door.States;

public class Opening : State
{
    private readonly GarageDoor _garageDoor;

    public Opening(GarageDoor garageDoor)
    {
        _garageDoor = garageDoor;
    }
    
    public char ProcessEvent(char @event)
    {
        _garageDoor.direction = new Opening(_garageDoor);
        var isButtonPressed = @event == 'P';
        if (isButtonPressed || _garageDoor.position == FullyOpened)
        {
            _garageDoor.ChangeState(new Pause(_garageDoor));
            return _garageDoor.position.ToString()[0];
        }

        var isObstacleDetected = @event == 'O';
        if (isObstacleDetected)
        {
            _garageDoor.ChangeState(new Closing(_garageDoor));
            _garageDoor.direction = new Closing(_garageDoor);
            return (_garageDoor.position -= 1).ToString()[0]; // TODO: Me chirria que opening conozca el comportamiento de Closing
        }

        _garageDoor.ChangeState(new Opening(_garageDoor));
        _garageDoor.direction = new Opening(_garageDoor);
        return (_garageDoor.position += 1).ToString()[0];
    }
}