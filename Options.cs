using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Pong.Components;

namespace Pong;

// The options are scaled in proportion to the screen dimensions. 
// Any magic numbers here are simply scale factors which felt right - adjust them accordingly.

public static class Options
{
    public const int ScreenWidth = 2560;

    public const int ScreenHeight = 1440;

    public const Keys LeftPlayerUp = Keys.W;

    public const Keys LeftPlayerDown = Keys.S;
    
    public const Keys RightPlayerUp = Keys.I;
    
    public const Keys RightPlayerDown = Keys.K;

    public static readonly Size BallSize = new(ScreenWidth / 100, ScreenWidth / 100);

    public static readonly Vector2 BallStartPosition = new(ScreenWidth / 2 - BallSize.Width / 2, ScreenHeight / 2 - BallSize.Height / 2);

    public static readonly float MaxBallServeAngle = (float)Math.Atan2(ScreenHeight / 4f, ScreenWidth / 2);

    public const int BallSpeed = 50 * ScreenHeight;

    public const int PlayerSpeed = 60 * ScreenHeight;

    public const float BallSpeedIncreaseFactor = 1.05f;

    public static readonly Size PlayerSize = new(ScreenWidth / 100, ScreenHeight / 12);

    public static readonly Vector2 LeftPlayerStartPosition = new(ScreenWidth / 20, ScreenHeight / 2 - PlayerSize.Height / 2);

    public static readonly Vector2 RightPlayerStartPosition = new(ScreenWidth - ScreenWidth / 20 - PlayerSize.Width, ScreenHeight / 2 - PlayerSize.Height / 2);

    public static readonly Size NetSize = new(ScreenWidth / 160, ScreenHeight);

    public static readonly Vector2 NetPosition = new(ScreenWidth / 2 - NetSize.Width / 2, 0);

    public static readonly Vector2 LeftPlayerScorePosition = new(ScreenWidth / 2 - ScreenWidth / 24, ScreenWidth / 16);

    public static readonly Vector2 RightPlayerScorePosition = new(ScreenWidth / 2 + ScreenWidth / 24 - ScoreWidth, ScreenWidth / 16);

    public static readonly Size ScoreSize = new(ScoreWidth, ScoreHeight);

    private const int ScoreHeight = ScreenHeight / 20;

    private const int ScoreWidth = (int)(0.6f * ScoreHeight);
}
