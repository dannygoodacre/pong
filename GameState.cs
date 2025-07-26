using Microsoft.Xna.Framework;

namespace Pong;

public class GameState
{
    public bool IsBallInPlay = false;

    public Vector2 ServeDirection = Vector2.UnitX;

    public int LeftPlayerScore = 0;

    public int RightPlayerScore = 0;
}
