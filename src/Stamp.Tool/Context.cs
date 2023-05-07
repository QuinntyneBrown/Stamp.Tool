// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Stamp.Tool;

public class Context {

    public Context(params string[] args)
    {
        args ??= Environment.GetCommandLineArgs().Skip(1).ToArray();

        FileName = GetValue(args, new [] { "--name", "-n" });

        Extension = GetValue(args, new[] { "--extension", "-e" });

        CurrentDirectory = GetValue(args, new[] { "--directory", "-d" });

        Template = GetValue(args, new[] { "--template", "-t" });

        Tokens = GetTokens(args);
    }

    string GetValue(string[] args, string[] keys)
    {
        int index = -1;

        foreach(var key in keys)
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

    Dictionary<string,string> GetTokens(string[] args)
    {
        var tokens = new Dictionary<string,string>();

        var entry = GetValue(args, new[] { "--tokens" } );

        if(string.IsNullOrEmpty(entry))
            return tokens;

        foreach(var item in entry.Split(','))
        {
            var parts = item.Split(':');

            tokens.Add("{{ " + parts[0].Trim() + " }}", parts[1]);

        }

        return tokens;
    }

    public string FileName { get; set; }
    public string Extension { get; set; }
    public string CurrentDirectory { get; set; } = Environment.CurrentDirectory;
    public string Template { get; set; }
    public Dictionary<string, string> Tokens { get; set; }

}

