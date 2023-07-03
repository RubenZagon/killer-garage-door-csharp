using static Killer.Garage.Door.Constants;

namespace Killer.Garage.Door.States;

public class Pause : State
{
    public void Handle(GarageDoor garageDoor, char @event)
    {
        var isButtonPressed = @event == 'P';
        switch (isButtonPressed)
        {
            case true when garageDoor.position == FullyClosed:
                garageDoor.ChangeState(new Opening());
                break;
            case true when garageDoor.position == FullyOpened:
                garageDoor.ChangeState(new Closing());
                break;
            case true when garageDoor.position is > FullyClosed and < FullyOpened:
                if (garageDoor.lastDirection is Opening)
                {
                    garageDoor.ChangeState(new Opening());
                }
                else
                {
                    garageDoor.ChangeState(new Closing());
                }
                break;
            default:
                garageDoor.ChangeState(new Pause());
                break;
        }
    }

    public string ProcessEvent(string events)
    {
        if (events.Length == 1)
        {
            return "0";
        }
        string restEventsToProcess = events.Substring(1);
        return "0" + this.ProcessEvent(restEventsToProcess);
    }

    public int ProcessEvent(int position)
    {
        return position;
    }
}