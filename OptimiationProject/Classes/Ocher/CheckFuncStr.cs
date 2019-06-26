using org.mariuszgromada.math.mxparser;

namespace OptimiationProject.Classes.Ocher
{
    public static class CheckFuncStr
    {
        public static bool CheckFunc(string func)
        {
            if (func == null || func.Equals(string.Empty))
            {
                return false;
            }

            var internalFunc = func.Trim().ToLower();

            var exp = new Expression("f(x)", new Function($"f(x) = { internalFunc }"));
            exp.addArguments(new Argument("x"));

            return exp.checkSyntax() && internalFunc.Contains("x");
        }
    }
}
