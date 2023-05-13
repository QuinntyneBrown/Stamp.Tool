// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using RazorEngineCore;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Stamp.Tool;

public class RazorTemplateProcessor
{
    private readonly RazorEngine _razorEngine = new RazorEngine();

    internal static bool IsAnonymousType(Type type)
    {
        if (type == null)
            throw new ArgumentNullException("type");

        return Attribute.IsDefined(type, typeof(CompilerGeneratedAttribute), false)
            && type.IsGenericType && type.Name.Contains("AnonymousType")
            && (type.Name.StartsWith("<>") || type.Name.StartsWith("VB$"))
            && type.Attributes.HasFlag(TypeAttributes.NotPublic);
    }

    private static bool IsAnonymousType<T>() => IsAnonymousType(typeof(T));

    public async Task<string> ProcessAsync<T>(string template, T model)
    {
        if(IsAnonymousType<T>())
            return await ProcessAnonymousModelAsync(template, model);

        var compiledTemplate = await _razorEngine.CompileAsync<RazorEngineTemplateBase<T>>(template);

        return await compiledTemplate.RunAsync(instance => instance.Model = model);

    }

    internal async Task<string> ProcessAnonymousModelAsync(string template, dynamic model)
    {
        var compiledTemplate = await _razorEngine.CompileAsync(template);

        return await compiledTemplate.RunAsync(model);
    }

    public async Task<string> ProcessAsync(string template)
    {
        var compiledTemplate = await _razorEngine.CompileAsync(template);

        return await compiledTemplate.RunAsync();
    }
}