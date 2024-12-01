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
    
    public static Shadow GetShadow(SceneTree tree)
    {
        var shadowNodes = tree.GetNodesInGroup(NodeGroup.Shadow);
        var shadow = shadowNodes.Cast<Shadow>().FirstOrDefault();
        if (shadow == null)
        {
            GD.PrintErr($"{nameof(shadow)} was null.");
        }
        
        return shadow;
    }
    
    public static Net GetNet(SceneTree tree)
    {
        var netNodes = tree.GetNodesInGroup(NodeGroup.Net);
        var net = netNodes.Cast<Net>().FirstOrDefault();
        if (net == null)
        {
            GD.PrintErr($"{nameof(net)} was null.");
        }
        
        return net;
    }
}