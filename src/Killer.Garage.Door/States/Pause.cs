using static Killer.Garage.Door.Constants;

namespace Killer.Garage.Door.States;

public class Pause : State
{
    private readonly GarageDoor _garageDoor;

    public Pause(GarageDoor garageDoor)
    {
        _garageDoor = garageDoor;
    }
    
    public Pause() { }

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
                if (garageDoor.direction is Opening)
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

    public int ProcessEvent(int position, char @event)
    {
        return position;
    }
    
    public char ProcessEvent(char @event) // En este enfoque me chirria que tenga que conocer tanto el método Process Event...
    {
        var isButtonPressed = @event == 'P';
        return isButtonPressed switch
        {
            true when _garageDoor.position == FullyClosed => OpeningCommand(),
            true when _garageDoor.position == FullyOpened => ClosingCommand(),
            true when _garageDoor.position is > FullyClosed and < FullyOpened => 
                _garageDoor.direction is Opening
                ? OpeningCommand()
                : ClosingCommand(),
            _ => _garageDoor.position.ToString()[0]
        };
    }

    private char OpeningCommand()
    {
        _garageDoor.ChangeState(new Opening(_garageDoor));
        _garageDoor.position += 1; // TODO: Me chirria tener que conocer la implementación o comportamiento de Opening
        return _garageDoor.position.ToString()[0];
    }

    private char ClosingCommand()
    {
        _garageDoor.ChangeState(new Closing(_garageDoor));
        _garageDoor.position -= 1;  // TODO: Me chirria tener que conocer la implementación o comportamiento de Closing
        return _garageDoor.position.ToString()[0];
    }
}