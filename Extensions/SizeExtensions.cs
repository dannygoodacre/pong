
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Extensions;

public static class SizeExtensions
{
    public static Texture2D WithSolidColor(this Components.Size size, Color color, GraphicsDevice graphicsDevice)
    {
        var width = size.Width;
        var height = size.Height;

        var texture = new Texture2D(graphicsDevice, width, height);

        var data = new Color[width * height];

        for (var i = 0; i < data.Length; i++)
        {
            data[i] = color;
        }

        texture.SetData(data);

        return texture;
    }
}
