public class Day7 {

    public static long calibrationResult(string inputPath) {
        long total = 0;
        foreach (string line in File.ReadLines(inputPath)) {
            string[] split = line.Split(": ");
            long target = long.Parse(split[0]);
            List<long> remainingNumbers = new List<long>(Array.ConvertAll(split[1].Split(" "), s => long.Parse(s)));
            long current = remainingNumbers[0];
            remainingNumbers.RemoveAt(0);

            bool lineResult = validCombo(target, current, remainingNumbers);
            Console.WriteLine(line + " " + lineResult);
            if (lineResult) total += target;
        }
        return total;
    }
    static bool validCombo(long target, long current, List<long> remainingNumbers) {
        if (target == 292) {
            int test = 0;
        }

        if (current == target && remainingNumbers.Count() == 0) return true;
        if (current > target) return false;
        if (remainingNumbers.Count() == 0) return false;

        List<long> remainingNumbersCopy = new List<long>(remainingNumbers);
        long next = remainingNumbersCopy[0];
        remainingNumbersCopy.RemoveAt(0);
        if (validCombo(target, current + next, remainingNumbersCopy)) return true;
        if (validCombo(target, current * next, remainingNumbersCopy)) return true;
        //Part 2
        if (validCombo(target, long.Parse(current.ToString() + next.ToString()), remainingNumbersCopy)) return true;
        return false;
    }
}