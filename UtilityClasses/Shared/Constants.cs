namespace TennisSummerGJ2024.UtilityClasses.Shared;
public static class InputMapAction
{
    public const string MoveUp = "move up";
    public const string MoveDown = "move down";
    public const string MoveLeft = "move left";
    public const string MoveRight = "move right";
    public const string SwingDown = "swing down";
    public const string SwingUp = "swing up";
    public const string RespawnBall = "respawn ball";
}

public static class NodeGroup
{
    public const string Player = "player";
    public const string Ball = "ball";
    public const string Shadow = "shadow";
    public const string Net = "net";
}

public static class HeightLevel
{
    public const int Shadow = 1;
    public const int Net = 2;
    public const int Eye = 4;
    public const int Arm = 8;
    public const int Sky = 16;
}

public enum SwingDirection
{
    Down = 0,
    Up = 1,
}

public enum Direction
{
    None = 0,
    Up = 1,
    Down = 2,
    Right = 3,
    Left = 4,
}