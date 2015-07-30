﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtificialNeuralNetwork;
using BasicGame;
using Logging;

namespace UnsupervisedTraining
{

    public class TrainingSession
    {
        private int _sessionNumber;
        private NeuralNetwork _nn;
        private Game _game;

        public TrainingSession(NeuralNetwork nn, Game game, int sessionNumber)
        {
            _nn = nn;
            _game = game;
            _sessionNumber = sessionNumber;
        }

        public void Run()
        {
            LoggerFactory.GetLogger().Log(LogLevel.Debug, string.Format("Starting training session {0}",_sessionNumber));
            Array values = Enum.GetValues(typeof(MoveDirection));

                Game g = new Game(10, 10, 300);
                while (!_game.IsGameWon() && !_game.IsGameLost())
                {
                    MoveDirection dirToMove = MoveDirection.DOWN;
                    double highestProb = double.MinValue;
                    foreach (MoveDirection val in values)
                    {
                        double distance = _game.GetDistanceToClosestDot(val, _game.CurrentCoord, new List<Tuple<int, int>>());
                        _nn.setInputs(new[] { distance });
                        _nn.calculate();
                        double probability = _nn.GetOutput();
                        if (probability > highestProb)
                        {
                            dirToMove = val;
                            highestProb = probability;
                        }
                    }
                    _game.UseTurn(dirToMove);
                }
                LoggerFactory.GetLogger().Log(LogLevel.Debug, string.Format("Stopping training session {0}", _sessionNumber));
        }

        public double GetSessionEvaluation()
        {
            if (!_game.IsGameWon() && !_game.IsGameLost())
            {
                throw new NotSupportedException("GetSessionEvaluation is not supported when game is not finished");
            }
            double result = _game.MovesLeft + (_game.Width * _game.Length) - _game.GetDotsLeft();
            return result;
        }



    }
}