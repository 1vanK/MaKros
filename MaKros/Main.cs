static partial class Script
{
    static bool quit = false;

    static void Main()
    {
        Start();

        while (!quit)
        {
            Update();
            ComboRunner.Update();
            System.Threading.Thread.Sleep(1);
        }

        Destructor();
    }
}
