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
            height = h;
            width = w;
            logicAPI = LogicBoardAPI.CreateAPI();
            ballsModel = new List<BallModelAPI>();
        }

        public override List<BallModelAPI> GetBallsModel()
        {
            return ballsModel;
        }

        public override void StartSimulation()
        {
            logicAPI.addBalls(_numOfBalls, 20);
            foreach (LogicBallAPI logicBall in logicAPI.getBalls())
            {
                BallModelAPI ballModelAPI = BallModelAPI.CreateBallModel(logicBall);
                logicBall.changedPosition += ballModelAPI.UpdateBallModel;
                ballsModel.Add(ballModelAPI);
            }
            logicAPI.startMoving();


        }

        public override void StopSimulation()
        {

            logicAPI.removeBalls();
            ballsModel.Clear();

        }



    }
}