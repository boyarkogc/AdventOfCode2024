//Worst code of all time ngl. Well, part 1 is fine, but part 2 is an ugly brute force.
public class Day6 {
    //Note: indexing is row, col
    public List<List<char>> grid;
    public int location_row;
    public int location_col;
    public Day6(string inputPath) {
        grid = new List<List<char>>();
        foreach (string s in File.ReadLines(inputPath)) {
            grid.Add(s.ToCharArray().ToList<char>());
            if (s.IndexOf("^") != -1) {
                location_row = grid.Count() - 1;
                location_col = s.IndexOf("^");
            }
        }
    }

    public int countVisitedPositions() {
        int count = 0;
        foreach (List<char> row in grid) {
            foreach (char c in row) {
                if (c == 'X') {
                    Console.WriteLine(count);
                    count++;
                }
            }
        }
        return count;
    }

    public int countLoopPositions() {
        int loop_count = 0;
        List<List<char>> original_grid = new List<List<char>>();
        foreach (List<char> r in grid) {
            original_grid.Add(new List<char>(r));
        }

        int original_row = location_row;
        int original_col = location_col;
        for (int i = 0; i < grid.Count(); i++) {
            for (int j = 0; j < grid[i].Count(); j++) {
                if (grid[i][j] == '.') {
                    //foreach (List<char> c in grid) {
                    //    Console.WriteLine(string.Join(" ", c));
                    //}
                    //Console.WriteLine();
                    grid[i][j] = '#';
                    if (CheckIfLoop()) {
                        loop_count++;
                        Console.WriteLine("Found a loop");
                        //foreach (List<char> c in grid) {
                        //    Console.WriteLine(string.Join(" ", c));
                        //}
                        //Console.WriteLine();
                    }

                }
                grid = new List<List<char>>();
                foreach (List<char> r in original_grid) {
                    grid.Add(new List<char>(r));
                }

                location_row = original_row;
                location_col = original_col;
                Console.WriteLine("Row " + i + " Col " + j);
            }
        }
        return loop_count;
    }

    public bool CheckIfLoop() {
        rotateRight(grid);
        bool done = false;
        while (!done) {
            for (int col = location_col; col < grid[location_row].Count(); col++) {

                if (grid[location_row][col] == '.' || grid[location_row][col] == '^') {
                    grid[location_row][col] = '1';
                    location_col = col;
                }else if (grid[location_row][col] == '1') {
                    grid[location_row][col] = '2';
                    location_col = col;
                }else if (grid[location_row][col] == '2') {
                    grid[location_row][col] = '3';
                    location_col = col;
                }else if (grid[location_row][col] == '3') {
                    grid[location_row][col] = '4';
                    location_col = col;
                }else if (grid[location_row][col] == '4') {
                    return true; //we're in a loop
                }else {
                    rotateRight(grid);
                    rotateRight(grid);
                    rotateRight(grid);
                    done = false;
                    break;
                }
                done = true;
            }
        }
        return false;
    }
    public void markPath() {
        rotateRight(grid);
        bool done = false;
        while (!done) {
            for (int col = location_col; col < grid[location_row].Count(); col++) {
                //foreach (List<char> c in grid) {
                //    Console.WriteLine(string.Join(" ", c));
                //}
                //Console.WriteLine(location_row + " " + location_col);

                if (grid[location_row][col] != '#') {
                    grid[location_row][col] = 'X';
                    location_col = col;
                }else {
                    rotateRight(grid);
                    rotateRight(grid);
                    rotateRight(grid);
                    done = false;
                    break;
                }
                done = true;
            }
            //foreach (List<char> c in grid) {
            //    Console.WriteLine(string.Join(" ", c));
            //}
            //Console.WriteLine(location_row + " " + location_col);
        }
    }

    public void rotateRight(List<List<char>> grid) {
        for (int i = 0; i < grid.Count(); i++) {
            for (int j = i; j < grid[i].Count(); j++) {
                char temp_char = grid[i][j];
                grid[i][j] = grid[j][i];
                grid[j][i] = temp_char;
            }
        }
        for (int i = 0; i < grid.Count(); i++) {
            grid[i].Reverse();
        }
        int temp_loc = location_row;
        location_row = location_col;
        location_col = temp_loc;
        location_col = grid[location_row].Count() - location_col - 1;
    }
}