using Microsoft.Xna.Framework.Input;

namespace Pong.Components;

public readonly struct PlayerInput(Keys up, Keys down)
{
    public readonly Keys Up = up;

    public readonly Keys Down = down;
}
