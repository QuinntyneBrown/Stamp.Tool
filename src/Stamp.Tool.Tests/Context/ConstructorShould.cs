// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Stamp.Tool.Tests.Context;

using Context = Stamp.Tool.Context;

public class ConstructorShould
{
    [Fact]
    public void ReturnContext()
    {
        // ARRANGE

        // ACT
        var result = new Context(new NamingConventionConverter(), new []
        {
            "",
            "--tokens",
            "name:Foo"
        })
        {
        };

        // ASSERT
        Assert.NotNull(result);
    }

}


