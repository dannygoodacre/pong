using Microsoft.Xna.Framework;
using Pong.Components;

namespace Pong.Systems;

public class ScoreSystem(Pong game, EntityManager entityManager, GameState state) : ISystem
{
    public void Update(GameTime gameTime)
    {
        Vector2 position = entityManager.GetComponent<Position>(game.Ball);
        
        var size = entityManager.GetComponent<Size>(game.Ball);

        var hasScored = false;

        if (position.X + size.Width < game.ScreenBounds.Left)
        {
            var score = ++state.RightPlayerScore;

            entityManager.SetComponent<Texture>(game.RightScore, game.ScoreFonts[score]);

            hasScored = true;

            state.ServeDirection = -Vector2.UnitX;
        }

        if (position.X > game.ScreenBounds.Right)
        {
            var score = ++state.LeftPlayerScore;

            entityManager.SetComponent<Texture>(game.LeftScore, game.ScoreFonts[score]);

            hasScored = true;

            state.ServeDirection = Vector2.UnitX;
        }

        if (hasScored)
        {
            entityManager.SetComponent<Position>(game.Ball, Options.BallStartPosition);

            entityManager.SetComponent<Velocity>(game.Ball, Vector2.Zero);

            entityManager.SetComponent<Position>(game.LeftPlayer, Options.LeftPlayerStartPosition);

            entityManager.SetComponent<Position>(game.RightPlayer, Options.RightPlayerStartPosition);

            if (state.LeftPlayerScore == 9 || state.RightPlayerScore == 9)
            {
                return;
            }

            state.IsBallInPlay = false;
        }
    }
}
