namespace RoboAPI.Models
{
    public class Commands
    {
        private int x, y;

        public Direction direction;

        public bool isPlaced = false;

        public static readonly int GridSize = 5;
        
        public enum Direction
        {
            NORTH,
            EAST,
            SOUTH,
            WEST
        }
        public void Place(int x, int y, Direction direction)
        {
            if (IsValidPosition(x, y))
            {
                this.x = x;
                this.y = y;
                this.direction = direction;
                isPlaced = true;
            }
        }

        public void Move()
        {
            if (!isPlaced)
            {
                return;
            }

            int newX = x, newY = y;
            
            switch (direction)
            {
                case Direction.NORTH:
                    newY++;
                    break;
                case Direction.SOUTH:
                    newY--;
                    break;
                case Direction.EAST:
                    newX++;
                    break;
                case Direction.WEST:
                    newX--;
                    break;
            }

            if (IsValidPosition(newX, newY))
            {
                x = newX;
                y = newY;
            }
        }

        public void TurnLeft()
        {
            if (!isPlaced)
            {
                return;
            }

            direction = (Direction)(((int)direction + 3) % 4);
        }

        public void TurnRight()
        {
            if (!isPlaced)
            {
                return;
            }

            direction = (Direction)(((int)direction + 1) % 4);
        }

        public string Report()
        {
            if (isPlaced)
            {
                return $"{x}, {y}, {direction}";
            }
            return "Robot is not placed on the table.";
        }

        private bool IsValidPosition(int x, int y)
        {
            return x >= 0 && x < GridSize && y >= 0 && y < GridSize;
        }
    }
}
