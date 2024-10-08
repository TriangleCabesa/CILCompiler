﻿using CILCompiler;
using CILCompiler.ASTVisitors.Implementations;
using CILCompiler.Tokens;

string filePath = Environment.ExpandEnvironmentVariables(@"%userprofile%\Desktop\class foo.jh.txt");

string src = FileReader.ReadFile(filePath);

var lexer = new Lexer(src);
var parser = new Parser(lexer);
var obj = parser.Parse();
var generator = new ILCreationVisitor();

var type = generator.CompileObject([obj])[0].CreateType();
var bar = Activator.CreateInstance(type);
var method = type.GetMethod("Main") ?? throw new InvalidProgramException("Project has no suitable entry point");
Console.WriteLine(method?.Invoke(bar, []) ?? "");