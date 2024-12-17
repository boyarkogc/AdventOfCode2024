public class Day17 {
    public long reg_A;
    public long reg_B;
    public long reg_C;
    public int inst_pointer;
    public List<long> program;
    public List<long> output;

    public Day17(string inputPath) {
        string[] lines = File.ReadAllLines(inputPath);
        reg_A = long.Parse(lines[0].Split(": ")[1]);
        reg_B = long.Parse(lines[1].Split(": ")[1]);
        reg_C = long.Parse(lines[2].Split(": ")[1]);
        program = lines[4].Split(": ")[1].Split(",").Select(x => long.Parse(x)).ToList<long>();
        output = new List<long>();
        inst_pointer = 0;
    }

    public void runProgram(long count) {
        //part 2
        reg_A = count;
        reg_B = 0;
        reg_C = 0;
        inst_pointer = 0;
        output = new List<long>();
        //
        while (inst_pointer < program.Count() - 1) {
            long opcode = program[inst_pointer];
            long operand = program[inst_pointer + 1];
            switch (opcode) {
                case 0:
                    adv(operand);
                    break;
                case 1:
                    bxl(operand);
                    break;
                case 2:
                    bst(operand);
                    break;
                case 3:
                    jnz(operand);
                    break;
                case 4:
                    bxc(operand);
                    break;
                case 5:
                    out_(operand);
                    break;
                case 6:
                    bdv(operand);
                    break;
                case 7:
                    cdv(operand);
                    break;
                default:
                    throw new Exception("invalid instruction " + program[inst_pointer]);
            }
        }
        //Console.WriteLine(count + " " + string.Join(",", output.ToArray()));
    }

    public void adv(long operand) {
        reg_A = reg_A / (long) Math.Pow(2,comboOp(operand));
        inst_pointer += 2;
    }
    public void bxl(long operand) {
        reg_B = reg_B ^ operand;
        inst_pointer += 2;
    }
    public void bst(long operand) {
        reg_B = comboOp(operand) % 8;
        inst_pointer += 2;
    }
    public void jnz(long operand) {
        if (reg_A == 0) {
            inst_pointer += 2;
            return;
        }
        inst_pointer = (int) operand;
    }
    public void bxc(long operand) {
        reg_B = reg_B ^ reg_C;
        inst_pointer += 2;
    }
    public void out_(long operand) {
        output.Add(comboOp(operand) % 8);
        //Console.WriteLine(comboOp(operand) + " " + comboOp(operand) % 8 + " " + reg_A);
        inst_pointer += 2;
    }
    public void bdv(long operand) {
        reg_B = reg_A / (long) Math.Pow(2,comboOp(operand));
        inst_pointer += 2;
    }
    public void cdv(long operand) {
        reg_C = reg_A / (long) Math.Pow(2,comboOp(operand));
        inst_pointer += 2;
    }
    public long comboOp(long operand) {
        switch (operand) {
            case 0: case 1: case 2: case 3:
                return operand;
            case 4:
                return reg_A;
            case 5:
                return reg_B;
            case 6:
                return reg_C;
            default:
                throw new Exception("Invalid combo op " + operand);
        }
    }
}