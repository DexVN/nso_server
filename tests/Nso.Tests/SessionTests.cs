using Xunit;
using Nso.World;

public class SessionTests
{
    [Fact]
    public void Session_Timeout_After_Inactivity()
    {
        var s = new Session();
        s.Touch();
        s.Advance(TimeSpan.FromMinutes(31));
        Assert.True(s.IsExpired);
    }
}
