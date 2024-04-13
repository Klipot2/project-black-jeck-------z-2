namespace Casino.SmallGames
{
    public class TriangleDrawer : IPlayable
    {
        private int height;

        public void ReadHeightFromInput()
        {
            string? input = Console.ReadLine();

            if (!int.TryParse(input, out height))
            {
                throw new ArgumentException("Input couldn't be parsed as a number.");
            }
        }

        public void DrawTriangle()
        {
            for (int i = 0; i < height; i++)
            {
                WriteTriangleLayer(i);
            }
        }

        private void WriteTriangleLayer(int currentLayer)
        {
            string layerString = new string(' ', height - currentLayer - 1) + "/";
            if (currentLayer == (height - 1))
            {
                layerString += new string('_', currentLayer * 2);
            }
            else if (currentLayer > 0)
            {
                layerString += new string(' ', currentLayer * 2);
            }
            layerString += "\\" + new string(' ', height - currentLayer - 1);

            Console.WriteLine(layerString);
        }

        public void Play()
        {
            ReadHeightFromInput();
            DrawTriangle();
        }
    }
}