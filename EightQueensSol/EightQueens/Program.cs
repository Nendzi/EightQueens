// See https://aka.ms/new-console-template for more information
using EightQueens;

Console.WriteLine("Welcome. Application wiil find all solutions for you.");

const int numberOfQueens = 8;
Matrix prem8 = FindAllPermutations(numberOfQueens);

Console.ReadLine();

static Matrix FindAllPermutations(int n)
{
    int numberOfPermutation = PermutationsCalculator.Factoriel(n);
    Matrix output = new Matrix(numberOfPermutation, n);
    output = PermutationsCalculator.Permutations(n);
    return output;
}
