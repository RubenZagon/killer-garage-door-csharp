using static Killer.Garage.Door.Constants;

namespace Killer.Garage.Door.States;

public class Opening : State
{
    private readonly GarageDoor _garageDoor;

    public Opening(GarageDoor _garageDoor)
    {
        this._garageDoor = _garageDoor;
    }

    public Opening()
    {
    }

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

    public string ProcessEvent(string events)
    {
        if (events.Length == 1)
        {
            return (_garageDoor.position += 1).ToString();
        }

        if (_garageDoor.position == FullyOpened)
        {
            _garageDoor.ChangeState(new Pause(_garageDoor));
            return _garageDoor.ProcessEvents(events);
        }

        return (_garageDoor.position += 1) + ProcessEvent(events[1..]);
    }

    public int ProcessEvent(int position)
    {
        return position + 1;
    }
}