using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;
using System.Threading;
using System.Runtime.InteropServices.ComTypes;

namespace WeGa
{
    public class Ga<T>
        where T : new()
    {

        private List<DNA<T>> _newPopulation;
        private readonly DNAConfig<T> _config;
        //private readonly Func<Ga<T>,int, List<DNA<T>>> _elitismFunc;
        //private readonly Action<DNA<T>, float> _mutationFunc;
        //private readonly Func<DNA<T>, DNA<T>, DNA<T>> _crossoverFunc;

        private float _fitnessSum;
        private bool _enabled;
        public Ga(int populationSize, DNAConfig<T> config, int elitism = 5, float mutationRate = 0.01f)
        {
            _config = config;
            // _elitismFunc = elitismFunc;
            // _crossoverFunc = crossoverFunc;
            // _mutationFunc = mutationFunc;

            Generation = 0;
            Elitism = elitism > 0 ? elitism : 5;
            MutationRate = mutationRate > 0 ? mutationRate : 0.01f;
            Population = new List<DNA<T>>();
            _newPopulation = new List<DNA<T>>();
            BestGenes = new T[_config.Size];
            for (int i = 0; i < populationSize; i++)
            {
                Population.Add(new DNA<T>(config));
            }
        }

        internal List<DNA<T>> Population { get; private set; }
        public int Generation { get; private set; }
        public DNAConfig<T> Configuration => _config;
        public float MutationRate { get; set; }
        public int Elitism { get; set; }

        public float BestFitness { get; private set; }
        public T[] BestGenes { get; private set; }
        public float FitnessSum => _fitnessSum;
        public void Generate(int numNewDNA = 0, bool crossoverNewDNA = false)
        {
            if (_enabled) return;
            int finalCount = Population.Count + numNewDNA;

            if (finalCount <= 0)
            {
                return;
            }

            if (Population.Count > 0)
            {
                CalculateFitness();

                Population.Sort();
                var best = Population[0];
                BestFitness = best.Fitness;
                best.Genes.CopyTo(BestGenes, 0);
            }
            _newPopulation.Clear();

            if (BestFitness < 1.0f)
            {
                _newPopulation.AddRange(Configuration.ElitismFunc?.Invoke(this, Elitism));
                for (int i = _newPopulation.Count; i < Population.Count; i++)
                {
                    if (i < Population.Count || crossoverNewDNA)
                    {
                        DNA<T> father = Configuration.SelectParentFunc?.Invoke(this) ?? null;
                        DNA<T> mother = Configuration.SelectParentFunc?.Invoke(this) ?? null;
                        if (father.IsNull() || mother.IsNull())
                        {
                            _enabled = false;
                            break;
                        }

                        DNA<T> child = Configuration.CrossoverFunc?.Invoke(father, mother);

                        Configuration.MutationFunc?.Invoke(child, MutationRate);
                       // child.Mutate(MutationRate);

                        _newPopulation.Add(child);
                    }
                    else
                    {
                        _newPopulation.Add(new DNA<T>(_config, true));
                    }
                }
            }
            List<DNA<T>> tmpList = Population;
            Population = _newPopulation;
            _newPopulation = tmpList;

            Generation++;
        }

        private void CalculateFitness()
        {
            _fitnessSum = 0;
            for (int i = 0; i < Population.Count; i++)
                _fitnessSum += Population[i].CalculateFitness(i);

        }

        /* private DNA<T> ChooseParent()
         {
             double n = _config.Random.NextDouble() * _fitnessSum;
             for (int i = 0; i < Population.Count; i++)
             {
                 if (n < Population[i].Fitness)
                     return Population[i];
                 n -= Population[i].Fitness;
             }
             return default(DNA<T>);
         }*/


    }

    public static class GaSelection
    {
        /// <summary>
        /// Elitism truncation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ga"></param>
        /// <param name="elitism"></param>
        /// <returns></returns>
        public static List<DNA<T>> Elitism<T>(this Ga<T> ga, int elitism)
             where T : new()
             => ga.Population.Take(elitism).ToList();

