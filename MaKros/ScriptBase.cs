using System;
using System.Collections.Generic;


using KeyList = System.Collections.Generic.List<Key>;
using DeviceID = System.Int32;


static partial class Script
{
    static IntPtr context;

    // Идентификатор используемой в последний раз клавиатуры.
    // Именно от ее лица будут имитироваться нажатия
    static DeviceID currentDeviceID = 1;

    // Отображать ли нажимаемые клавиши
    static bool showKeys = false;

    static Script()
    {
        context = Interception.CreateContext();
        Interception.SetFilter(context, Interception.IsKeyboard, Interception.Filter.All);
    }

    public static void Destructor()
    {
        Interception.DestroyContext(context);
    }

    static Key ToKey(Interception.KeyStroke keyStroke)
    {
        ushort result = (ushort)keyStroke.Code;

        if ((keyStroke.State & Interception.KeyState.E0) != 0)
            result += 0x100;

        return (Key)result;
    }

    static Interception.KeyStroke ToKeyStroke(Key key, bool down)
    {
        Interception.KeyStroke result = new Interception.KeyStroke();

        if (!down)
            result.State = Interception.KeyState.Up;

        ushort code = (ushort)key;
        if (code >= 0x100)
        {
            code -= 0x100;
            result.State = Interception.KeyState.E0;
        }
        result.Code = code;

        return result;
    }

    // Для каждого устройства хранится список вжатых кнопок.
    // Можно в любой момент узнать реальное состояние любой кнопки
    static Dictionary<DeviceID, KeyList> downedKeys = new Dictionary<DeviceID, KeyList>();

    static KeyList GetOrCreateKeyList(Dictionary<DeviceID, KeyList> dictionary, DeviceID deviceID)
    {
        KeyList result;
        if (!dictionary.TryGetValue(deviceID, out result))
        {
            result = new KeyList();
            dictionary[deviceID] = result;
        }
        return result;
    }

    public static bool IsKeyDown(DeviceID deviceID, Key key)
    {
        KeyList deviceDownedKeys;
        if (!downedKeys.TryGetValue(deviceID, out deviceDownedKeys))
            return false;
        return deviceDownedKeys.Contains(key);
    }

    public static bool IsKeyDown(Key key)
    {
        return IsKeyDown(currentDeviceID, key);
    }

    public static bool IsKeyUp(DeviceID deviceID, Key key)
    {
        return !IsKeyDown(deviceID, key);
    }

    public static bool IsKeyUp(Key key)
    {
        return IsKeyUp(currentDeviceID, key);
    }

    public static void KeyDown(DeviceID deviceID, params Key[] keys)
    {
        foreach (Key key in keys)
        {
            Interception.Stroke stroke = new Interception.Stroke();
            stroke.Key = ToKeyStroke(key, true);
            Interception.Send(context, deviceID, ref stroke, 1);
        }
    }

    public static void KeyDown(params Key[] keys)
    {
        KeyDown(currentDeviceID, keys);
    }

    public static void KeyUp(DeviceID deviceID, params Key[] keys)
    {
        foreach (Key key in keys)
        {
            Interception.Stroke stroke = new Interception.Stroke();
            stroke.Key = ToKeyStroke(key, false);
            Interception.Send(context, deviceID, ref stroke, 1);
        }
    }

    public static void KeyUp(params Key[] keys)
    {
        KeyUp(currentDeviceID, keys);
    }

    public static void KeyPress(DeviceID deviceID, params Key[] keys)
    {
        KeyDown(deviceID, keys);
        System.Threading.Thread.Sleep(Keys.PressTime);
        KeyUp(deviceID, keys);
    }

    public static void KeyPress(params Key[] keys)
    {
        KeyPress(currentDeviceID, keys);
    }

    // Оставил эту функцию для совместимости со старыми скриптами.
    // Используйте Keys.UnpressAll() вместо нее
    public static void UnpressAllKeys(params Key[] except)
    {
        Keys.UnpressAll(except);
    }

    public static void Update()
    {
        // Повторяем цикл, пока есть необработанные нажатия
        while (true)
        {
            DeviceID deviceID = Interception.WaitWithTimeout(context, 0);
            if (deviceID == 0)
                break;

            currentDeviceID = deviceID;

            Interception.Stroke stroke = new Interception.Stroke();

            while (Interception.Receive(context, deviceID, ref stroke, 1) > 0)
            {
                Key key = ToKey(stroke.Key);

                if (showKeys)
                    Console.WriteLine("Key: {0}; Scancode: 0x{1:X2}; State: {2}", key, stroke.Key.Code, stroke.Key.State);

                bool processed;
                
                KeyList deviceDownedKeys = GetOrCreateKeyList(downedKeys, deviceID);

                if (stroke.Key.State.IsKeyDown())
                {
                    if (!deviceDownedKeys.Contains(key))
                    {
                        deviceDownedKeys.Add(key);
                        processed = OnKeyDown(key, false);
                    }
                    else
                    {
                        processed = OnKeyDown(key, true);
                    }
                }
                else
                {
                    deviceDownedKeys.Remove(key);
                    processed = OnKeyUp(key);
                }

                if (!processed)
                    Interception.Send(context, deviceID, ref stroke, 1);
            }
        }
    }
}

