namespace WCFIntern
{
    public class Intern : IIntern
    {
        public string SayHello(string msg)
        {
            return "Hello " + msg;
        }

        public string GetName()
        {
            return "Call GetName()";
        }
    }
}