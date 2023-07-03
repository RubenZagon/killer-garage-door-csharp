using static Killer.Garage.Door.Constants;

namespace Killer.Garage.Door.States;

public class Opening : State
{
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
        throw new NotImplementedException();
    }

    public int ProcessEvent(int position)
    {
        return position + 1;
    }
}