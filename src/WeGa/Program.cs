using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace WeGa
{
    class Program
    {


        static void Main(string[] args)
        {
            //ProgramShakspear.MainShakespear(args);
            new JSSP().Start();
            //Console.ReadLine();
        }

        public class JSSP
        {
            Ga<Dictionary<int, List<Of>>> ga;
            DNAConfig<Dictionary<int, List<Of>>> _config;
            Random rnd = new Random();
            List<int> _resources=new List<int>();
            int _maxOfCount;
            List<Ressource> resources = new List<Ressource>()
            {
                new Ressource(){Numero=215,Nom="Laser Bystronic",Nombre=1 },
                new Ressource(){Numero=300,Nom="Pliage",Nombre=2 },
                new Ressource(){Numero=400,Nom="Soudure MIG",Nombre=2 },
                new Ressource(){Numero=405,Nom="Soudure TIG",Nombre=2 },
                new Ressource(){Numero=1000,Nom="Peinture",Nombre=1 },
            };
            List<Of> _ofs = new List<Of>()
            {
                new Of(){Article="TOTO",Numero=34860,Operation=10,Resource=215,DebutPrevue=new DateTime(2020,4,15,8,0,0),TempsPrevus=420},
                new Of(){Article="TOTO",Numero=34860,Operation=20,Resource=300,DebutPrevue=new DateTime(2020,4,15,8,0,0),TempsPrevus=120},
                new Of(){Article="TOTO",Numero=34860,Operation=30,Resource=400,DebutPrevue=new DateTime(2020,4,15,8,0,0),TempsPrevus=60},
                new Of(){Article="TITI",Numero=34861,Operation=10,Resource=215,DebutPrevue=new DateTime(2020,4,11,8,0,0),TempsPrevus=50},
                new Of(){Article="TITI",Numero=34861,Operation=20,Resource=300,DebutPrevue=new DateTime(2020,4,11,8,0,0),TempsPrevus=45},
                new Of(){Article="TITI",Numero=34861,Operation=30,Resource=405,DebutPrevue=new DateTime(2020,4,11,8,0,0),TempsPrevus=86},
                new Of(){Article="TITI",Numero=34861,Operation=40,Resource=1000,DebutPrevue=new DateTime(2020,4,11,8,0,0),TempsPrevus=90},
                new Of(){Article="TUTU",Numero=34855,Operation=20,Resource=300,DebutPrevue=new DateTime(2020,4,11,8,0,0),TempsPrevus=12},

            };
            static Of EMPTY = new Of() { Article = "EMPTY", Numero = 0, Operation =0,TempsPrevus=0 };
            public void Start()
            {
                var _nbResource = (from o in _ofs group o by o.Resource into grp select new { Resource = grp.Key });
                if (_nbResource.Count() == 0) throw new ArgumentException("There is no Resource to planned. Aborting ...");
                var _maxOfInResource = (from o in _ofs group o by o.Resource into grp select new { Resource = grp.Key, Count = grp.Count() }).OrderByDescending(a => a.Count).FirstOrDefault();
                if (_maxOfInResource == null) throw new ArgumentException("There is nothing todo");
                _resources.AddRange(from o in _ofs group o by o.Resource into grp select grp.Key);
                _maxOfCount = _maxOfInResource.Count;
                _config = new DNAConfig<Dictionary<int, List<Of>>>(_nbResource.Count(), rnd, GetRandomOf, Fitness, GaSelection.Elitism, GaSelection.SUS, GaMutation.None, GaCrossover.Crossover);
                ga = new Ga<Dictionary<int, List<Of>>>(200, _config);

                 ga.Generate();
                // Console.WriteLine($"Count:{ga.Generation} - Content:");

            }

            Dictionary<int, List<Of>> GetRandomOf(DNA<Dictionary<int, List<Of>>> ofs)
            {
                var l = new List<int>();
                ofs.NonNullGenes.ForEach(nng => l.AddRange(nng.Keys.ToList()));
                var eligibleKeys = _resources.Except(l).ToList();
                var n = rnd.Next( eligibleKeys.Count());
                
                var currentResource = eligibleKeys[n];
                var result = new Dictionary<int, List<Of>>(_maxOfCount);
                result[currentResource] = new List<Of>();
                var i = 0;
                while (i < _maxOfCount)
                {
                    var eligibleOfs = (from of in _ofs where of.Resource == currentResource && (result[currentResource].Where(o => o.Value == of.Value).Count() == 0) select of).ToList();
                    if (eligibleOfs.Count() == 0)
                    {
                        var index =Math.Min(result[currentResource].Count(), rnd.Next(_maxOfCount));
                        result[currentResource].Insert(index, EMPTY);
                    }
                    else
                    {
                        var nn = rnd.Next(eligibleOfs.Count());
                        result[currentResource].Add(eligibleOfs[nn]);
                    }
                    i++;
                }
                

                return result;
               
            }
            float Fitness(int index)
            {
                float score = 0;
                int lastValue=0;
                var dna = ga.Population[index];
                foreach (var gene in dna.Genes)
                {
                    var g = gene.First();
                    for (int i = 0; i < g.Value.Count; i++)
                    {
                        if (g.Value[i].Value == 0) continue;
                        if (g.Value[i].Value > lastValue)
                        {
                            score+= g.Value[i].TempsPrevus;
                            lastValue = g.Value[i].Value;
                        }
                        else
                        {
                            score = 0.001f;
                            break;
                        }
                    }
                }
                /* DNA<Of> dna = ga.Population[index];
                 var resources = dna.Genes.ToList();
                 var result0 = (from r in resources group r by r.Resource into newgp select newgp).Count();
                 if (result0 > 1) return 0;*/

                return score;
            }

        }
       

        [DebuggerDisplay("of:{Numero}-{Operation}")]
        internal class Of : IComparable<Of>
        {
            public string Article { get; set; }
            public int Numero { get; set; }
            public int Operation { get; set; }

            public List<Of> Childs { get; set; }

            public int Resource { get; set; }

            public DateTime DebutPrevue { get; set; }

            public int TempsPrevus { get; set; }
            public int PreSpan { get; set; }
            public int Value => Numero * 1000 + Operation;
            public int CompareTo([AllowNull] Of other)
            {
                if (Value > other.Value)
                    return 1;
                else if (Value < other.Value)
                    return -1;
                else
                    return 0;

            }
        }
        internal struct Ressource
        {
            public int Numero { get; set; }
            public int Nombre { get; set; }
            public string Nom { get; set; }
        }
    }
}
