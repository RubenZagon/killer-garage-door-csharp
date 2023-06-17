namespace Killer.Garage.Door.Unit.Tests;

public class GarageDoorShould : IDisposable
{
    private GarageDoor sut;

    public GarageDoorShould()
    {
        sut = new GarageDoor();
    }

    public void Dispose()
    {
        sut = new GarageDoor();
    }

    [Fact]
    public void No_press_the_button()
    {
        var actual = sut.ProcessEvents(".....");

        actual.Should().Be("00000");
    }

    [Fact]
    public void Press_the_button_for_2_sec()
    {
        var actual = sut.ProcessEvents("..P.");

        actual.Should().Be("0012");
    }

    #region NormalOperation

    [Fact]
    public void NormalOperation_ShouldOpenCompletelyAndStayOpen_Random()
    {
        var input = "P...." + new string(Enumerable.Repeat('.', 10).ToArray());
        var expected = "12345" + new string(Enumerable.Repeat('5', 10).ToArray());

        var actual = sut.ProcessEvents(input);

        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_Open_Completely_And_Stay_Open_Then_Close()
    {
        var actual = sut.ProcessEvents("P......P......");
        actual.Should().Be("12345554321000");
    }

    #endregion

    #region Pause

    [Fact]
    public void Pause_Should_Start_Opening_And_Pause_On_Second_Press()
    {
        var actual = sut.ProcessEvents("P.P..");
        actual.Should().Be("12222");
    }

    [Fact]
    public void Pause_Should_Resume_Opening_On_Third_Press()
    {
        var actual = sut.ProcessEvents("P.P.P....");
        actual.Should().Be("122234555");
    }
    
    #endregion

    [Fact]
    public void Obstacle_ShouldReverseWhileOpening()
    {
        var actual = sut.ProcessEvents("P.O....");
        actual.Should().Be("1210000");
    }
    
    [Fact]
    public void Obstacle_ShouldReverseWhileClosing()
    {
        var actual = sut.ProcessEvents("P......P.O....");
        actual.Should().Be("12345554345555");
    }

    [Fact]
    public void ObstacleAndPause_ShouldReverseWhileOpeningAndAllowPause()
    {
        var actual = sut.ProcessEvents("P..OP..P..");
        actual.Should().Be("1232222100");
    }

    [Fact]
    public void Example_ShouldStartOpeningAndReverseWhenObstacle()
    {
        var actual = sut.ProcessEvents("..P...O.....");
        actual.Should().Be("001234321000");
    }
}