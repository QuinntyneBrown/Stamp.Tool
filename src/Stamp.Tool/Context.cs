// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Stamp.Tool;

public class Context {

    public Context()
    {
        var args = Environment.GetCommandLineArgs();    


    }

    public string FileName { get; set; }
    public string Extension { get; set; }
    public string CurrentDirectory { get; set; } = Environment.CurrentDirectory;
    public string Template { get; set; }
    public Dictionary<string, string> Tokens { get; set; }
}

