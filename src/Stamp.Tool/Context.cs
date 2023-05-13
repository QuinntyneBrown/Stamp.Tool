// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Stamp.Tool;

public class Context
{
    private readonly INamingConventionConverter _namingConventionConverter;
    public Context(INamingConventionConverter namingConventionConverter, params string[] args)
    {
        _namingConventionConverter = namingConventionConverter;

        args ??= Environment.GetCommandLineArgs().Skip(1).ToArray();

        FileName = GetValue(args, new[] { "--name", "-n" });

        Extension = GetValue(args, new[] { "--extension", "-e" });

        var currentDirectory = GetValue(args, new[] { "--directory", "-d" });

        Directory = string.IsNullOrEmpty(currentDirectory) ? Environment.CurrentDirectory : currentDirectory;

        Template = GetValue(args, new[] { "--template", "-t" });

        Tokens = GetTokens(args);
    }

    string GetValue(string[] args, string[] keys)
    {
        int index = -1;

        foreach (var key in keys)
        {
            var i = Array.IndexOf(args, key);

            if (i != -1)
            {
                index = i;
                break;
            }
        }


        if (index == -1)
            return string.Empty;

        return args[index + 1];
    }

    Dictionary<string, string> GetTokens(string[] args)
    {
        var tokens = new Dictionary<string, string>();

        var entry = GetValue(args, new[] { "--tokens" });

        var namingConventions = new[]
        {
            NamingConvention.PascalCase,
            NamingConvention.CamelCase,
            NamingConvention.TitleCase,
            NamingConvention.SnakeCase,
            NamingConvention.KebobCase
        };

        if (string.IsNullOrEmpty(entry))
            return tokens;

        foreach (var item in entry.Split(','))
        {
            var parts = item.Split(':');

            tokens.Add(WithHandleBars(parts[0]), parts[1]);

            foreach(var namingConvention in  namingConventions)
            {
                tokens.Add(WithHandleBars($"{parts[0]}{namingConvention}"), _namingConventionConverter.Convert(namingConvention, parts[1]));
            }

            tokens.Add(WithHandleBars($"{parts[0]}PascalCase"), _namingConventionConverter.Convert(NamingConvention.PascalCase, parts[1]));
            tokens.Add(WithHandleBars($"{parts[0]}CamelCase"), _namingConventionConverter.Convert(NamingConvention.CamelCase, parts[1]));
            tokens.Add(WithHandleBars($"{parts[0]}TitleCase"), _namingConventionConverter.Convert(NamingConvention.TitleCase, parts[1]));
            tokens.Add(WithHandleBars($"{parts[0]}SnakeCase"), _namingConventionConverter.Convert(NamingConvention.SnakeCase, parts[1]));
            tokens.Add(WithHandleBars($"{parts[0]}KebobCase"), _namingConventionConverter.Convert(NamingConvention.KebobCase, parts[1]));
        }

        return tokens;
    }

    string WithHandleBars(string value) => "{{ " + value.Trim() + " }}";

    public string FileName { get; set; }
    public string Extension { get; set; }
    public string Directory { get; set; } = Environment.CurrentDirectory;
    public string Template { get; set; }
    public Dictionary<string, string> Tokens { get; set; }

}

