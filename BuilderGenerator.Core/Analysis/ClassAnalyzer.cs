using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuilderGenerator.Core.Analysis
{
    public class ClassAnalyzer
    {
        public AnalysisResult Analyze(string classAsAString)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(classAsAString);
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();
            var classDeclarationSyntax = root.DescendantNodes().OfType<ClassDeclarationSyntax>().FirstOrDefault();
            if (classDeclarationSyntax == null)
            {
                throw new Exception("No class definition");
            }

            var className = classDeclarationSyntax.Identifier.ValueText;

            var propertyDeclarationSyntaxArray = classDeclarationSyntax.DescendantNodes()
                .OfType<PropertyDeclarationSyntax>()
                .ToArray();
            var properties = new List<ClassMember>(propertyDeclarationSyntaxArray.Length);
            foreach (var propertyDeclarationSyntax in propertyDeclarationSyntaxArray)
	        {
                var type = propertyDeclarationSyntax.Type.ToString();
                var name = propertyDeclarationSyntax.Identifier.ValueText;
                var property = new ClassMember(type, name);
                properties.Add(property);
            }

            return new AnalysisResult(className, properties);
        }
    }
}
