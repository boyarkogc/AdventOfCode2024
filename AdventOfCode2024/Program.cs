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
// Day14 fourteen = new Day14();
// int j = 70;
// for (int i = 0; i < 500; i++) {
//     fourteen.loadAndMoveRobots(@"../../../inputs/day14_input", j);
//     j+=101;
// }
// Day15 fifteen = new Day15(@"../../../inputs/day15_input");
// fifteen.attemptAllRobotMoves();
// fifteen.printWarehouse();
// Console.WriteLine(fifteen.gpsTotalPart2());
// Day16 sixteen = new Day16(@"../../../inputs/day16_input");
// sixteen.fillMazeCost(sixteen.start, sixteen.start.Facing, 0);
// sixteen.findSeats();
// sixteen.printSeats();
// Console.WriteLine(sixteen.minCost);
// Console.WriteLine(sixteen.seats.Count());
// Day17 seventeen = new Day17(@"../../../inputs/day17_input");
// //seventeen.runProgram();
// long i = 0;
// while(true) {
//     seventeen.runProgram(i);
//     if (seventeen.program.Count() == seventeen.output.Count() && seventeen.program.SequenceEqual(seventeen.output)) {
//         Console.WriteLine("Found: " + i);
//         System.Environment.Exit(0);
//     }
//     //Console.WriteLine(string.Join(",",seventeen.program.Slice(seventeen.program.Count() - seventeen.output.Count(), seventeen.output.Count()).ToArray()) +" "+ string.Join(",", seventeen.output));
//     if (seventeen.program.Slice(seventeen.program.Count() - seventeen.output.Count(), seventeen.output.Count()).SequenceEqual(seventeen.output)) {
//         Console.WriteLine("hi " + i + " " + string.Join(",", seventeen.output));
//         i*=8;
//     }else {
//         i++;
//     }

// }
// Day18 eighteen = new Day18(@"../../../inputs/day18_input");
// eighteen.fillgridCost();
// Console.WriteLine(eighteen.minCost);
// Day19 nineteen = new Day19(@"../../../inputs/day19_input");
// //Console.WriteLine(nineteen.numDesignsPossible());
// Console.WriteLine(nineteen.totalDesignsPossibleWays());
// Day20 twenty = new Day20(@"../../../inputs/day20_input");
// Console.WriteLine(twenty.numCheatsPart2());


// Day21 twentyone = new Day21(@"../../../inputs/day21_input");
// // Console.WriteLine(string.Join("",twentyone.codeToNumpadSeq("029A")));
// Console.WriteLine(string.Join("",twentyone.numpadSeqToDirSeq(twentyone.codeToNumpadSeq("029A"))));
// Console.WriteLine(string.Join("",twentyone.dirSeqTodirSeq(twentyone.numpadSeqToDirSeq(twentyone.codeToNumpadSeq("029A")))));

// Console.WriteLine(twentyone.totalComplexities());

Day22 twentytwo = new Day22(@"../../../inputs/day22_input");
//Console.WriteLine(twentytwo.nextRandom(123));


// Day23 twentythree = new Day23(@"../../../inputs/day23_input");
// //Console.WriteLine(twentythree.groupsOfThreeStartsWith("t"));
// Console.WriteLine(twentythree.maxSet());