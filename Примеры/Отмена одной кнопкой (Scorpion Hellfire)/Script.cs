/*
    Пример отмены одной кнопкой для персонажа Скорпион Адское Пламя из игры Mortal Kombat XL.
    
    Держать X или M - огненный шар с задержкой.
    Отпустить X или M - отмена огненного шара в бег.
    
    Тут необходимо привыкнуть к таймингу, так как длительность нажатия X или M зависит от того,
    в какую комбу вы забуферили огненный шар.

    Как играть скорпионом: https://youtu.be/5FPDvRX6IBg?t=1730
*/


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

        if (key == Key.X)
        {
            if (!repeat)
            {
                Keys.LeftSide();
                FireBallStart();
            }
            
            return true;
        }

        if (key == Key.M)
        {
            if (!repeat)
            {
                Keys.RightSide();
                FireBallStart();
            }
            
            return true;
        }

        return false;
    }

    static bool OnKeyUp(Key key)
    {
        if (!enabled)
            return false;
        
        if (key == Key.X || key == Key.M)
        {
            FireBallCancel();
            return true;
        }
        
        return false;
    }

    static void FireBallStart()
    {
        UnpressAllKeys();
        
        KeyPress(Keys.Down);
        KeyPress(Keys.Back);
        KeyDown(Keys.FrontPunch);

        Thread.Sleep(Keys.PressTime);
    }
    
    static void FireBallCancel()
    {
        KeyPress(Keys.Forward);
        Thread.Sleep(Keys.PressTime);
        KeyPress(Keys.Forward, Keys.Block);
        KeyUp(Keys.FrontPunch);
    }
}
