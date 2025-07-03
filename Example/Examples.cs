namespace Example
{
    public static class Examples
    {
        public static void Main(string[] args)
        {
            // Process arguments.
            
            if (args.Length < 1) Window.Create();
            else if (args.Length == 1)
            {
                switch (args[0].ToLower())
                {
                    case "window": Window.Create(); break;
                    case "rainbow": Rainbow.Create(); break;
                    default: Window.Create(); break;
                }
            }
            else Window.Create();
        }
    }
}