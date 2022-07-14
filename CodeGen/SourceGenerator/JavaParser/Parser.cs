using System;
using System.Collections.Generic;
using System.IO;

namespace JavaParser
{
    public class Parser
    {
        public List<MethodDeclaration> ParseController(string path)
        {
            //string path = "/Users/annakomova/Desktop/TechProg/lab-2/cats/src/main/java/com/lab2/cats/controllers/CatController.java";
            var methodDeclarations = new List<MethodDeclaration>();
            using (StreamReader reader = new StreamReader(path))
            {
                string? line;
                string prevLine=string.Empty;
                string startPath = string.Empty;
                while ((line = reader.ReadLine()) != null)
                { 
                    if (line.Contains("@RequestMapping"))
                    { 
                        startPath = line.Substring(line.IndexOf('"') + 1);
                        startPath = startPath.Substring(0, startPath.IndexOf('"'));
                    }

                    if (line.Contains("GetMapping") || line.Contains("PostMapping"))
                    {
                        MethodDeclaration md = new MethodDeclaration();
                        string httpMethodName = line.Substring(0, line.IndexOf('('));
                        httpMethodName = httpMethodName.Trim();
                        httpMethodName = httpMethodName.Replace("@", "");
                        httpMethodName = httpMethodName.Replace("Mapping", "");
                        string httpName = line.Substring(line.IndexOf('"') + 1);
                        httpName = httpName.Substring(0, httpName.IndexOf('"'));
                        httpName = "http://localhost:8080/" + startPath + '/' + httpName;
                        md.HttpMethodName = httpMethodName;
                        md.Url = httpName;
                        methodDeclarations.Add(md);
                    }

                    if (prevLine.Contains("GetMapping") || prevLine.Contains("PostMapping"))
                    {
                        string[] words = line.Split(new char[] { ' ' });
                        bool isArgs = false;
 
                        for (int i =0; i<words.Length; i++)
                        {
                            
                            if (isArgs)
                            {
                                string argType = words[i];
                                if (!argType.Contains('@'))
                                {
                                    var argDeclaration = new ArgDeclaration();
                                    if (prevLine.Contains("GetMapping"))
                                    {
                                        argType = argType.ToLower();
                                    }
                                    argDeclaration.Type = argType;
                                    argDeclaration.Name = words[i+1].Substring(0, words[i+1].Length-1);
                                    methodDeclarations[^1].ArgList.Add(argDeclaration);
                                    if (words[i+1].Contains(')'))
                                    {
                                        isArgs = false;
                                    }
                                    i++;
                                }

                            }
                            
                            if (words[i].Contains('('))
                            {
                                string returnType = words[i - 1];
                                if (returnType.Contains("Optional"))
                                {
                                    returnType = returnType.Substring(returnType.IndexOf('<') + 1);
                                    returnType = returnType.Substring(0, returnType.Length-1);
                                }
                                string methodName = words[i].Substring(0, words[i].IndexOf('('));
                                methodDeclarations[^1].ReturnType = returnType;
                                methodDeclarations[^1].MethodName = methodName;
                                string argType = words[i].Substring(words[i].IndexOf('(')+1);
                                if (!argType.Contains(')'))
                                {
                                    isArgs = true;
                                }

                                if (!argType.Contains('@') && !argType.Contains(')') && argType!="")
                                {
                                    var argDeclaration = new ArgDeclaration();
                                    if (prevLine.Contains("GetMapping"))
                                    {
                                        argType = argType.ToLower();
                                    }
                                    argDeclaration.Type = argType;

                                    argDeclaration.Name = words[i+1].Substring(words[i+1].IndexOf('(')+1);
                                    methodDeclarations[^1].ArgList.Add(argDeclaration);
                                    if (words[i + 1].Contains(')'))
                                    {
                                        argDeclaration.Name = words[i+1].Substring(0, words[i+1].IndexOf(')'));
                                        isArgs = false;
                                    }

                                    i++;
                                }
                            }
                        }
                    }

                    if (line != String.Empty)
                    {
                        prevLine = line;
                    }
                }
            }

            return methodDeclarations;
        }

        public EntityDeclaration ParseEntity(string path)
        {
            var entityDeclaration = new EntityDeclaration();
            using (StreamReader reader = new StreamReader(path))
            {
                string? line;
                string prevLine = string.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("class"))
                    {
                        string[] words = line.Split(new char[] { ' ' });
                        entityDeclaration.EntityName = words[2];
                    }
                    
                    if (prevLine.Contains("@Column") || prevLine.Contains("@ManyToOne") || prevLine.Contains("@OneToMany"))
                    {
                        string[] words = line.Split(new char[] { ' ' });
                        var arg = new ArgDeclaration();
                        arg.Name = words[^1].Substring(0, words[^1].IndexOf(';'));
                        if (!words[^2].Contains("List") && !words[^2].Contains("LocalDate"))
                        {
                            arg.Type= words[^2].ToLower();
                        }
                        else
                        {
                            if (words[^2].Contains("LocalDate"))
                            {
                                arg.Type = "DateTime";
                            }
                            else
                            {
                                arg.Type = words[^2];
                            }
                        }

                        entityDeclaration.ArgList.Add(arg);
                    }
                    
                    if (line != String.Empty)
                    {
                        prevLine = line;
                    }
                }
            }

            return entityDeclaration;
        }
    }
}