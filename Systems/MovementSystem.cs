using Microsoft.Xna.Framework;
using Pong.Components;

namespace Pong.Systems;

public class MovementSystem(EntityManager entityManager) : ISystem
{
    public void Update(GameTime gameTime)
    {
        var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        foreach (var entity in entityManager.WithComponents<Position, Velocity>())
        {
            Vector2 position = entityManager.GetComponent<Position>(entity);
            Vector2 velocity = entityManager.GetComponent<Velocity>(entity);

            position += deltaTime * velocity;
            
            entityManager.SetComponent<Position>(entity, position);
        }
    }
}
