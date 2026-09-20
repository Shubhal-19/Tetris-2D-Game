
namespace TetrisGameDemo
{
    public partial class Form1 : Form
    {
        private const int Rows = 20;
        private const int Columns = 10;
        private const int CellSize = 23;

        private int[,] board = new int[Rows, Columns];
        private Tetris currentPiece;
        private int score = 0;
        public Form1()
        {
            InitializeComponent();
            ClientSize = new Size(Columns * CellSize, Rows * CellSize);
            MaximumSize = new Size(Columns * CellSize + 16, Rows * CellSize + 32);
            MinimumSize = new Size(Columns * CellSize + 16, Rows * CellSize + 32);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Tetris Game Demo";
            MaximizeBox = false;
            MinimizeBox = false;
            DoubleBuffered = true;
            DoubleBuffered = true;
            timerGame.Interval = 300;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            timerGame.Tick += TimerGame_Tick;
            KeyDown += Form1_KeyDown;
            pictureBoxGame.Paint += PictureBoxGame_Paint;
            timerGame.Start();
            SpawnNewPiece();

        }

        private void SpawnNewPiece()
        {
            currentPiece = new Tetris();
            if (!IsValidPosition(currentPiece, currentPiece.X, currentPiece.Y))
            {
                timerGame.Stop();
                MessageBox.Show($"Game Over! Your score: {score}");
                ResetGame();
            }
        }

        private bool IsValidPosition(Tetris currentPiece, int x, int y)
        {
            if (currentPiece == null) return false;
            int[,] shape = currentPiece.Shape;
            for (int i = 0; i < shape.GetLength(0); i++)
            {
                for (int j = 0; j < shape.GetLength(1); j++)
                {
                    if (shape[i, j] != 0)
                    {
                        int boardX = x + j;
                        int boardY = y + i;
                        if (boardX < 0 || boardX >= Columns || boardY < 0 || boardY >= Rows)
                            return false;
                        if ( boardY >= 0 && board[boardY, boardX] != 0)
                            return false;
                    }
                }
            }
            return true;
        }

        private void ResetGame()
        {
            board = new int[Rows, Columns];
            score = 0;
            timerGame.Start();
            SpawnNewPiece();
            
        }

        private void PictureBoxGame_Paint(object? sender, PaintEventArgs e)
        {
            // Draw the board and settled pieces
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    if (board[r, c] != 0)
                    {
                        e.Graphics.FillRectangle(Brushes.Blue, c * CellSize, r * CellSize, CellSize, CellSize);
                        e.Graphics.DrawRectangle(Pens.Black, c * CellSize, r * CellSize, CellSize, CellSize);
                    }
                }
            }

