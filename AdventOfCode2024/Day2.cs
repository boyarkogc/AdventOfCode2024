using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

//This code is pretty disgusting but it does work. 
public class Day2 {
    public static bool isSafe(List<int> report) {
        bool ascending = report[0] < report[1] ? true : false;
        bool mulligan = false;
        for (int i = 0; i < report.Count - 1; i++) {
            if (report[i] >= report[i + 1] && ascending) {
                report.RemoveAt(i+1);
                mulligan = true;
                break;
            }else if (report[i] <= report[i + 1] && !ascending) {
                report.RemoveAt(i+1);
                mulligan = true;
                break;
            }else if (Math.Abs(report[i] - report[i + 1]) > 3) {
                report.RemoveAt(i+1);
                mulligan = true;
                break;
            }
        }
        if (!mulligan) return true;

        ascending = report[0] < report[1] ? true : false;
        for (int i = 0; i < report.Count - 1; i++) {
            if (report[i] >= report[i + 1] && ascending) {
                return false;
            }else if (report[i] <= report[i + 1] && !ascending) {
                return false;
            }else if (Math.Abs(report[i] - report[i + 1]) > 3) {
                return false;
            }
        }
        return true;
    }
    public static int numSafeReports(string inputPath) {
        int safeCount = 0;
        foreach (string s in File.ReadLines(inputPath)) {
            //if the problem number is the first number, the logic in isSafe won't catch it, 
            //so we reverse the list and send it in again to account for that possibility
            //I have 2 different lists here because the list can be modified in isSafe, which would affect the second pass with the reversed list if it were used again
            //There is a better way to do this I'm sure. We should be able to use just one list for starters.
            List<int> reportLine = new List<int>(Array.ConvertAll(s.Split(' '), int.Parse));
            List<int> reportLine2 = new List<int>(Array.ConvertAll(s.Split(' '), int.Parse));
            int safeResult = isSafe(reportLine) ? 1 : 0;
            if (safeResult == 0) {
                reportLine2.Reverse();
                safeResult = isSafe(reportLine2) ? 1 : 0;
                if (safeResult == 1) {

                }
            }
            safeCount += safeResult;
            //Console.WriteLine(s + "___" + safeCount);
        }
        return safeCount;
    }
}