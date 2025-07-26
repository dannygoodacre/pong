using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Pong;

public class SystemManager
{
    private readonly List<ISystem> _systems = [];

    public void RegisterSystems(params ISystem[] system) => _systems.AddRange(system);

    public void Update(GameTime gameTime)
    {
        foreach (var system in _systems)
        {
            system.Update(gameTime);
        }
    }
}

public interface ISystem
{
    public void Update(GameTime gameTime);
}
