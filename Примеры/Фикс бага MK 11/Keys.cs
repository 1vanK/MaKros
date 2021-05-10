// Измените кнопки на те, которые вы используете в игре
static class Keys
{
    // Кнопки ударов
    public const Key FrontPunch = Key.J; // Быстрый с руки = 1
    public const Key BackPunch = Key.I; // Мощный с руки = 2
    public const Key FrontKick = Key.K; // Быстрый с ноги = 3
    public const Key BackKick = Key.L; // Мощный с ноги = 4

    // Кнопки направлений
    public const Key Up = Key.E; // Вверх = U
    public const Key Down = Key.D; // Вниз = D
    public const Key Left = Key.S; // Влево (не используйте это в комбе)
    public const Key Right = Key.F; // Вправо (не используйте это в комбе)

    // Кнопки перемещений зависят от того, куда смотрит персонаж.
    // Тут ничего трогать не надо, значения этих переменных устанавливаются функциями ниже
    public static Key Forward; // Вперед = F
    public static Key Back; // Назад = B
    
    // Вызвать эту функцию, когда персонаж слева от врага
    public static void LeftSide()
    {
        Forward = Right;
        Back = Left;
    }

    // Вызвать эту функцию, когда персонаж справа от врага
    public static void RightSide()
    {
        Forward = Left;
        Back = Right;
    }

    // Остальные кнопки
    public const Key Interact = Key.A; // Взаимодействовать, усиление спецприёма
    public const Key Throw = Key.Space; // Бросок
    public const Key FlipStance = Key.P; // Сменить стойку
    public const Key Block = Key.Semicolon; // Блок

    // Тут можно добавить альтернативные имена для кнопок
    public const Key Блок = Block;
    public const Key _1 = FrontPunch; // Быстрый с руки
    public const Key _2 = BackPunch; // Мощный с руки
    public const Key _3 = FrontKick; // Быстрый с ноги
    public const Key _4 = BackKick; // Мощный с ноги

    // Сколько времени клавиша должна быть вжата, чтобы игра зафиксировала нажатие
    public const int PressTime = 40;
    
    // Функция для отжатия кнопок
    public static void UnpressAll(params Key[] except)
    {
        // Отжимаем кнопку Up, если ее нет в списке except
        if (System.Array.IndexOf(except, Up) == -1)
            Script.KeyUp(Up);

        // Отжимаем кнопку Down, если ее нет в списке except
        if (System.Array.IndexOf(except, Down) == -1)
            Script.KeyUp(Down);

        // И так далее
        if (System.Array.IndexOf(except, Left) == -1)
            Script.KeyUp(Left);
        
        if (System.Array.IndexOf(except, Right) == -1)
            Script.KeyUp(Right);
        
        if (System.Array.IndexOf(except, FrontPunch) == -1)
            Script.KeyUp(FrontPunch);
        
        if (System.Array.IndexOf(except, BackPunch) == -1)
            Script.KeyUp(BackPunch);
        
        if (System.Array.IndexOf(except, FrontKick) == -1)
            Script.KeyUp(FrontKick);
        
        if (System.Array.IndexOf(except, BackKick) == -1)
            Script.KeyUp(BackKick);

        if (System.Array.IndexOf(except, Interact) == -1)
            Script.KeyUp(Interact);
        
        if (System.Array.IndexOf(except, Throw) == -1)
            Script.KeyUp(Throw);
        
        if (System.Array.IndexOf(except, FlipStance) == -1)
            Script.KeyUp(FlipStance);
        
        if (System.Array.IndexOf(except, Block) == -1)
            Script.KeyUp(Block);

        System.Threading.Thread.Sleep(PressTime);
    }
}

/*
Рекомендуемые настройки в игре:

Release Check = Легкое комбо = OFF
Alternate Control = Другое управление = OFF
Input Shortcuts = Быстрый ввод = ON
*/
