// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Stamp.Tool;

var context = new Context(new NamingConventionConverter(), args);

var template = string.IsNullOrEmpty(context.Template) ? string.Empty : File.ReadAllText(context.Template);

var result = template;

var fileName = context.FileName;

foreach (var token in context.Tokens)
{
    result = result.Replace(token.Key, token.Value);

    fileName = fileName.Replace(token.Key, token.Value);
}

File.WriteAllText(Path.Combine(context.Directory, $"{fileName}{context.Extension}"), result);