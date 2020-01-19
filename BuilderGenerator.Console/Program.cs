using System;
using System.IO;

namespace BuilderGenerator.Console
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var classFilePath = GetClassFilePath(args);
            var classFileContent = GetClassFileContent(classFilePath);
            var builderClassContent = GenerateBuilder(classFileContent);
            var builderClassFilePath = CreateBuilderClassFilePath(classFilePath);
            SaveBuilderClassFile(builderClassFilePath, builderClassContent);
        }

        private static string CreateBuilderClassFilePath(string classFilePath)
        {
            var builderClassFileName = Path.GetFileNameWithoutExtension(classFilePath) + "Builder" + Path.GetExtension(classFilePath);
            return Path.Join(
                Path.GetDirectoryName(classFilePath), builderClassFileName);
        }

        private static void SaveBuilderClassFile(string builderClassFilePath, string builderClassContent)
        {
            File.WriteAllText(builderClassFilePath, builderClassContent);
        }

        private static string GetClassFilePath(string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentException("Source file class path was not specified. It should be the first argument", "arg0");
            }

            return args[0];
        }

        private static string GetClassFileContent(string classFilePath)
        {
            if (!File.Exists(classFilePath))
            {
                throw new ArgumentException("Source file class does not exist under specified path: " + classFilePath, "arg0");
            }

            return File.ReadAllText(classFilePath);
        }

        private static string GenerateBuilder(string classFileContent)
        {
            var generator = new Core.BuilderGenerator();
            return generator.Generate(classFileContent);
        }
    }
}