using System;
using System.Collections;
using System.Diagnostics;


static class ComboRunner
{
    // Текущая выполняемая комба
    static IEnumerator combo = null;

    // Пользователь может в комбу вставить запуск другой комбы
    static bool alreadyStopped = false;

    // Задержка в миллисекундах. Выполнение комбы приостановлено, если delay > 0
    static long delay = 0;

    // Измеритель интервалов времени
    static Stopwatch stopwatch = new Stopwatch();

    // Посреди комбы может быть пауза. Во время паузы нужно вернуть управление главному циклу программы,
    // чтобы пользователь мог прервать выполнение комбы.
    // Использование функции: yield return ComboRunner.Wait(20);
    public static object Wait(long milliseconds)
    {
        delay = milliseconds;

        // Нужно вернуть что угодно, чтобы можно было использовать с yield
        return null;
    }

    public static void Update()
    {
        // Никакая комба не выполняется. Ничего не делаем
        if (combo == null)
            return;

        while (true)
        {
            if (delay > 0)
            {
                if (!stopwatch.IsRunning)
                {
                    stopwatch.Restart();
                    return;
                }

                if (delay > stopwatch.ElapsedMilliseconds)
                    return;

                stopwatch.Stop();
                delay = 0;
            }

            if (!combo.MoveNext())
            {
                // Хотя старая комба кончилась, но была запущена вложенная комба
                // и ее стопать не нужно
                if (!alreadyStopped)
                {
                    Stop();
                    return;
                }
            }

            alreadyStopped = false;
        }
    }

    public static void Start(Func<IEnumerator> combo)
    {
        // Если в данный момент уже выполняется комба, то прерываем ее
        if (ComboRunner.combo != null)
        {
            Stop();
            alreadyStopped = true;
        }

        ComboRunner.combo = combo();
    }

    public static void Stop()
    {
        combo = null;
        delay = 0;
        stopwatch.Stop();
    }
}
