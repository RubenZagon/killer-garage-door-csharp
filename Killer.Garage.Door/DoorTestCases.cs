namespace TestProject1;

/*

- [.][.][P][.] -> [0][0][1][1]
- "..." -> "000"
- "..P...." -> "0012345"

 */

public class DoorTestCases : IDisposable
{
    private Door _objectUnderTest;

    public DoorTestCases()
    {
        _objectUnderTest = new Door();
    }

    public void Dispose()
    {
        _objectUnderTest = new Door();
    }

    [Fact]
    public void No_press_the_button()
    {
        var input = ".....";
        var actual = _objectUnderTest.ProcessEvents(input);

        actual.Should().Be("00000");
    }

    [Fact]
    public void Press_the_button_for_2_sec()
    {
        var input = "..P.";
        var actual = _objectUnderTest.ProcessEvents(input);

        actual.Should().Be("0012");
    }

    [Fact]
    public void NormalOperation_ShouldOpenCompletelyAndStayOpen_Random()
    {
        var input = "P...." + new string(Enumerable.Repeat('.', 10).ToArray());
        var expected = "12345" + new string(Enumerable.Repeat('5', 10).ToArray());

        var actual = _objectUnderTest.ProcessEvents(input);

        actual.Should().Be(expected);
    }

    [Fact]
    public void Should_Open_Completely_And_Stay_Open_Then_Close()
    {
        var actual = _objectUnderTest.ProcessEvents("P......P......");
        actual.Should().Be("12345554321000");
    }


    [Fact]
    public void Pause_ShouldStartOpeningAndPauseOnSecondPress()
    {
        var actual = _objectUnderTest.ProcessEvents("P.P..", true);
        actual.Should().Be("12222");
    }

    [Fact]
    public void Pause_ShouldResumeOpeningOnThirdPress()
    {
        var actual = _objectUnderTest.ProcessEvents("P.P.P....");
        //Assert.AreEqual("122234555", actual);
    }

    [Fact]
    public void Obstacle_ShouldReverseWhileOpening()
    {
        var actual = _objectUnderTest.ProcessEvents("P.O....");
        //Assert.AreEqual("1210000", actual);
    }

    [Fact]
    public void ObstacleAndPause_ShouldReverseWhileOpeningAndAllowPause()
    {
        var actual = _objectUnderTest.ProcessEvents("P..OP..P..");
        //Assert.AreEqual("1232222100", actual);
    }

    [Fact]
    public void Example_ShouldStartOpeningAndReverseWhenObstacle()
    {
        var actual = _objectUnderTest.ProcessEvents("..P...O.....");
        //Assert.AreEqual("001234321000", actual);
    }
}