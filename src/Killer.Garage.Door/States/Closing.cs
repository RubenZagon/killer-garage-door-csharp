using static Killer.Garage.Door.Constants;

namespace Killer.Garage.Door.States;

public class Closing : State
{
    private readonly GarageDoor _garageDoor;

    public Closing(GarageDoor _garageDoor)
    {
        this._garageDoor = _garageDoor;
        this._garageDoor.direction = Direction.TO_CLOSE;
    }

    public Closing()
    {
    }

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

    public string ProcessEvent(string events)
    {
        if (events.Length == 1)
        {
            return (_garageDoor.position -= 1).ToString();
        }
        
        if (_garageDoor.position == FullyClosed)
        {
            _garageDoor.ChangeState(new Pause(_garageDoor));
            return _garageDoor.ProcessEvents(events);
        }

        return (_garageDoor.position -= 1) + ProcessEvent(events[1..]);
    }

    public int ProcessEvent(int position)
    {
        return position - 1;
    }
}