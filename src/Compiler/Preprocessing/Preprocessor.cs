using org.amimchik.QuantLangLinuxCompiler.src.Utils;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.Preprocessing;

public class Preprocessor
{
    public string PreprocessCode(string sourceCode)
    {
        List<string> defines = [];
        string[] sourceSplited = SplitLines(sourceCode);
        Stack<bool> ifCondsStack = [];
        List<string> outputLines = [];

        for (int i = 0; i < sourceSplited.Length; i++)
        {
            string line = sourceSplited[i];
            if (line.Trim().StartsWith("#endif"))
            {
                ifCondsStack.Pop();
                continue;
            }
            else if (ifCondsStack.Count != 0)
            {
                if (!ifCondsStack.Peek())
                {
                    continue;
                }
            }
            if (line.Trim().StartsWith("#include"))
            {
                string[] parts = line.Split(" ");
                if (parts.Length != 2)
                {
                    throw new Exception("Preprocessor syntax error.");
                }
                string targetFile = parts[1];
                string fileContent;
                if (targetFile.StartsWith('<') && targetFile.EndsWith('>'))
                {
                    targetFile = targetFile.Remove(0, 1).Remove(targetFile.Length - 2, 1);
                    fileContent = File.ReadAllText(FindFile(AppInfo.GetStdIncludes(), targetFile));
                }
                else
                {
                    targetFile = targetFile.Remove(0, 1).Remove(targetFile.Length - 2, 1);
                    fileContent = File.ReadAllText(FindFile(AppInfo.GetUsrIncludes(), targetFile));
                }
                foreach (var includeline in SplitLines(PreprocessCode(fileContent)))
                {
                    outputLines.Add(includeline);
                }
            }
            else if (line.Trim().StartsWith("#define"))
            {
                string[] parts = line.Split(" ");
                if (parts.Length != 2)
                {
                    throw new Exception("Preprocessor syntax error.");
                }
                string macro = parts[1];
                defines.Add(macro);
            }
            else if (line.Trim().StartsWith("#ifndef"))
            {
                string[] parts = line.Split(" ");
                if (parts.Length != 2)
                {
                    throw new Exception("Preprocessor syntax error.");
                }
                string arg = parts[1];
                ifCondsStack.Push(!defines.Contains(arg));
            }
            else
            {
                outputLines.Add(line);
            }
        }

        return CombineLines([.. outputLines]);
    }
    private static string FindFile(string[] directories, string targetFileName)
    {
        foreach (var directory in directories)
        {
            // Проверка, существует ли директория
            if (Directory.Exists(directory))
            {
                string[] files = Directory.GetFiles(directory, targetFileName);

                if (files.Length > 0)
                {
                    // Если файл найден, возвращаем его путь
                    return files[0]; // Можно вернуть первый найденный файл
                }
            }
        }

        // Если файл не найден в перечисленных директориях
        return null!;
    }
    private static string[] SplitLines(string input)
    {
        return input.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
    }
    private static string CombineLines(string[] lines)
    {
        return string.Join(Environment.NewLine, lines);
    }
}