// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Stamp.Tool;

var processor = new RazorTemplateProcessor();

var context = new Context(new NamingConventionConverter(), args);

var template = string.IsNullOrEmpty(context.Template) ? string.Empty : File.ReadAllText(context.Template);

var result = await processor.ProcessAsync(template, context.Tokens);

var fileName = await processor.ProcessAsync(context.FileName, context.Tokens);

File.WriteAllText(Path.Combine(context.Directory, $"{fileName}{context.Extension}"), result);
