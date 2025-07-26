using Microsoft.Xna.Framework.Graphics;

namespace Pong.Components;

public struct Texture()
{
    public Texture2D texture;

    public static implicit operator Texture2D(Texture texture) => texture.texture;
    
    public static implicit operator Texture(Texture2D texture) => new() { texture = texture};
}
