namespace Stamp.Tool.Tests.RazorTemplateProcessor;

using RazorTemplateProcessor = Stamp.Tool.RazorTemplateProcessor;

public class IsAnonymousTypeShould
{
    [Fact]
    public void ReturnFalse()
    {
        // ARRANGE

        Assert.False(RazorTemplateProcessor.IsAnonymousType(typeof(IsAnonymousTypeShould))); 
    }

    [Fact]
    public void ReturnTrue()
    {
        // ARRANGE

        dynamic person = new { Name = "Joey" };

        Assert.True(RazorTemplateProcessor.IsAnonymousType(person.GetType()));
    }
}