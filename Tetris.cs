namespace TetrisGameDemo
{
    public class Tetris
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int[,] Shape { get; set; }

        private static readonly Random random = new Random();

        public Tetris()
        {
            Shape = GetRandomShape();
            X = 4;
            Y = 0;
        }

        private int[,]? GetRandomShape()
        {
            int[][,] shapes = new int[][,]
            {
                new int[,]
                {
                    { 1, 1, 1, 1 }
                },
                new int[,]
                {
                    { 1, 0, 0 },
                    { 1, 1, 1 }
                },
                new int[,]
                {
                    { 0, 0, 1 },
                    { 1, 1, 1 }
                },
                new int[,]
                {
                    { 1, 1 },
                    { 1, 1 }
                },
                new int[,]
                {
                    { 0, 1, 1 },
                    { 1, 1, 0 }
                },
                new int[,]
                {
                    { 0, 1, 0 },
                    { 1, 1, 1 }
                },
                new int[,]
                {
                    { 1, 1, 0 },
                    { 0, 1, 1 }
                }
            };

            return shapes[random.Next(shapes.Length)];
        }

        public void Rotate()
        {
            int rows = Shape.GetLength(0);
            int cols = Shape.GetLength(1);
            int[,] rotated = new int[cols, rows];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    rotated[c, rows - 1 - r] = Shape[r, c];
                }
            }

            Shape = rotated;
        }
    }
}