        /// <summary>
        /// Roulette Wheel Selection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ga"></param>
        /// <returns></returns>
        public static DNA<T> RWS<T>(this Ga<T> ga)
            where T : new()
        {

            var iSum = 0.0f;
            var j = 0;
            double n = ga.Configuration.Random.NextDouble() * ga.FitnessSum;
            do
            {
                iSum += ga.Population[j].Fitness;
                j++;
            } while (iSum < n && j - 1 < ga.Population.Count);
            return ga.Population[j - 1];

        }
        /// <summary>
        /// Stochastic Universal Sampling
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ga"></param>
        /// <returns></returns>
        public static DNA<T> SUS<T>(this Ga<T> ga)
            where T : new()
        {
            var mean = ga.FitnessSum / ga.Population.Count;
            var n = ga.Configuration.Random.NextDouble();
            var sum = ga.Population[0].Fitness;
            var delta = n * mean;
            var j = 0;
            DNA<T> selected = default(DNA<T>);
            do
            {
                if (delta < sum)
                {
                    selected = ga.Population[j];
                    break;
                }
                else
                {
                    j++;
                    sum += ga.Population[j].Fitness;
                }
            } while (j < n);
            if (selected.IsNull())
            {
                selected = ga.Population[(int)n * ga.Population.Count];
            }
            return selected;
        }

        /// <summary>
        /// Linear Rank Selection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ga"></param>
        /// <returns></returns>
        public static DNA<T> LRS<T>(Ga<T> ga)
            where T : new()
        {
            DNA<T> selected = default(DNA<T>);
            Func<int, float> rank = i => i / (ga.Population.Count * (ga.Population.Count - 1));
            var v = 1 / (ga.Population.Count - 2.001);
            for (int i = 0; i < ga.Population.Count; i++)
            {
                var n = ga.Configuration.Random.NextDouble() * v;
                for (int j = 0; j < n; j++)
                {
                    if (rank(j) < n)
                    {
                        selected = ga.Population[j];
                        break;
                    }

                }
            }
            return selected;
        }

        /// <summary>
        /// Exponential Rank Selection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ga"></param>
        /// <returns></returns>
        public static DNA<T> ERS<T>(this Ga<T> ga)
            where T : new()
        {
            Func<int, float> cfunc = n => (n * 2 * (n - 1)) / (6 * (n - 1) + n);
            var c = cfunc(ga.Population.Count);
            var min = (1.0f / 9) * c;
            var max = 2.0f / c;
            Func<float, double> rank = i => 1.0 * Math.Exp(-i / c);
            DNA<T> selected = default(DNA<T>);
            for (int i = 0; i < ga.Population.Count; i++)
            {
                var n = ga.Configuration.Random.NextDouble() * (Math.Max(max, min) - Math.Min(max, min)) + Math.Min(max, min);
                for (int j = 0; j < ga.Population.Count; j++)
                {
                    if (rank(j) <= n)
                    {
                        selected = ga.Population[j];
                        break;
                    }
                }

            }
            return selected;
        }
        /// <summary>
        /// Tournament Selection NOT WORKING!!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ga"></param>
        /// <returns></returns>
        public static DNA<T> TOS<T>(this Ga<T> ga)
            where T : new()
        {
            var v = ga.Configuration.Random.Next(0, ga.Population.Count);

            return ga.Population.Skip(v).First();
        }
    }

    public static class GaCrossover
    {
        public static DNA<T> Crossover<T>(DNA<T> father, DNA<T> mother)
            where T : new()
            => father * mother;
    }

    public static class GaMutation
    {
        public static void None<T>(DNA<T> value, float mutationRate)
            where T : new(){ }
       public static void Mutate<T>(DNA<T> value, float mutationRate)
            where T:new()
        {
            for (int i = 0; i < value.Length; i++)
            {
                if (value.Configuration.Random.NextDouble() < mutationRate)
                {
                    value[i] = value.Configuration.CreateGenefunc(value);
                    break;
                }

            }
        }
    }

}
