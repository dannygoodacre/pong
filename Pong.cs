using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Components;
using Pong.Extensions;
using Pong.Systems;

namespace Pong;

public class Pong : Game
{
    public GraphicsDeviceManager GraphicsDeviceManager;

    public Rectangle ScreenBounds;

    private SpriteBatch _spriteBatch;

    public readonly Texture2D[] ScoreFonts = new Texture2D[10];

    private readonly EntityManager _entityManager = new();

    private readonly SystemManager _systemManager = new();

    private readonly GameState _gameState = new();

    public int Ball;

    public int LeftPlayer;

    public int RightPlayer;

    public int LeftScore;

    public int RightScore;

    public Pong()
    {
        GraphicsDeviceManager = new(this)
        {
            PreferredBackBufferWidth = Options.ScreenWidth,
            PreferredBackBufferHeight = Options.ScreenHeight,
            IsFullScreen = true
        };

        Content.RootDirectory = "Content";
    }

    protected override void Initialize()
    {
        ScreenBounds = new Rectangle
        (
            0,
            0,
            GraphicsDevice.PresentationParameters.BackBufferWidth,
            GraphicsDevice.PresentationParameters.BackBufferHeight
        );

        _spriteBatch = new SpriteBatch(GraphicsDevice);

        var net = _entityManager.CreateEntity();

        _entityManager.AddComponent<Position>(net, Options.NetPosition);
        _entityManager.AddComponent<Size>(net, Options.NetSize);
        _entityManager.AddComponent<Components.Texture>(net, Options.NetSize.WithSolidColor(Color.White, GraphicsDevice));

        Ball = _entityManager.CreateEntity();

        _entityManager.AddComponent<Position>(Ball, Options.BallStartPosition);
        _entityManager.AddComponent<Velocity>(Ball, Vector2.Zero);
        _entityManager.AddComponent<Size>(Ball, Options.BallSize);
        _entityManager.AddComponent<Components.Texture>(Ball, Options.BallSize.WithSolidColor(Color.White, GraphicsDevice));
        _entityManager.AddComponent<Collision>(Ball, new() { IsStatic = false });

        LeftPlayer = _entityManager.CreateEntity();

        _entityManager.AddComponent<Position>(LeftPlayer, Options.LeftPlayerStartPosition);
        _entityManager.AddComponent<Velocity>(LeftPlayer, Vector2.Zero);
        _entityManager.AddComponent<Size>(LeftPlayer, Options.PlayerSize);
        _entityManager.AddComponent<Components.Texture>(LeftPlayer, Options.PlayerSize.WithSolidColor(Color.White, GraphicsDevice));
        _entityManager.AddComponent<Collision>(LeftPlayer, new() { IsStatic = true });
        _entityManager.AddComponent<PlayerInput>(LeftPlayer, new(Options.LeftPlayerUp, Options.LeftPlayerDown));
        
        RightPlayer = _entityManager.CreateEntity();

        _entityManager.AddComponent<Position>(RightPlayer, Options.RightPlayerStartPosition);
        _entityManager.AddComponent<Velocity>(RightPlayer, Vector2.Zero);
        _entityManager.AddComponent<Size>(RightPlayer, Options.PlayerSize);
        _entityManager.AddComponent<Components.Texture>(RightPlayer, Options.PlayerSize.WithSolidColor(Color.White, GraphicsDevice));
        _entityManager.AddComponent<Collision>(RightPlayer, new() { IsStatic = true });
        _entityManager.AddComponent<PlayerInput>(RightPlayer, new(Options.RightPlayerUp, Options.RightPlayerDown));

        LeftScore = _entityManager.CreateEntity();

        _entityManager.AddComponent<Position>(LeftScore, Options.LeftPlayerScorePosition);
        _entityManager.AddComponent<Size>(LeftScore, Options.ScoreSize);

        RightScore = _entityManager.CreateEntity();

        _entityManager.AddComponent<Position>(RightScore, Options.RightPlayerScorePosition);
        _entityManager.AddComponent<Size>(RightScore, Options.ScoreSize);

        _systemManager.RegisterSystems
        (
            new MovementSystem(_entityManager),
            new CollisionSystem(this, _entityManager),
            new InputSystem(this, _gameState, _entityManager),
            new ScoreSystem(this, _entityManager, _gameState)
        );

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new(GraphicsDevice);

        for (int i = 0; i < ScoreFonts.Length; i++)
        {
            ScoreFonts[i] = Content.Load<Texture2D>($"font/{i}");
        }

        _entityManager.AddComponent<Components.Texture>(LeftScore, ScoreFonts[0]);
        _entityManager.AddComponent<Components.Texture>(RightScore, ScoreFonts[0]);

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        _systemManager.Update(gameTime);


        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        foreach (var entity in _entityManager.WithComponent<Components.Texture>())
        {
            Texture2D texture = _entityManager.GetComponent<Components.Texture>(entity);

            Size size = _entityManager.GetComponent<Size>(entity);

            Vector2 position = _entityManager.GetComponent<Position>(entity);

            _spriteBatch.Draw(texture, size.GetRectangleAt(position), Color.White);
        } 

        _spriteBatch.End();
        
        base.Draw(gameTime);
    }
}
