using System;
using System.Collections.Generic;
using System.Text;

namespace WeGa
{
    class ProgramShakspear
    {

        static string target = "To be, or not to be, that is the question";
        static string validChars = "azertyuiopqsdfghjklmwxcvbnAZERTYUIOPQSDFGHJKLMWXCVBN,;:!?./ ";
        static Ga<char> ga;
        static Random rnd = new Random();
        static DNAConfig<char> _config;
        public static void MainShakespear(string[] args)
        {
            _config = new DNAConfig<char>(target.Length, rnd, getRandomCharacter, Fitness, GaSelection.Elitism, GaSelection.SUS,GaMutation.Mutate, GaCrossover.Crossover);

            ga = new Ga<char>(200, _config);

            while (ga.BestFitness < 1)
            {
                ga.Generate();
                Console.WriteLine($"Count:{ga.Generation} - Content:{new string(ga.BestGenes)}");
            }
            Console.WriteLine($"Count:{ga.Generation} - Content:{new string(ga.BestGenes)}");
            Console.WriteLine("Hello World!");
        }

        static char getRandomCharacter(DNA<char> chars)
        {
            int i = rnd.Next(validChars.Length);
            return validChars[i];
        }

        static float Fitness(int index)
        {
            float score = 0;
            DNA<char> dna = ga.Population[index];

            for (int i = 0; i < dna.Genes.Length; i++)
            {
                if (dna.Genes[i] == target[i])
                {
                    score += 1;
                }
            }

            score /= target.Length;

            score = (float)(Math.Pow(2, score) - 1) / (2 - 1);

            return score;
        }

        static void Mutate(DNA<char> dna, float mutationRate)
        {
            for (int i = 0; i < dna.Length; i++)
            {
                if (dna.Configuration.Random.NextDouble() < mutationRate)
                {
                    dna[i] = dna.Configuration.CreateGenefunc(dna);
                }

            }
        }
    }
}
