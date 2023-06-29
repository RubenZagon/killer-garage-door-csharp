using static Killer.Garage.Door.Constants;

namespace Killer.Garage.Door.States;

public class Pause : State
{
    private readonly GarageDoor _garageDoor;

    public Pause(GarageDoor garageDoor)
    {
        _garageDoor = garageDoor;
    }
    
    public char ProcessEvent(char @event) // En este enfoque me chirria que tenga que conocer tanto el método Process Event...
    {
        var isButtonPressed = @event == 'P'; // TODO: Se debería extraer a un método privado ya que se comparte en 3 sitios diferentes ? o State debería ser una clase abstracta ?
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