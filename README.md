# C# Builder Generator
[![Build Status](https://dev.azure.com/konradpociask/BuilderGenerator/_apis/build/status/BuilderGenerator?branchName=master)](https://dev.azure.com/konradpociask/BuilderGenerator/_build/latest?definitionId=1&branchName=master)

C# Builder Generator is a able to generate Builder class from your code class.

## Usage

### Console
Run 'BuilderGenerator.Console.exe path/to/file/with/class/declaration/ClassName.cs'
As a result new file ClassNameBuilder.cs will be created next to ClassName.cs

### Web app
https://buildergenerator.azurewebsites.net/

## Example

### Input

```cs
public class ExampleClass
{
    public string Name { get; set; }
}
```

### Output

```cs
public class ExampleClassBuilder
{
    private string _name;

    public ExampleClassBuilder()
    {
        _name = "name";
    }

    public ExampleClassBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public ExampleClass Build()
    {
        return new ExampleClass()
        {
            Name = _name
        };
    }
}
```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)

* How to use it


* Version
1.0.0