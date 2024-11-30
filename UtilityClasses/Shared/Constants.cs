namespace TennisSummerGJ2024.UtilityClasses.Shared;
public static class InputMapAction
{
    public const string MoveUp = "move up";
    public const string MoveDown = "move down";
    public const string MoveLeft = "move left";
    public const string MoveRight = "move right";
    public const string Swing = "swing";
}

public static class NodeGroup
{
    public const string Player = "player";
    public const string Ball = "ball";
    public const string Shadow = "shadow";
}

public static class HeightLevel
{
    public const int Shadow = 1;
    public const int Ground = 2;
    public const int Eye = 4;
    public const int Sky = 8;
}