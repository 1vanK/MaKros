using System;
using System.Collections;
using System.Threading;


static partial class Script
{
    static void Start()
    {
        Console.WriteLine("Press Num Lock to enable/disable\nPress F12 to exit\nPress F11 to show keys");
    }

    static bool enabled = false;
    
    static bool вправоНасильноОтжато = false;

    static bool OnKeyDown(Key key, bool repeat)
    {
        if (key == Key.F12)
        {
            quit = true;
            return true;
        }

        if (key == Key.F11)
        {
            if (!repeat)
                showKeys = !showKeys;

            return true;
        }

        if (key == Key.NumLock)
        {
            if (!repeat)
                enabled = !enabled;

            return false;
        }

        if (!enabled)
            return false;

        if (key == Keys.Left && IsKeyDown(Keys.Right))
        {
            KeyUp(Keys.Right);
            вправоНасильноОтжато = true;
            return false;
        }

        if (key == Key.W)
        {
            if (!repeat)
            {
                Keys.RightSide();
                Дэш();
            }

            return true;
        }

        if (key == Key.R)
        {
            if (!repeat)
            {
                Keys.LeftSide();
                Дэш();
            }

            return true;
        }

        return false;
    }

    static bool OnKeyUp(Key key)
    {
        // Нужно двигаться вправо, если держали ВЛЕВО и ВПРАВО, а потом отпустили ВЛЕВО
        if (key == Keys.Left && вправоНасильноОтжато)
        {
            KeyDown(Keys.Right);
            вправоНасильноОтжато = false;
        }

        if (key == Keys.Right)
            вправоНасильноОтжато = false;

        return false;
    }

    static void Дэш()
    {
        KeyPress(Keys.Forward);
        Thread.Sleep(Keys.PressTime);
        KeyPress(Keys.Forward);
    }
}
