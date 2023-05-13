// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using RazorEngineCore;

public class RazorTemplateProcessor
{
    private readonly RazorEngine _razorEngine = new RazorEngine();

    public async Task<string> ProcessAsync<T>(string template, T model)
    {
        var compiledTemplate = await _razorEngine.CompileAsync<RazorEngineTemplateBase<T>>(template);

        return await compiledTemplate.RunAsync(i => i.Model = model);
    }

}