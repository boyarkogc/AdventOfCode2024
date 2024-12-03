using System.Text.RegularExpressions;

public class Day3 {
    public static void test(string inputPath) {
        //string inputString = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";
        string regexPattern = @"mul\(\d+,\d+\)"; // Matches mul(<digits>,<digits>)
        Regex regex = new Regex(regexPattern);
        int total = 0;

        //string s = inputString;
        string s = File.ReadAllText(inputPath);
        string[] mulSplit = s.Split(@"do()");
        foreach (string segment in mulSplit) {
            string[] segmentSplit = segment.Split(@"don't");
            MatchCollection matches = regex.Matches(segmentSplit[0]);

            foreach (Match match in matches) 
            {
                string[] split = match.ToString().Split(",");
                split[0] = Regex.Replace(split[0], "[^0-9]", ""); //remove all non-digits
                split[1] = Regex.Replace(split[1], "[^0-9]", ""); //remove all non-digits
                total += Int32.Parse(split[0]) * Int32.Parse(split[1]);
                Console.WriteLine(split[0] + " " + split[1]);
            }
            Console.WriteLine(total);
        }
    }
}