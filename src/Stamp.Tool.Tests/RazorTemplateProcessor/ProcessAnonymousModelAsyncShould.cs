// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Stamp.Tool.Tests.RazorTemplateProcessor;

using RazorTemplateProcessor = Stamp.Tool.RazorTemplateProcessor;

public class ProcessAnonymousModelAsyncShould {


    [Fact]
    public async Task RenderAnnoymousModel()
    {
        // ARRAGE
        var sut = new RazorTemplateProcessor();

        // ACT

        var result  =  await sut.ProcessAnonymousModelAsync("@Model.Name", new
        {
            Name = "Test"
        });

        Assert.Equal("Test", result);


    }
}
