using Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PresentationModel
{
    public class Model : ModelAbstractAPI
    {
        public Model(int w, int h)
        {
            _height = h;
            _width = w;
            _logicAPI = LogicBoardAPI.CreateAPI();
            _ballsModel = new List<BallModelAPI>();
        }

        public override List<BallModelAPI> GetBallsModel()
        {
            return _ballsModel;
        }

        public override void StartSimulation()
        {
            _logicAPI.addBalls(_numOfBalls, 20);
            foreach (LogicBallAPI logicBall in _logicAPI.getBalls())
            {
                BallModelAPI ballModelAPI = BallModelAPI.CreateBallModel(logicBall);
                logicBall.changedPosition += ballModelAPI.UpdateBallModel;
                _ballsModel.Add(ballModelAPI);
            }
            _logicAPI.startMoving();


        }

        public override void StopSimulation()
        {

            _logicAPI.removeBalls();
            _ballsModel.Clear();

        }



    }
}
