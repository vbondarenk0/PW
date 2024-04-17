using Data;
using System.Diagnostics;
namespace Logic
{
    public class LogicBoard : LogicBoardAPI
    {
        private int height { get; set; }
        private int width { get; set; }
        private List<LogicBallAPI> balls { get; set; }
        internal BoardAPI dataAPI;

        private int maxX = 370;
        private int maxY = 370;


        public LogicBoard(BoardAPI boardAPI)
        {
            this.height = boardAPI.Height;
            this.width = boardAPI.Width;
            balls = new List<LogicBallAPI>();
            ballAPIs = new List<BallAPI>();
            dataAPI = boardAPI;

        }
        public override List<LogicBallAPI> getBalls()
        {
            return balls;
        }

        private List<BallAPI> ballAPIs;

        public override void addBalls(int ballsQuantity, int ballRadius)
        {
            for (int i = 0; i < ballsQuantity; i++)
            {
                Random random = new Random();
                float x = random.Next(0, height - ballRadius);
                float y = random.Next(0, width - ballRadius);

                int SpeedX;
                do
                {
                    SpeedX = random.Next(-3, 3);
                } while (SpeedX == 0);

                int SpeedY;
                do
                {
                    SpeedY = random.Next(-3, 3);
                } while (SpeedY == 0);

                BallAPI dataBall = dataAPI.AddBall(x, y, SpeedX, SpeedY, ballRadius);
                LogicBall ball = new LogicBall(dataBall.getPosition().X, dataBall.getPosition().Y, ballRadius);


                dataBall.ChangedPosition += ball.UpdateBall;
                dataBall.ChangedPosition += checkBorderCollision;

                ballAPIs.Add(dataBall);

                balls.Add(ball);
            }
        }

        public int GetHeight()
        {
            return height;
        }

        public void SetHeight(int height)
        {
            this.height = height;
        }

        public int GetWidth()
        {
            return width;
        }

        public void SetWidth(int width)
        {
            this.width = width;
        }

        public override void checkBorderCollision(Object s, DataEventArgs e)
        {
            BallAPI ball = (BallAPI)s;
            bool isCorrectInX = (ball.getPosition().X + ball.getR() + ball.getSpeed().X < maxX /*- 2 * ball.getR()*/) && (ball.getPosition().X + ball.getSpeed().X /*- ball.getR()*/ > 0);
            bool isCorrectInY = (ball.getPosition().Y + ball.getR() + ball.getSpeed().Y < maxY /*- 2 * ball.getR()*/) && (ball.getPosition().Y + ball.getSpeed().Y /*- ball.getR()*/ > 0);
            if (!isCorrectInX)
            {
                ball.setSpeed(-ball.getSpeed().X, ball.getSpeed().Y);
            }
            if (!isCorrectInY)
            {
                ball.setSpeed(ball.getSpeed().X, -ball.getSpeed().Y);
            }
        }

        public override void removeBalls()
        {
            isMoving = false;
            balls.Clear();
        }
        public override void startMoving()
        {
            isMoving = true;
            Task.Run(() =>
            {
                while (isMoving)
                {
                    foreach (BallAPI ballAPI in ballAPIs)
                    {
                        ballAPI.MakeMove(maxX, maxY);
                        Thread.Sleep(3);
                    }
                }
            });
        }
    }

}