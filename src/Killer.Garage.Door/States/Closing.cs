using static Killer.Garage.Door.Constants;

namespace Killer.Garage.Door.States;

public class Closing : State
{
    private readonly GarageDoor _garageDoor;

    public Closing(GarageDoor garageDoor)
    {
        _garageDoor = garageDoor;
    }

    public char ProcessEvent(char @event)
    {
        _garageDoor.direction = new Closing(_garageDoor);
        var isButtonPressed = @event == 'P';
        if (isButtonPressed || _garageDoor.position == FullyClosed)
        {
            return PauseCommand();
        }
        var isObstacleDetected = @event == 'O';
        return isObstacleDetected 
            ? OpeningCommand() 
            : ClosingCommand();
    }

    private char PauseCommand()
    {
        _garageDoor.ChangeState(new Pause(_garageDoor));
        return _garageDoor.position.ToString()[0];
    }

    private char OpeningCommand()
    {
        _garageDoor.ChangeState(new Opening(_garageDoor));
        _garageDoor.direction = new Opening(_garageDoor);
        return (_garageDoor.position += 1).ToString()[0];
    }
    
    private char ClosingCommand()
    {
        _garageDoor.ChangeState(new Closing(_garageDoor));
        _garageDoor.direction = new Closing(_garageDoor);
        return (_garageDoor.position -= 1).ToString()[0];
    }

}