            // Draw the current piece
            if (currentPiece != null)
            {
                int[,] shape = currentPiece.Shape;
                for (int i = 0; i < shape.GetLength(0); i++)
                {
                    for (int j = 0; j < shape.GetLength(1); j++)
                    {
                        if (shape[i, j] != 0)
                        {
                            int boardX = currentPiece.X + j;
                            int boardY = currentPiece.Y + i;
                            e.Graphics.FillRectangle(Brushes.Red, boardX * CellSize, boardY * CellSize, CellSize, CellSize);
                            e.Graphics.DrawRectangle(Pens.Black, boardX * CellSize, boardY * CellSize, CellSize, CellSize);
                        }
                    }
                }
            }
        }

        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    MovePiece(-1, 0);
                    break;
                    case Keys.Right:
                    MovePiece(1, 0);
                    break;
                    case Keys.Down:
                    MovePiece(0, 1);
                    break;
                    case Keys.Up:
                    RotatePiece();
                    break;
                    case Keys.Space:
                    DropPiece();
                    break;                   

            }
        }

        private void DropPiece()
        {
            // Pseudocode:
            // 1. While the piece can move down, move it down by one.
            // 2. When it can't move down, lock it in place.
            // 3. Check for completed lines and update score.
            // 4. Spawn a new piece.

            if (currentPiece == null) return;

            while (IsValidPosition(currentPiece, currentPiece.X, currentPiece.Y + 1))
            {
                currentPiece.Y += 1;
            }

            // Lock the piece in place
            int[,] shape = currentPiece.Shape;
            for (int i = 0; i < shape.GetLength(0); i++)
            {
                for (int j = 0; j < shape.GetLength(1); j++)
                {
                    if (shape[i, j] != 0)
                    {
                        int boardX = currentPiece.X + j;
                        int boardY = currentPiece.Y + i;
                        if (boardY >= 0 && boardY < Rows && boardX >= 0 && boardX < Columns)
                        {
                            board[boardY, boardX] = 1;
                        }
                    }
                }
            }

            // Check for completed lines
            for (int r = Rows - 1; r >= 0; r--)
            {
                bool fullLine = true;
                for (int c = 0; c < Columns; c++)
                {
                    if (board[r, c] == 0)
                    {
                        fullLine = false;
                        break;
                    }
                }
                if (fullLine)
                {
                    // Remove the line and move everything above down
                    for (int row = r; row > 0; row--)
                    {
                        for (int col = 0; col < Columns; col++)
                        {
                            board[row, col] = board[row - 1, col];
                        }
                    }
                    for (int col = 0; col < Columns; col++)
                    {
                        board[0, col] = 0;
                    }
                    score += 100;
                    r++; // Recheck the same row after shifting
                }
            }

            SpawnNewPiece();
            pictureBoxGame.Invalidate();
        }

        private void RotatePiece()
        {
            if (currentPiece == null) return;
            int[,] originalShape = currentPiece.Shape;
            currentPiece.Rotate();
            if (!IsValidPosition(currentPiece, currentPiece.X, currentPiece.Y))
            {
                currentPiece.Shape = originalShape; // Revert if not valid
            }
        }

        private void MovePiece(int v1, int v2)
        {
            if (currentPiece == null) return;
            if (IsValidPosition(currentPiece, currentPiece.X + v1, currentPiece.Y + v2))
            {
                currentPiece.X += v1;
                currentPiece.Y += v2;
                pictureBoxGame.Invalidate();
            }
            else if (v2 == 1) // If moving down is not valid, lock the piece
            {
                SettledPieces();
                ClearLines();
                DropPiece();
                SpawnNewPiece();
               
            }
        }

        private void ClearLines()
        {
            int linesCleared = 0;
            for (int r = Rows - 1; r >= 0; r--)
            {
                bool fullLine = true;
                for (int c = 0; c < Columns; c++)
                {
                    if (board[r, c] == 0)
                    {
                        fullLine = false;
                        break;
                    }
                }
                if (fullLine)
                {
                    linesCleared++;
                    for (int row = r; row > 0; row--)
                    {
                        for (int col = 0; col < Columns; col++)
                        {
                            board[row, col] = board[row - 1, col];
                        }
                    }
                    for (int col = 0; col < Columns; col++)
                    {
                        board[0, col] = 0;
                    }
                    r++; // Recheck the same row after shifting
                }
            }
            if (linesCleared > 0)
            {
                score += linesCleared * 100;
            }
        }

        private void SettledPieces()
        {
            if (currentPiece == null) return;
            int[,] shape = currentPiece.Shape;
            for (int i = 0; i < shape.GetLength(0); i++)
            {
                for (int j = 0; j < shape.GetLength(1); j++)
                {
                    if (shape[i, j] != 0)
                    {
                        int boardX = currentPiece.X + j;
                        int boardY = currentPiece.Y + i;
                        if (boardY >= 0 && boardY < Rows && boardX >= 0 && boardX < Columns)
                        {
                            board[boardY, boardX] = currentPiece.Shape[i,j];
                        }
                    }
                }
            }
        }

        private void TimerGame_Tick(object? sender, EventArgs e)
        {
            MovePiece(0, 1);
            pictureBoxGame.Invalidate();
        }
    }
}
