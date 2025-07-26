using Microsoft.Xna.Framework;

namespace Pong.Components;

public struct Position()
{
    public Vector2 position;

    public static implicit operator Vector2(Position p) => p.position;

    public static implicit operator Position(Vector2 vector2) => new() { position = vector2 };
}
