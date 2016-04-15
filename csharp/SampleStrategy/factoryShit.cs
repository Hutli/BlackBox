using ArtificialNeuralNetwork;
using NeuralNetwork.GeneticAlgorithm.Evaluatable;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleStrategyStrategy
{
    public class EvalFact : IEvaluatableFactory
    {
        List<double> _data;
        public EvalFact(List<double> data)
        {
            _data = data;
        }
        public IEvaluatable Create(INeuralNetwork neuralNetwork)
        {
            return new Eval(_data, neuralNetwork);
        }
    }

    public class Eval : IEvaluatable
    {
        private readonly INeuralNetwork _neuralNet;
        private readonly List<double> _dataPoints;
        public Eval(List<double> dataPoints, INeuralNetwork neuralNet)
        {
            _neuralNet = neuralNet;
            _dataPoints = dataPoints;
        }
        public double GetEvaluation()
        {
            return profit;
        }

        double profit = 0;

        public void RunEvaluation()
        {
            profit = 0;
            for(int i = Runner.HISTORYSIZE; i < _dataPoints.Count() - 1; i++)
            {
                _neuralNet.SetInputs(_dataPoints.GetRange(i - Runner.HISTORYSIZE, Runner.HISTORYSIZE).ToArray());
                _neuralNet.Process(); // Do magic!
                double tomorrowsPrice = _neuralNet.GetOutputs()[0];
                double difference = Math.Abs(tomorrowsPrice - _dataPoints[i + 1]);
                if (difference != 0) {
                    profit += 1 / difference;
                }
            }
        }
    }
}
