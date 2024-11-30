using Godot;
using System.Linq;
using TennisSummerGJ2024.Scenes.Player;
using TennisSummerGJ2024.UtilityClasses.Shared;

namespace TennisSummerGJ2024.UtilityClasses.Helpers;
public static class GetNodeHelper
{
    public static Player GetPlayer(SceneTree tree)
    {
        var playerNodes = tree.GetNodesInGroup(NodeGroup.Player);
        var player = playerNodes.Cast<Player>().FirstOrDefault();
        if (player == null)
        {
            GD.PrintErr($"{nameof(player)} was null.");
        }
        
        return player;
    }
    
    public static Ball GetBall(SceneTree tree)
    {
        var ballNodes = tree.GetNodesInGroup(NodeGroup.Ball);
        var ball = ballNodes.Cast<Ball>().FirstOrDefault();
        if (ball == null)
        {
            GD.PrintErr($"{nameof(ball)} was null.");
        }
        
        return ball;
    }
}