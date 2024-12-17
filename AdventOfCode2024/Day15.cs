public class Day15 {
    public List<List<char>> warehouse;
    public Coord robotPos;
    public List<char> robotMovements;

    public readonly struct Coord {
        public Coord(int x, int y) {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj is Coord other)
            {
                return X == other.X && Y == other.Y;
            }
            return false;
        }

        public int X { get; }
        public int Y { get; }
    }

    public Day15(string inputPath) {
        warehouse = new List<List<char>>();
        robotMovements = new List<char>();

        bool loadingWareHouse = true;

        foreach (string s in File.ReadLines(inputPath)) {
            if (s.Length == 0) loadingWareHouse = false;
            if (loadingWareHouse) {
                List<char> line = new List<char>();
                for (int i = 0; i < s.Length; i++) {
                    //Part 1
                    //line.Add(s[i]);
                    //Part 2
                    if (s[i] == '#') {line.Add('#'); line.Add('#');}
                    if (s[i] == 'O') {line.Add('['); line.Add(']');}
                    if (s[i] == '.') {line.Add('.'); line.Add('.');}
                    if (s[i] == '@') {line.Add('@'); line.Add('.');}
                    //
                    if (s[i] == '@') robotPos = new Coord(warehouse.Count(), i * 2);
                }
                warehouse.Add(line);
            }else {
                for (int i = 0; i < s.Length; i++) {
                    robotMovements.Add(s[i]);
                }
            }
        }
        Console.WriteLine();
        printWarehouse();
    }

    public void attemptRobotMove(char dir) {
        if (dir == '>') {
            int x = robotPos.X;
            int y = robotPos.Y;
            while (warehouse[x][y] != '#' && warehouse[x][y] != '.') {
                y++;
            }
            if (warehouse[x][y] == '#') return;
            while (y != robotPos.Y) {
                char temp = warehouse[x][y];
                warehouse[x][y] = warehouse[x][y - 1];
                warehouse[x][y - 1] = temp;
                y--;
            }
            robotPos = new Coord(robotPos.X, robotPos.Y + 1);
        }
        if (dir == '<') {
            int x = robotPos.X;
            int y = robotPos.Y;
            while (warehouse[x][y] != '#' && warehouse[x][y] != '.') {
                y--;
            }
            if (warehouse[x][y] == '#') return;
            while (y != robotPos.Y) {
                char temp = warehouse[x][y];
                warehouse[x][y] = warehouse[x][y + 1];
                warehouse[x][y + 1] = temp;
                y++;
            }
            robotPos = new Coord(robotPos.X, robotPos.Y - 1);
        }
        if (dir == '^') {
            int x = robotPos.X;
            int y = robotPos.Y;
            while (warehouse[x][y] != '#' && warehouse[x][y] != '.') {
                x--;
            }
            if (warehouse[x][y] == '#') return;
            while (x != robotPos.X) {
                char temp = warehouse[x][y];
                warehouse[x][y] = warehouse[x + 1][y];
                warehouse[x + 1][y] = temp;
                x++;
            }
            robotPos = new Coord(robotPos.X - 1, robotPos.Y);
        }
        if (dir == 'v') {
            int x = robotPos.X;
            int y = robotPos.Y;
            while (warehouse[x][y] != '#' && warehouse[x][y] != '.') {
                x++;
            }
            if (warehouse[x][y] == '#') return;
            while (x != robotPos.X) {
                char temp = warehouse[x][y];
                warehouse[x][y] = warehouse[x - 1][y];
                warehouse[x - 1][y] = temp;
                x--;
            }
            robotPos = new Coord(robotPos.X + 1, robotPos.Y);
        }
    }

    public void attemptRobotMovePart2(char dir) {
        if (dir == '>') {
            int x = robotPos.X;
            int y = robotPos.Y;
            while (warehouse[x][y] != '#' && warehouse[x][y] != '.') {
                y++;
            }
            if (warehouse[x][y] == '#') return;
            while (y != robotPos.Y) {
                char temp = warehouse[x][y];
                warehouse[x][y] = warehouse[x][y - 1];
                warehouse[x][y - 1] = temp;
                y--;
            }
            robotPos = new Coord(robotPos.X, robotPos.Y + 1);
        }
        if (dir == '<') {
            int x = robotPos.X;
            int y = robotPos.Y;
            while (warehouse[x][y] != '#' && warehouse[x][y] != '.') {
                y--;
            }
            if (warehouse[x][y] == '#') return;
            while (y != robotPos.Y) {
                char temp = warehouse[x][y];
                warehouse[x][y] = warehouse[x][y + 1];
                warehouse[x][y + 1] = temp;
                y++;
            }
            robotPos = new Coord(robotPos.X, robotPos.Y - 1);
        }
        //I think  we need to create a list of positions in the vertical direction to check
        //ie. a box will add the 2 positions above it to be checked
        //this list can also be used in the 2nd step when moving everything
        if (dir == '^') {
            int x = robotPos.X;
            int y = robotPos.Y;
            List<Coord> positions = new List<Coord>();
            positions.Add(new Coord(x, y));
            int index = 0;
            while (index < positions.Count() && warehouse[positions[index].X][positions[index].Y] != '#') {
                if (warehouse[positions[index].X - 1][positions[index].Y] == '[') {
                    Coord box_edge_left = new Coord(positions[index].X - 1, positions[index].Y);
                    Coord box_edge_right = new Coord(positions[index].X - 1, positions[index].Y + 1);
                    if (!positions.Contains(box_edge_left) && !positions.Contains(box_edge_right)) {
                        positions.Add(box_edge_left);
                        positions.Add(box_edge_right);
                    }
                }
                if (warehouse[positions[index].X - 1][positions[index].Y] == ']') {
                    Coord box_edge_left = new Coord(positions[index].X - 1, positions[index].Y);
                    Coord box_edge_right = new Coord(positions[index].X - 1, positions[index].Y - 1);
                    if (!positions.Contains(box_edge_left) && !positions.Contains(box_edge_right)) {
                        positions.Add(box_edge_left);
                        positions.Add(box_edge_right);
                    }
                }
                if (index < positions.Count() && warehouse[positions[index].X - 1][positions[index].Y] == '#') return; //hit wall, can't move
                index++;
            }
            for (int i = positions.Count() - 1; i >= 0; i--) {
                Coord pos = positions[i];
                char temp = warehouse[pos.X][pos.Y];
                warehouse[pos.X][pos.Y] = warehouse[pos.X - 1][pos.Y];
                warehouse[pos.X - 1][pos.Y] = temp;
            }
            robotPos = new Coord(robotPos.X - 1, robotPos.Y);
        }
        if (dir == 'v') {
            int x = robotPos.X;
            int y = robotPos.Y;
            List<Coord> positions = new List<Coord>();
            positions.Add(new Coord(x, y));
            int index = 0;
            while (index < positions.Count() && warehouse[positions[index].X][positions[index].Y] != '#') {
                if (warehouse[positions[index].X + 1][positions[index].Y] == '[') {
                    Coord box_edge_left = new Coord(positions[index].X + 1, positions[index].Y);
                    Coord box_edge_right = new Coord(positions[index].X + 1, positions[index].Y + 1);
                    if (!positions.Contains(box_edge_left) && !positions.Contains(box_edge_right)) {
                        positions.Add(box_edge_left);
                        positions.Add(box_edge_right);
                    }
                }
                if (warehouse[positions[index].X + 1][positions[index].Y] == ']') {
                    Coord box_edge_left = new Coord(positions[index].X + 1, positions[index].Y);
                    Coord box_edge_right = new Coord(positions[index].X + 1, positions[index].Y - 1);
                    if (!positions.Contains(box_edge_left) && !positions.Contains(box_edge_right)) {
                        positions.Add(box_edge_left);
                        positions.Add(box_edge_right);
                    }
                }
                if (index < positions.Count() && warehouse[positions[index].X + 1][positions[index].Y] == '#') return; //hit wall, can't move
                index++;
            }
            for (int i = positions.Count() - 1; i >= 0; i--) {
                Coord pos = positions[i];
                char temp = warehouse[pos.X][pos.Y];
                warehouse[pos.X][pos.Y] = warehouse[pos.X + 1][pos.Y];
                warehouse[pos.X + 1][pos.Y] = temp;
            }
            robotPos = new Coord(robotPos.X + 1, robotPos.Y);
        }
        //Console.WriteLine();
        //printWarehouse();
        int test = 0;
    }

    public void attemptAllRobotMoves() {
        foreach (char c in robotMovements) {
            //Part 1
            //attemptRobotMove(c);
            //Part 2
            attemptRobotMovePart2(c);
        }
    }

    public int gpsTotal() {
        int total = 0;
        for (int i = 0; i < warehouse.Count(); i++) {
            for (int j = 0; j < warehouse[i].Count(); j++) {
                if (warehouse[i][j] == 'O') {
                    total += 100 * i + j;
                }
            }
        }

        return total;
    }

    public int gpsTotalPart2() {
        int total = 0;
        for (int i = 0; i < warehouse.Count(); i++) {
            for (int j = 0; j < warehouse[i].Count(); j++) {
                if (warehouse[i][j] == '[') {
                    total += 100 * i + j;
                }
            }
        }

        return total;
    }
    public void printWarehouse() {
        foreach (List<char> row in warehouse) {
            Console.WriteLine(new string(row.ToArray()));
        }
    }
}