using System.Diagnostics;
using System.Numerics;

namespace Data
{
    public class Ball : BallAPI
    {
        private Vector2 position { get; set; }
        private Vector2 speed { get; set; }
        private int r { get; set; }

        private int maxX = 370;
        private int maxY = 370;

        public override Vector2 getPosition()
        {
            return position;
        }
        public override void setPosition(float x, float y)
        {
            position = new Vector2(x, y);
        }
        public override Vector2 getSpeed()
        {
            return speed;
        }

        public override void setSpeed(float x, float y)
        {
            speed = new Vector2(x, y);
        }

        public override int getR()
        {
            return r;
        }
        public override event EventHandler<DataEventArgs> ChangedPosition;

        public Ball(Vector2 position, int r, Vector2 speed)
        {
            this.speed = speed;
            this.position = position;
            this.r = r;
        }
        public Ball(Vector2 position, int r)
        {
            speed = Vector2.Zero;
            this.position = position;
            this.r = r;
        }

        public override void MakeMove(int width, int height)
        // (0, 350), (0,350)

        {
            bool isCorrectInX = (this.getPosition().X + this.getR() + this.getSpeed().X < maxX /*- 2 * getR()*/) && (this.getPosition().X + this.getSpeed().X /*- this.getR()*/ > 0);
            bool isCorrectInY = (this.getPosition().Y + this.getR() + this.getSpeed().Y < maxY /*- 2 * getR()*/) && (this.getPosition().Y + this.getSpeed().Y /*- this.getR()*/ > 0);
            if (!isCorrectInX)
            {
                this.setSpeed(-this.getSpeed().X, this.getSpeed().Y);

            }
            if (!isCorrectInY)
            {
                this.setSpeed(this.getSpeed().X, -this.getSpeed().Y);

            }

            position += speed;
            DataEventArgs args = new DataEventArgs(this);
            ChangedPosition?.Invoke(this, args);

        }

    }
}
