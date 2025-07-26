using Microsoft.Xna.Framework;

namespace Pong.Components;

public struct Size(int width, int height)
{
    public int Width = width;

    public int Height = height;
    
    public readonly Rectangle GetRectangleAt(Vector2 position) => new((int)position.X, (int)position.Y, Width, Height);
}
