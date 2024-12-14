// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
//Console.WriteLine(Day1.TotalDistance());
//Console.WriteLine(Day1.SimilarityScore());
//Console.WriteLine(Day2.numSafeReports(@"inputs/day2_part1_input"));
//Day3.test(@"inputs/day3_input");
//Console.WriteLine(Day3.test());
//Console.WriteLine(Day4.xmasCount(@"inputs/day4_input"));
//Console.WriteLine(Day4.xmasCountPart2(@"inputs/day4_input"));
//Console.WriteLine(Day5.Part1(@"inputs/day5_input"));
//Console.WriteLine(Day5.Part2(@"inputs/day5_input"));
//Day6 six = new Day6(@"../../../inputs/day6_input");
//six.markPath();
//Console.WriteLine(six.countVisitedPositions());
//Console.WriteLine(six.countLoopPositions());
//Console.WriteLine(Day7.calibrationResult(@"../../../inputs/day7_input"));
//Console.WriteLine(Day8.uniqueLocations(@"../../../inputs/day8_input"));
//Day9 nine = new Day9(@"../../../inputs/day9_input");
//nine.rearrange2();
//Console.WriteLine(nine.checksum());
//Day10 ten = new Day10(@"../../../inputs/day10_input");
//Console.WriteLine(ten.totalPathCount());
//Day12 twelve = new Day12(@"../../../inputs/day12_input");
//Console.WriteLine(twelve.totalFenceCost());
//Console.WriteLine(Day13.totalTokens(@"../../../inputs/day13_input"));
Day14 fourteen = new Day14();
int j = 70;
for (int i = 0; i < 500; i++) {
    fourteen.loadAndMoveRobots(@"../../../inputs/day14_input", j);
    j+=101;
}