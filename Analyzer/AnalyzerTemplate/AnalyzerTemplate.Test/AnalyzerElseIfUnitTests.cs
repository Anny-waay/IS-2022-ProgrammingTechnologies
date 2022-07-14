using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VerifyCS = AnalyzerTemplate.Test.CSharpCodeFixVerifier<
    AnalyzerTemplate.AnalyzerElseIfAnalyzer,
    AnalyzerTemplate.AnalyzerElseIfCodeFixProvider>;

namespace AnalyzerTemplate.Test
{
    [TestClass]
    public class AnalyzerElseIfUnitTest
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var test = @"namespace ConsoleApplication1
            {
                class Test
                {
                    public void Method(int a)
                    {
                        if (a == 1)
                        {
                            a++;
                            Console.WriteLine(2);
                        }
                        else
                        {
                            if (a == 2)
                            {
                                a--;
                                Console.WriteLine(1);
                            }
                        }
                    }   

                    static void Main()
                    {
                    }
                }
            }";

            var fixtest = @"namespace ConsoleApplication1
            {
                class Test
                {
                    public void Method(int a)
                    {
                        if (a == 1)
                        {
                            a++;
                            Console.WriteLine(2);
                        }
                        else if (a == 2)
                        {
                            a--;
                            Console.WriteLine(1);
                        }
                    }

                    static void Main()
                    {
                    }   
                }
            }";
            
            var (diagnostics, document, workspace) = await UtilitiesElseIf.GetDiagnosticsAdvanced(test);
            var diagnostic = diagnostics[0];
            var codeFixProvider = new AnalyzerElseIfCodeFixProvider();
            CodeAction registeredCodeAction = null;
            var context = new CodeFixContext(document, diagnostic, (codeAction, _) =>
            {
                if (registeredCodeAction != null)
                    throw new Exception("Code action was registered more than once");
            
                registeredCodeAction = codeAction;
            
            }, CancellationToken.None);
            await codeFixProvider.RegisterCodeFixesAsync(context);
            
            if (registeredCodeAction == null)
                throw new Exception("Code action was not registered");
            
            var operations = await registeredCodeAction.GetOperationsAsync(CancellationToken.None);
            foreach (var operation in operations)
            {
                operation.Apply(workspace, CancellationToken.None);
            }
            
            var updatedDocument = workspace.CurrentSolution.GetDocument(document.Id);
            var newCode = (await updatedDocument.GetTextAsync()).ToString();
            Assert.AreEqual(fixtest.Replace(" ", ""), newCode.Replace(" ", ""));
        }
        
        [TestMethod]
        public async Task TestMethod2()
        {
            var test = @"namespace ConsoleApplication1
            {
                class Test
                {
                    public void Method(int a)
                    {
                        if (a == 1)
                        {
                            a++;
                        }
                        else
                        {
                            if (a == 2)
                            {
                                a--;
                            }
                            a--;
                        }
                    }   

                    static void Main()
                    { 
                    }
                }
            }";

            var fixtest = @"namespace ConsoleApplication1
            {
                class Test
                {
                    public void Method(int a)
                    {
                        if (a == 1)
                        {
                            a++;
                        }
                        else
                        {
                            if (a == 2)
                            {
                                a--;
                            }
                            a--;
                        }
                    }   

                    static void Main()
                    { 
                    }
                }
            }";
            
            await VerifyCS.VerifyCodeFixAsync(test, fixtest);
        }
        
        [TestMethod]
        public async Task TestMethod3()
        {
            var test = @"namespace ConsoleApplication1
            {
                class Test
                {
                    public void Method(int a)
                    {
                        if (a == 1)
                        {
                            a++;
                        }
                    }   

                    static void Main()
                    { 
                    }
                }
            }";

            var fixtest = @"namespace ConsoleApplication1
            {
                class Test
                {
                    public void Method(int a)
                    {
                        if (a == 1)
                        {
                            a++;
                        }
                    }   

                    static void Main()
                    { 
                    }
                }
            }";
            
            await VerifyCS.VerifyCodeFixAsync(test, fixtest);
        }
    }
}
