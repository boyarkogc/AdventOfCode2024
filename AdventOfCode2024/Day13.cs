public class Day13 {
    public struct Combo {
        public Combo(long a, long b) {
            A = a;
            B = b;
        }

        public override bool Equals(object obj)
        {
            if (obj is Combo other)
            {
                return A == other.A && B == other.B;
            }
            return false;
        }

        public long A { get; }
        public long B { get; }
    }

    public static long totalTokens(string inputPath) {
        long tokens = 0;
        string[] lines = File.ReadAllLines(inputPath);
        long a_x = 0;
        long a_y = 0;
        long b_x = 0;
        long b_y = 0;
        long prize_x = 0;
        long prize_y = 0;

        for (long i = 0; i < lines.Length; i++) {
            if (lines[i].Length == 0) continue;
            if (lines[i].Substring(0, 8) == "Button A") {
                string[] split = lines[i].Split();
                a_x = long.Parse(GetNumbers(split[2]));
                a_y = long.Parse(GetNumbers(split[3]));
            }else if (lines[i].Substring(0, 8) == "Button B") {
                string[] split = lines[i].Split();
                b_x = long.Parse(GetNumbers(split[2]));
                b_y = long.Parse(GetNumbers(split[3]));
            }else if (lines[i].Substring(0,5) == "Prize") {
                string[] split = lines[i].Split();
                prize_x = long.Parse(GetNumbers(split[1]));
                prize_y = long.Parse(GetNumbers(split[2]));

                //Part 1
                //List<Combo> comboList = combos(a_x, b_x, prize_x);
                //long mt = minimalTokens(comboList, a_y, b_y, prize_y);
                //if (mt != -1) tokens += mt;

                long mt = part2(a_x, a_y, b_x, b_y, prize_x + 10000000000000, prize_y + 10000000000000);
                //long mt = part2(a_x, a_y, b_x, b_y, prize_x, prize_y);
                if (mt != -1) tokens += mt;
                Console.WriteLine(i + " " + mt);
            }
        }

        return tokens;
    }

    public static List<Combo> combos(long a, long b, long prize) {
        List<Combo> comboList = new List<Combo>();
        long a_count = 0;
        long b_count_start = prize / b;

        for (long b_count = b_count_start; b_count >= 0; b_count--) {
            long result = a * a_count + b * b_count;
            while (result < prize) {
                a_count++;
                result = a * a_count + b * b_count;
            }
            if (result == prize) {
                comboList.Add(new Combo(a_count, b_count));
            }
        }
        return comboList;
    }
    //returns minimalTokens needed or -1 if no combos given in list are valid
    public static long minimalTokens(List<Combo> comboList, long a, long b, long prize) {
        //Combo combo = new Combo(-1, -1);
        long minimal_tokens = -1;
        foreach (Combo combo in comboList) {
            if (combo.A * a + combo.B * b == prize) {
                long tokens = combo.A * 3 + combo.B;
                if (minimal_tokens == -1 || tokens < minimal_tokens) {
                    minimal_tokens = tokens;
                }
            }
        }
        return minimal_tokens;
    }

    public static long part2(long a_x, long a_y, long b_x, long b_y, long prize_x, long prize_y) {
        long A = (prize_x * b_y - prize_y * b_x) % (a_x * b_y - a_y * b_x) == 0 ? (prize_x * b_y - prize_y * b_x) / (a_x * b_y - a_y * b_x) : -1;
        long B = (a_x * prize_y - a_y * prize_x) % (a_x * b_y - a_y * b_x) == 0 ? (a_x * prize_y - a_y * prize_x) / (a_x * b_y - a_y * b_x) : -1;
        return A != -1 && B != -1 ? A * 3 + B : -1;
    }

    public static bool isValidCombo(Combo c, long a, long b, long prize) {
        return c.A * a + c.B * b == prize;
    }

    private static string GetNumbers(string input) {
        return new string(input.Where(c => char.IsDigit(c)).ToArray());
    }

    public static bool isDivisible(long divisor, long d1, long d2) {
        for (long i = 0; i <= d1; i++) {
            if ((divisor - d1 * i) % d2 == 0) return true;
        }
        for (long i = 0; i <= d2; i++) {
            if ((divisor - d2 * i) % d1 == 0) return true;
        }
        return false;
    }
}