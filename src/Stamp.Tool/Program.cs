using Stamp.Tool;

var context = new Context(args);

var template = File.ReadAllText(context.Template);

var result = template;

var fileName = context.FileName;

foreach(var token in context.Tokens)
{
    result = result.Replace(token.Key, token.Value);

    fileName = fileName.Replace(token.Key, token.Value);
}

File.WriteAllText(Path.Combine(context.CurrentDirectory, fileName, context.Extension), result);