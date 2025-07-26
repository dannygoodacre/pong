using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Pong.Components;

namespace Pong.Systems;

public class InputSystem(Pong game, GameState state, EntityManager entityManager) : ISystem
{
    private readonly Random _random = new();

    public void Update(GameTime gameTime)
    {

        var keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.Escape))
        {
            game.Exit();
        }

        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (!state.IsBallInPlay && keyboardState.IsKeyDown(Keys.Space))
        {
            state.IsBallInPlay = true;

            var angle = Options.MaxBallServeAngle * (_random.NextSingle() * 2 - 1);

            var velocity = Options.BallSpeed * deltaTime * Vector2.Rotate(state.ServeDirection, angle);
            
            entityManager.SetComponent<Velocity>(game.Ball, velocity);
        }

        foreach (var entity in entityManager.WithComponent<PlayerInput>())
        {
            var playerInput = entityManager.GetComponent<PlayerInput>(entity);

            var direction = Vector2.Zero;

            if (keyboardState.IsKeyDown(playerInput.Up))
            {
                direction.Y -= 1;
            }
            else if (keyboardState.IsKeyDown(playerInput.Down))
            {
                direction.Y += 1;
            }

            entityManager.SetComponent<Velocity>(entity, Options.PlayerSpeed * direction * deltaTime);
        }
    }
}
