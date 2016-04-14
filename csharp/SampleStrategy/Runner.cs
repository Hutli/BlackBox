using ArtificialNeuralNetwork;
using ArtificialNeuralNetwork.ActivationFunctions;
using ArtificialNeuralNetwork.Factories;
using ArtificialNeuralNetwork.WeightInitializer;
using NeuralNetwork.GeneticAlgorithm;
using NeuralNetwork.GeneticAlgorithm.Evaluatable;
using NeuralNetwork.GeneticAlgorithm.Evolution;
using NeuralNetwork.GeneticAlgorithm.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SampleStrategyStrategy
{
    class Runner
    {
        public const int HISTORYSIZE = 3;

        public static object MyEvaluatableFactory { get; private set; }

        public static List<double> LoadJson()
        {
            using (StreamReader r = new StreamReader("C:/Users/hutli/Documents/BlackBox/javascript/src/data/DANSKE.CO.json"))
            {
                string json = r.ReadToEnd();
                firstItem array = JsonConvert.DeserializeObject<firstItem>(json);
                IEnumerable<Item> arr = array.Prices;
                return arr.Select(itm => itm.Value).Take(5).ToList();
            }
        }

        public static void run()
        {
            var networkFactory = NeuralNetworkFactory.GetInstance();
            var evalWorkingSetFactory = EvalWorkingSetFactory.GetInstance();
            var evaluatableFactory = new EvalFact(LoadJson());
            var randomInit = new RandomWeightInitializer(new Random());
            var breederFactory = BreederFactory.GetInstance(networkFactory, randomInit);
            var mutatorFactory = MutatorFactory.GetInstance(networkFactory, randomInit);
            IGeneticAlgorithmFactory factory = GeneticAlgorithmFactory.GetInstance(networkFactory, evalWorkingSetFactory, evaluatableFactory, breederFactory, mutatorFactory);

            NeuralNetworkConfigurationSettings networkConfig = new NeuralNetworkConfigurationSettings
            {
                NumInputNeurons = HISTORYSIZE,
                NumOutputNeurons = 1,
                NumHiddenLayers = 2,
                NumHiddenNeurons = 3,
                SummationFunction = new SimpleSummation(),
                ActivationFunction = new TanhActivationFunction()
            };
            IGeneticAlgorithm evolver = factory.Create(networkConfig);
            evolver.RunSimulation();
        }

        static void main()
        {
            run();
        }

    }

    public class firstItem
    {
        public String Ticker;
        public List<Item> Prices;
    }

    public class Item
    {
        public DateTime Date;
        public double Value;
    }
}
