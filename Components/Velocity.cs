using Microsoft.Xna.Framework;

namespace Pong.Components;

public struct Velocity
{
    public Vector2 velocity;

    public static implicit operator Vector2(Velocity p) => p.velocity;
    
    public static implicit operator Velocity(Vector2 vector2) => new() { velocity = vector2 };

}
