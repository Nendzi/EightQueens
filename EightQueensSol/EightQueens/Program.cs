

namespace EightQueens
{
    class Program
    {
        ProgramLogic _programLogic;
        MatrixUtilityMethods _utilityMethods;
        static void Main()
        {
            Program program = new Program();
            Console.WriteLine("Welcome. Application will find all solutions for you.");
            program.Run();
            Console.ReadLine();
        }
        private void Run()
        {
            _utilityMethods = new MatrixUtilityMethods(8);
            _programLogic = new ProgramLogic(8,_utilityMethods);
            Matrix perm = _programLogic.FindAllPermutations();
            List<string> solutions = _programLogic.SearchForSolutions(perm);
            ShowSolutions(solutions);
        }
        private void ShowSolutions(List<string> solutions)
        {
            int count = 1;
            foreach (var solution in solutions)
            {
                if (count < 10)
                {
                    Console.Write(count + ".  ");
                }
                else
                {
                    Console.Write(count + ". ");
                }
                _utilityMethods.EncodeSolution(solution);
                _utilityMethods.VisualizeSolution(solution);
                Console.WriteLine();
                Console.WriteLine("_________________________________");
                count++;
            }
        }
    }
    
}
