using static Killer.Garage.Door.Constants;

namespace Killer.Garage.Door.States;

public class Closing : State
{
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
        throw new NotImplementedException();
    }

    public int ProcessEvent(int position)
    {
        return position - 1;
    }
}