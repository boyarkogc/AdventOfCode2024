public class Day22 {
    Dictionary<string, long> comboCount;

    public Day22(string inputPath) {
        comboCount = new Dictionary<string, long>();

        long total = 0;
        foreach (string s in File.ReadLines(inputPath)) {
            HashSet<string> seenCombos = new HashSet<string>();
            long number = long.Parse(s);
            Queue<long> recentFour = new Queue<long>();

            for (int i = 0; i < 2000; i++) {
                long nextNumber = nextRandom(number);
                if (recentFour.Count() == 4) recentFour.Dequeue();
                recentFour.Enqueue(nextNumber % 10 - number % 10);
                string recentFourString = string.Join(",",recentFour);
                if (!seenCombos.Contains(recentFourString)) {
                    seenCombos.Add(recentFourString);
                    if (!comboCount.ContainsKey(recentFourString)) comboCount[recentFourString] = 0;
                    comboCount[recentFourString] += nextNumber % 10;
                }
                number = nextNumber;
            }
            total += number;
        }
        Console.WriteLine(total);
        Console.WriteLine(comboCount.MaxBy(combo => combo.Value));
    }
    public long nextRandom(long random) {
        long nextRandom = 0;
        long t = random * 64;
        nextRandom = t ^ random;
        nextRandom %= 16777216;
        t = nextRandom / 32;
        nextRandom = t ^ nextRandom;
        nextRandom %= 16777216;
        t = nextRandom * 2048;
        nextRandom = t ^ nextRandom;
        nextRandom %= 16777216;

        return nextRandom;
    }
}