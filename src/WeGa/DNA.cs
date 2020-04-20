using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace WeGa
{
    public readonly struct DNAConfig<T>
    where T : new()
    {
        public DNAConfig(int size, Random random, Func<DNA<T>,T> createGeneFunc, Func<int, float> fitnessFunc, Func<Ga<T>, int, List<DNA<T>>> elitismFunc, Func<Ga<T>, DNA<T>> selectParentFunc, Action<DNA<T>, float> mutationFunc, Func<DNA<T>, DNA<T>, DNA<T>> crossoverFunc)
            =>
            (Size, Random,CreateGenefunc,FitnessFunc,ElitismFunc, SelectParentFunc, MutationFunc,CrossoverFunc) = 
            (size, random,createGeneFunc,fitnessFunc,elitismFunc, selectParentFunc, mutationFunc,crossoverFunc);


        public int Size { get;}
        public Random Random { get; }
        internal Func<DNA<T>,T> CreateGenefunc { get; }
        internal Func<int, float> FitnessFunc { get; }

        internal Func<Ga<T>, int, List<DNA<T>>> ElitismFunc { get; }
        internal Action<DNA<T>, float> MutationFunc { get; }
        internal Func<DNA<T>, DNA<T>, DNA<T>> CrossoverFunc { get; }
        internal Func<Ga<T>, DNA<T>> SelectParentFunc { get; }
    }

    
    public class DNA<T>:IComparable<DNA<T>>
        where T : new()
    {
        private readonly DNAConfig<T> _config;
        public DNA(DNAConfig<T> config, bool initializeGene = true)
        {
            _config = config;
            Genes = new T[config.Size];

            Fitness = 0;
            if (initializeGene)
                this.RandomizeGene();
        }
        internal DNAConfig<T> Configuration => _config;
        public T[] Genes { get; }
        public int Length => Genes?.Length ?? 0;
        public int LengthNotNull => (from g in Genes.ToList() where g != null select g).Count();
        public List<T> NonNullGenes=> (from g in Genes.ToList() where g != null select g).ToList();
        
        public float Fitness{get; set;}
        public T this[int index]
        {
            get => Genes[index];
            set => Genes[index] = value;
        }
        public static bool operator >=(DNA<T> dna, DNA<T> other) => dna.CompareTo(other) >= 0;
        public static bool operator <=(DNA<T> dna, DNA<T> other) => dna.CompareTo(other) <= 0;
        public static bool operator >(DNA<T> dna, DNA<T> other) => dna.CompareTo(other) > 0;
        public static bool operator <(DNA<T> dna, DNA<T> other) => dna.CompareTo(other) < 0;
        public static DNA<T> operator *(DNA<T> dna, DNA<T> other) => dna.Crossover(other);
        public static int operator ==(DNA<T> dna, DNA<T> other) => dna.CompareTo(other);
        public static int operator !=(DNA<T> dna, DNA<T> other) => dna.CompareTo(other)*-1;

        public static bool operator >=(DNA<T> dna, float other) => dna.Fitness >= other;
        public static bool operator <=(DNA<T> dna, float other) => dna.Fitness <= other;
        public static bool operator >(DNA<T> dna, float other) => dna.Fitness > other;
        public static bool operator <(DNA<T> dna, float other) => dna.Fitness < other;
        public static bool operator ==(DNA<T> dna, float other) => dna.Fitness == other;
        public static bool operator !=(DNA<T> dna, float other) => dna.Fitness != other;

        public int CompareTo([AllowNull] DNA<T> other)
        {
            if (Fitness > other.Fitness)
            {
                return -1;
            }
            else if (Fitness < other.Fitness)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            throw new NotImplementedException();
        }

        public override int GetHashCode() => Fitness.GetHashCode();
        
    }

    internal static class DNAExtensions
    {
        public static void RandomizeGene<T>(this DNA<T> dna)
            where T : new()     
        {
            for (int i = 0; i < dna.Length; i++)
            {
                dna[i] = dna.Configuration.CreateGenefunc(dna);
            }
        }
        public static float CalculateFitness<T>(this DNA<T> dna, int index)
            where T : new()
        {
            dna.Fitness = dna.Configuration.FitnessFunc?.Invoke(index) ?? 0;
            return dna.Fitness;
        }

        public static DNA<T> Crossover<T>(this DNA<T> dna, DNA<T> other)
            where T : new()
        {

            var child = new DNA<T>(dna.Configuration, initializeGene: false);
            for (int i = 0; i < child.Length; i++)
            {
                child[i] = dna.Configuration.Random.NextDouble() <= 0.5 ? dna[i] : other[i];
            }
            return child;
        }
        public static void Mutate<T>(this DNA<T> dna, float mutationRate)
            where T : new()
        {
            dna.Configuration.MutationFunc?.Invoke(dna, mutationRate);
            
        }

        public static bool IsNull<T>(this DNA<T> dna)
            where T:new()
            => dna?.Equals(null) ?? true;
    }
}
