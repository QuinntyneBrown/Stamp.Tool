// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Stamp.Tool.Tests.RazorTemplateProcessor;

using System.Text;
using RazorTemplateProcessor = Stamp.Tool.RazorTemplateProcessor;

public class ProcessAsyncShould
{
    public class ViewModel
    {
        private readonly RazorTemplateProcessor _processor = new RazorTemplateProcessor();
        public string SayHi() => "Hi";

        public List<int> List = new List<int>() {  1, 2, 3 };

        public async Task<string> Indent(dynamic value) => await Task.FromResult($"{value}".PadLeft(4));

        public async Task<string> ProcessAsync(string template, dynamic model) => await _processor.ProcessAsync(template,model);
    }

    [Fact]
    public async Task RenderDateTime()
    {
        // ARRANGE

        var sut = new RazorTemplateProcessor();

        // ACT
        var result = await sut.ProcessAsync($"@using System{Environment.NewLine}@DateTime.Now");

        // ASSERT
        Assert.NotNull(result);
    }

    [Fact]
    public async Task PreserveIndentationInLoop()
    {
        // ARRANGE


        StringBuilder template = new StringBuilder()
            .AppendLine("@for(var i = 10; i < 21; i++)")
            .AppendLine("{  @await Model.ProcessAsync(\"Foo\", i)  }");

        var sut = new RazorTemplateProcessor();

        // ACT
        var result = await sut.ProcessAsync(template.ToString(), new ViewModel());

        // ASSERT
        Assert.NotNull(result);
    }

    [Fact]
    public async Task AwaitPreserveIndentationInLoop()
    {
        // ARRANGE


        StringBuilder template = new StringBuilder()
            .AppendLine("@for(var i = 10; i < 21; i++)")
            .AppendLine("{  @await Model.Indent(i)  }");

        var sut = new RazorTemplateProcessor();

        // ACT
        var result = await sut.ProcessAsync(template.ToString(), new ViewModel());

        // ASSERT
        Assert.NotNull(result);
    }

    [Fact]
    public async Task RenderStronglyTypedFunctionResult()
    {
        // ARRANGE

        var sut = new RazorTemplateProcessor();

        // ACT
        var result = await sut.ProcessAsync($"@SayHi()", new ViewModel());

        // ASSERT
        Assert.NotNull(result);
    }

    [Fact]
    public async Task RenderFunctionResult()
    {
        // ARRANGE

        var sut = new RazorTemplateProcessor();

        
        // ACT
        var result = await sut.ProcessAsync("<area>@{ SayHi(); }</area>" + Environment.NewLine + "@{ void SayHi() {  <div>Hi</div> } }");

        // ASSERT
        Assert.NotNull(result);
    }

    [Fact]
    public async Task RenderFunctionResult1()
    {
        // ARRANGE

        var sut = new RazorTemplateProcessor();


        // ACT
        var result = await sut.ProcessAsync("<area>@{ SayHi(); }</area>" + Environment.NewLine + "@{ void SayHi() {  public } }");

        // ASSERT
        Assert.NotNull(result);
    }
}


