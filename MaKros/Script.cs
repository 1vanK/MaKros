/*
    Пример скрипта (персонаж Горо Куатанский воин из игры Mortal Kombat XL)
*/


using System;
using System.Collections;
using System.Threading;


static partial class Script
{
    // Действия при запуске программы
    static void Start()
    {
        // Выводим подсказку
        Console.WriteLine("Press Num Lock to enable/disable\nPress F12 to exit\nPress F11 to show keys");
        Console.WriteLine("Q, E - Goro's command throw");
        Console.WriteLine("X, M - Foot Smash, Punch Walk");
        Console.WriteLine("Hold F, H - Long combo");
        Console.WriteLine("Hold C - Infinite Tremor");
    }

    static bool enabled = false;

    // Реакция на нажатие какой-нибудь клавиши
    static bool OnKeyDown(Key key, bool repeat)
    {
        // При нажатии F12 выходим из программы
        if (key == Key.F12)
        {
            quit = true;
            return true;
        }

        // При нажатии F11 включаем/отключаем отображение нажимаемых клавиш
        if (key == Key.F11)
        {
            if (!repeat)
                showKeys = !showKeys;

            return true;
        }

        // При нажатии Num Lock включаем/отключаем работу скрипта
        if (key == Key.NumLock)
        {
            if (!repeat)
                enabled = !enabled;

            return false; // Разрешаем дальнейшую обработку нажатия, чтобы загорелся индикатор
        }

        // Если скрипт отключен, то выходим
        if (!enabled)
            return false;

        // При нажатии Q выполняем командный захват Горо влево
        if (key == Key.Q)
        {
            if (!repeat)
            {
                Keys.RightSide();
                GoroThrow();
            }
            
            return true;
        }

        // При нажатии E выполняем командный захват Горо вправо
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
        
        // При нажатии F запускаем длинную комбу
        if (key == Key.F)
        {
            if (!repeat)
            {
                Keys.LeftSide();
                ComboRunner.Start(LongCombo);
            }
            
            return true;
        }
        
        // При нажатии H запускаем длинную комбу
        if (key == Key.H)
        {
            if (!repeat)
            {
                Keys.RightSide();
                ComboRunner.Start(LongCombo);
            }
            
            return true;
        }
        
        // При нажатии на C запускаем бесконечное землетрясение
        if (key == Key.C)
        {
            if (!repeat)
                ComboRunner.Start(Tremor);
                
            return true;
        }

        return false;
    }

    // Реакция на отпускание какой-нибудь клавиши
    static bool OnKeyUp(Key key)
    {
        if (key == Key.F || key == Key.H || key == Key.C)
            ComboRunner.Stop(); // Прерываем длинную комбу
        
        return false;
    }

    // Наказание
    static IEnumerator LongCombo()
    {
        Keys.UnpressAll();
        
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

    // Командный захват Горо
    static void GoroThrow()
    {
        Keys.UnpressAll();

        KeyPress(Keys.Down);
        KeyPress(Keys.Back);
        KeyPress(Keys.Forward, Keys.FrontKick);
    }

    // Землетрясение Горо Куатанского Воина
    static IEnumerator Tremor()
    {
        Keys.UnpressAll();

        while (true) // Бесконечный цикл
        {
            // Землетрясение
            KeyPress(Keys.Down);
            Thread.Sleep(Keys.PressTime); // Здесь используется ОБЫЧНАЯ задержка. В этом месте комбу прервать нельзя
            KeyPress(Keys.Down, Keys.BackKick);

            yield return ComboRunner.Wait(17 * 88); // Здесь используется ПРЕРЫВАЕМЯ задержка. В этом месте комбу можно прервать
        }
    }

    // Нижний удар, а потом кулачная прогулка
    static void LowWalkPunch()
    {
        Keys.UnpressAll();
        
        KeyPress(Keys.Back, Keys.FrontKick);
        KeyPress(Keys.Forward, Keys.BackKick);
    }
}
