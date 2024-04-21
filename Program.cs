using Raylib_cs;
using System.Numerics;

namespace Pong;

class Program
{
    // Constants
    private const int D_WIDTH = 800;
    private const int D_HEIGHT = 480;

    private const int D_BALL_SIZE = 5;

    private const int D_PADDLE_WIDTH = 10;
    private const int D_PADDLE_HEIGHT = 100;
    private const int D_PADDLE_VELOCITY = 5;

    // Readonly Fields
    private static float _P1Score = 0;
    private static float _P2Score = 0;
    private static readonly string ROOT_DIR = Directory.GetCurrentDirectory();
    private static readonly Vector2 D_PADDLE_SIZE = new Vector2(D_PADDLE_WIDTH, D_PADDLE_HEIGHT);

    // Variables
    private static Vector2 _BallPos;
    private static Vector2 _BallSpeed;
    private static Vector2 _LeftPaddlePos;
    private static Vector2 _RightPaddlePos;

    private static List<int> _Keys = new List<int>();

    public static void Main()
    {
        Raylib.SetTargetFPS(60);

        Raylib.InitWindow(D_WIDTH, D_HEIGHT, "Pong");
        var windowIcon = Raylib.LoadImage(Path.Combine(ROOT_DIR, "Assets/Icon.png"));
        Raylib.SetWindowIcon(windowIcon);

        SetInitialState();

        while (!Raylib.WindowShouldClose())
        {
            UpdateState();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            DrawGame();

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }

    public static void SetInitialState()
    {
        _BallSpeed = new Vector2(2.5f, 3.5f);
        _BallPos = new Vector2((D_WIDTH / 2) - (D_BALL_SIZE / 2), (D_HEIGHT / 2) - (D_BALL_SIZE / 2));
        _LeftPaddlePos = new Vector2(10, (D_HEIGHT / 2) - (D_PADDLE_HEIGHT / 2));
        _RightPaddlePos = new Vector2(D_WIDTH - 20, (D_HEIGHT / 2) - (D_PADDLE_HEIGHT / 2));
    }

    public static bool CanMovePaddle(int futureValue) => futureValue >= 0 && futureValue + D_PADDLE_HEIGHT <= D_HEIGHT;

    public static void UpdateState()
    {
        // Paddles
        if (Raylib.IsKeyDown(KeyboardKey.W) && CanMovePaddle((int)_LeftPaddlePos.Y - D_PADDLE_VELOCITY))
        {
            //_LeftPaddlePos = new Vector2(_LeftPaddlePos.X, _LeftPaddlePos.Y - D_PADDLE_VELOCITY);
            _LeftPaddlePos.Y -= D_PADDLE_VELOCITY;
        }
        if (Raylib.IsKeyDown(KeyboardKey.S) && CanMovePaddle((int)_LeftPaddlePos.Y + D_PADDLE_VELOCITY))
        {
            //_LeftPaddlePos = new Vector2(_LeftPaddlePos.X, _LeftPaddlePos.Y + D_PADDLE_VELOCITY);
            _LeftPaddlePos.Y += D_PADDLE_VELOCITY;
        }
        if (Raylib.IsKeyDown(KeyboardKey.Up) && CanMovePaddle((int)_RightPaddlePos.Y - D_PADDLE_VELOCITY))
        {
            //_RightPaddlePos = new Vector2(_RightPaddlePos.X, _RightPaddlePos.Y - D_PADDLE_VELOCITY);
            _RightPaddlePos.Y -= D_PADDLE_VELOCITY;
        }
        if (Raylib.IsKeyDown(KeyboardKey.Down) && CanMovePaddle((int)_RightPaddlePos.Y + D_PADDLE_VELOCITY))
        {
            //_RightPaddlePos = new Vector2(_RightPaddlePos.X, _RightPaddlePos.Y + D_PADDLE_VELOCITY);
            _RightPaddlePos.Y += D_PADDLE_VELOCITY;
        }

        // Ball
        _BallPos.X += _BallSpeed.X;
        _BallPos.Y += _BallSpeed.Y;

        if (_BallPos.X <= D_BALL_SIZE || _BallPos.X >= D_WIDTH - D_BALL_SIZE) _BallSpeed.X *= -1.0f;
        if (_BallPos.Y <= D_BALL_SIZE || _BallPos.Y >= D_HEIGHT + D_BALL_SIZE) _BallSpeed.Y *= -1.0f;
    }

    public static void DrawGame()
    {
        // Score
        Raylib.DrawText(_LeftPaddlePos.Y.ToString(), (D_WIDTH / 4), 10, 20, Color.Black);
        Raylib.DrawText(_RightPaddlePos.Y.ToString(), (D_WIDTH / 4) * 3, 10, 20, Color.Black);

        // Ball
        Raylib.DrawCircleV(_BallPos, D_BALL_SIZE, Color.Black);

        // Paddles
        Raylib.DrawRectangleV(_LeftPaddlePos, D_PADDLE_SIZE, Color.Blue);
        Raylib.DrawRectangleV(_RightPaddlePos, D_PADDLE_SIZE, Color.Green);

        // Telemetry
        if (_Keys.Any())
        {
            Raylib.DrawText($"Key: {_Keys[^1]} - {(char)_Keys[^1]}", (D_WIDTH / 2) - 60, (D_HEIGHT - 30), 20, Color.Black);
        }
    }
}