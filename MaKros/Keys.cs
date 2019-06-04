// Измените кнопки на те, которые вы используете в игре
static class Keys
{
    // Кнопки ударов
    public const Key FrontPunch = Key.J; // Быстрый с руки = 1
    public const Key BackPunch = Key.I; // Мощный с руки = 2
    public const Key FrontKick = Key.K; // Быстрый с ноги = 3
    public const Key BackKick = Key.L; // Мощный с ноги = 4

    // Кнопки направлений
    public const Key Up = Key.W; // Вверх = U
    public const Key Down = Key.S; // Вниз = D
    public const Key Left = Key.A; // Влево (не используйте это в комбе)
    public const Key Right = Key.D; // Вправо (не используйте это в комбе)

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
    public const Key Interact = Key.O; // Взаимодействовать
    public const Key Throw = Key.U; // Бросок
    public const Key FlipStance = Key.P; // Сменить стойку
    public const Key Block = Key.Semicolon; // Блок

    // Тут можно добавить альтернативные имена для кнопок
    public const Key Блок = Block;
    public const Key _1 = FrontPunch; // Быстрый с руки
    public const Key _2 = BackPunch; // Мощный с руки
    public const Key _3 = FrontKick; // Быстрый с ноги
    public const Key _4 = BackKick; // Мощный с ноги

    // Сколько времени клавиша должна быть вжата, чтобы игра зафиксировала нажатие
    public const int PressTime = 20;
}

/*
Рекомендуемые настройки в игре:

Release Check = Легкое комбо = OFF
Alternate Control = Другое управление = OFF
Input Shortcuts = Быстрый ввод = ON
*/
