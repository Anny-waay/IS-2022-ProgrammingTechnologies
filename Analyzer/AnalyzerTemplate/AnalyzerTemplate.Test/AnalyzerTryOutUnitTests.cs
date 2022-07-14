using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VerifyCS = AnalyzerTemplate.Test.CSharpCodeFixVerifier<
    AnalyzerTemplate.AnalyzerTryOutAnalyzer,
    AnalyzerTemplate.AnalyzerTryOutCodeFixProvider>;

namespace AnalyzerTemplate.Test
{
    [TestClass]
    public class AnalyzerTryOutUnitTest
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            var test = @"namespace ConsoleApplication1
            {
                class Test
                {
                    public int TryMethod(int a, int b)
                    {
                        if(a > b)
                        {
                            return a;
                        }

                        if(a < b)
                        {
                            return b;
                        }

                        return a+b;
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
                    public bool TryMethod(int a, int b, out int res)
                    {
                        if(a > b)
                        {
                            res = a;
                            return true;
                        }

                        if(a < b)
                        {
                            res = b;
                            return true;
                        }

                        res = a+b;
                        return true;
                    }

                    static void Main()
                    {
                    }   
                }
            }";
            
            var (diagnostics, document, workspace) = await UtilitiesTryOut.GetDiagnosticsAdvanced(test);
            var diagnostic = diagnostics[0];
            var codeFixProvider = new AnalyzerTryOutCodeFixProvider();
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
                    public bool TryMethod(int a)
                    {
                        if(a == 1)
                        {
                            return true;
                        }

                        if(a == 2)
                        {
                            return true;
                        }

                        return false;
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
                    public bool TryMethod(int a)
                    {
                        if(a == 1)
                        {
                            return true;
                        }

                        if(a == 2)
                        {
                            return true;
                        }

                        return false;
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
            var test = @"using System.Collections.Generic;

            namespace ConsoleApplication1
            {
                class Test
                {
                    public List<string> TryMethod(int a, int b)
                    {
                        if(a > b)
                        {
                            return new List<string>(){""a""};
                        }

                        if(a < b)
                        {
                            return new List<string>(){""b""};
                        }

                        return new List<string>(){""ab""};
                    }   

                    static void Main()
                    {
                    }
                }
            }";

            var fixtest = @"using System.Collections.Generic;

            namespace ConsoleApplication1
            {
                class Test
                {
                    public bool TryMethod(int a, int b, out List<string> res)
                    {
                        if(a > b)
                        {
                            res = new List<string>(){""a""};
                            return true;
                        }

                        if(a < b)
                        {
                            res = new List<string>(){""b""};
                            return true;
                        }

                        res = new List<string>(){""ab""};
                        return true;
                    }

                    static void Main()
                    {
                    }   
                }
            }";
            
            var (diagnostics, document, workspace) = await UtilitiesTryOut.GetDiagnosticsAdvanced(test);
            var diagnostic = diagnostics[0];
            var codeFixProvider = new AnalyzerTryOutCodeFixProvider();
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
        public async Task TestMethod4()
        {
            var test = @"namespace ConsoleApplication1
            {
                class Test
                {
                    public int MyMethod(int a, int b)
                    {
                        if(a > b)
                        {
                            return a;
                        }

                        if(a < b)
                        {
                            return b;
                        }

                        return a+b;
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
                    public int MyMethod(int a, int b)
                    {
                        if(a > b)
                        {
                            return a;
                        }

                        if(a < b)
                        {
                            return b;
                        }

                        return a+b;
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
