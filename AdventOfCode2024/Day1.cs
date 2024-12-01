using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

public class Day1 {
    public static int TotalDistance() {
        int total_distance = 0;
        List<int> left = new List<int>();
        List<int> right = new List<int>();
        foreach (string s in File.ReadLines(@"inputs/day1_part1_input")) {
            //this splits by any amount of whitespace
            string[] line = s.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            left.Add(Int32.Parse(line[0]));
            right.Add(Int32.Parse(line[1]));
        }
        left.Sort();
        right.Sort();
        for (int i = 0; i < left.Count; i++) {
            total_distance += Math.Abs(left[i] - right[i]);
        }
        return total_distance;
    }
    public static int SimilarityScore() {
        int similarity_score = 0;
        List<int> left = new List<int>();
        //int, occurance_count
        Dictionary<int, int> right = new Dictionary<int, int>();
        foreach (string s in File.ReadLines(@"inputs/day1_part1_input")) {
            //this splits by any amount of whitespace
            string[] line = s.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            left.Add(Int32.Parse(line[0]));
            //add right value to dict, if already present then instead increment value by 1
            try {
                right.Add(Int32.Parse(line[1]), 1);
            }catch (ArgumentException e) {
                right[Int32.Parse(line[1])] += 1;
            }
        }
        foreach (int i in left) {
            if (right.ContainsKey(i)) {
                similarity_score += i * right[i];
            }
        }
        return similarity_score;
    }
}