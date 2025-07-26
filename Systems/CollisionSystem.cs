using System;
using Microsoft.Xna.Framework;
using Pong.Components;

namespace Pong.Systems;

public class CollisionSystem(Pong game, EntityManager entityManager) : ISystem
{
    public void Update(GameTime gameTime)
    {
        foreach (var entity1 in entityManager.WithComponents<Collision, Position>())
        {
            var collision1 = entityManager.GetComponent<Collision>(entity1);

            var height = entityManager.GetComponent<Size>(entity1).Height;

            Vector2 position1 = entityManager.GetComponent<Position>(entity1);
            Vector2 velocity1 = entityManager.GetComponent<Velocity>(entity1);

            var top = position1.Y;
            var bottom = position1.Y + height;
            
            if (top <= game.ScreenBounds.Top)
            {
                if (collision1.IsStatic)
                {
                    position1.Y = game.ScreenBounds.Top;

                    entityManager.SetComponent<Position>(entity1, position1);
                }
                else
                {
                    velocity1.Y *= -1;

                    entityManager.SetComponent<Velocity>(entity1, velocity1);
                }
            }

            if (bottom >= game.ScreenBounds.Bottom)
            {
                if (collision1.IsStatic)
                {
                    position1.Y = game.ScreenBounds.Bottom - height;

                    entityManager.SetComponent<Position>(entity1, position1);
                }
                else
                {
                    velocity1.Y *= -1;

                    entityManager.SetComponent<Velocity>(entity1, velocity1);
                }
            }

            if (!collision1.IsStatic)
            {
                continue;
            }

            var size1 = entityManager.GetComponent<Size>(entity1);

            var rectangle1 = size1.GetRectangleAt(position1);

            foreach (var entity2 in entityManager.WithComponents<Collision, Position>())
            {
                if (entity1 == entity2)
                {
                    continue;
                }
                
                var size2 = entityManager.GetComponent<Size>(entity2);

                Vector2 position2 = entityManager.GetComponent<Position>(entity2);

                var rectangle2 = size2.GetRectangleAt(position2);

                if (!rectangle1.Intersects(rectangle2))
                {
                    continue;
                }
                
                Vector2 velocity2 = entityManager.GetComponent<Velocity>(entity2);

                var collision2 = entityManager.GetComponent<Collision>(entity2);

                var isStatic2 = collision2.IsStatic;

                if (!isStatic2)
                {
                    var centre = position2.Y + size2.Height / 2;

                    velocity2.X *= -1;

                    if (centre < position1.Y + size1.Height / 3)
                    {
                        velocity2.Y = -Math.Abs(velocity2.Y);
                    }
                    else if (centre >= position2.Y + 2 * size1.Height / 3)
                    {
                        velocity2.Y = Math.Abs(velocity2.Y);
                    }
                    
                    entityManager.SetComponent<Velocity>(entity2, velocity2 * Options.BallSpeedIncreaseFactor);
                }
            }
        }
    }
}
