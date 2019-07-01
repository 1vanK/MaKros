/*
    Горо Куатанский воин из игры Mortal Kombat XL.
    Этот скрипт отличается от примера из руководства.
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

        if (key == Key.Q)
        {
            if (!repeat)
            {
                Keys.RightSide();
                GoroThrow();
            }
            
            return true;
        }

        if (key == Key.E)
        {
            if (!repeat)
            {
                Keys.LeftSide();
                GoroThrow();
            }
            
            return true;
        }
        
        if (key == Key.X)
        {
            if (!repeat)
            {
                Keys.LeftSide();
                LowWalkPunch();
            }
            
            return true;
        }
        
        if (key == Key.M)
        {
            if (!repeat)
            {
                Keys.RightSide();
                LowWalkPunch();
            }
            
            return true;
        }
        
        if (key == Key.R)
        {
            if (!repeat)
                Teleport();

            return true;
        }
        
        if (key == Key.F)
        {
            if (!repeat)
            {
                Keys.LeftSide();
                ComboRunner.Start(LongCombo);
            }
            
            return true;
        }
        
        if (key == Key.H)
        {
            if (!repeat)
            {
                Keys.RightSide();
                ComboRunner.Start(LongCombo);
            }
            
            return true;
        }
        
        if (key == Key.C)
        {
            if (!repeat)
                ComboRunner.Start(Tremor);
                
            return true;
        }

        return false;
    }

    static bool OnKeyUp(Key key)
    {
        if (key == Key.F || key == Key.H || key == Key.C)
            ComboRunner.Stop();
        
        return false;
    }

    static IEnumerator LongCombo()
    {
        UnpressAllKeys();
        
        // Удар варвара
        KeyPress(Keys.Back, Keys.FrontPunch);
        KeyPress(Keys.BackPunch);
        Thread.Sleep(Keys.PressTime);
        KeyPress(Keys.Up, Keys.BackPunch);
        
        yield return ComboRunner.Wait(17 * 80);
        
        // Микропробежка
        KeyPress(Keys.Forward);
        Thread.Sleep(Keys.PressTime);
        KeyPress(Keys.Forward, Keys.Block);

        yield return ComboRunner.Wait(17 * 10);

        // Удар варвара
        KeyPress(Keys.Back, Keys.FrontPunch);
        KeyPress(Keys.BackPunch);
        Thread.Sleep(Keys.PressTime);
        KeyPress(Keys.Up, Keys.BackPunch);
        
        yield return ComboRunner.Wait(17 * 85);
        
        // Попутный ветер
        KeyPress(Keys.Back, Keys.BackPunch);
        
        yield return ComboRunner.Wait(17 * 42);
        
        // Выпад грудью
        KeyPress(Keys.Back);
        KeyPress(Keys.Forward, Keys.BackPunch);
    }

    static void GoroThrow()
    {
        UnpressAllKeys();

        if (IsKeyDown(Key.LShift))
        {
            KeyPress(Keys.Down);
            KeyPress(Keys.Back);
            KeyPress(Keys.Forward, Keys.FrontKick, Keys.Block);
        }
        else
        {
            KeyPress(Keys.Down);
            KeyPress(Keys.Back);
            KeyPress(Keys.Forward, Keys.FrontKick);
        }
    }

    // Землетрясение
    static IEnumerator Tremor()
    {
        UnpressAllKeys();

        while (true)
        {
            if (IsKeyDown(Key.LShift))
            {
                KeyPress(Keys.Down);
                Thread.Sleep(Keys.PressTime);
                KeyPress(Keys.Down, Keys.BackKick, Keys.Block);
            }
            else
            {
                KeyPress(Keys.Down);
                Thread.Sleep(Keys.PressTime);
                KeyPress(Keys.Down, Keys.BackKick);
            }

            yield return ComboRunner.Wait(17 * 88);
        }
    }

    static void LowWalkPunch()
    {
        UnpressAllKeys();
        
        KeyPress(Keys.Back, Keys.FrontKick);
        KeyPress(Keys.Forward, Keys.BackKick);
    }
    
    static void Teleport()
    {
        UnpressAllKeys();

        if (IsKeyDown(Key.LShift))
        {
            KeyPress(Keys.Down);
            KeyPress(Keys.Up, Keys.Block);
        }
        else
        {
            KeyPress(Keys.Down);
            KeyPress(Keys.Up);
        }
    }
}
