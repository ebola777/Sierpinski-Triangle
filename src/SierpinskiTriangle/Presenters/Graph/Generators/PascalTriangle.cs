namespace SierpinskiTriangle.Presenters.Graph.Generators
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;

    using SierpinskiTriangle.Presenters.Graph.Generators.Base;

    public class PascalTriangle : GeneratorBase<BigInteger>
    {
        #region Constructors and Destructors

        public PascalTriangle(int generatedHigh, IList<BigInteger> sequence)
            : base(generatedHigh, sequence)
        {
        }

        public PascalTriangle()
        {
        }

        #endregion

        #region Public Methods and Operators

        public static int GetGroupIndex(int ind)
        {
            return (int)Math.Floor(Math.Sqrt(ind + 0.25) - 0.5);
        }

        public static int GetLowerByIndex(int ind)
        {
            var lowerLineHalf = (int)Math.Floor(Math.Sqrt(ind + 0.25) - 0.5);
            return GetLowerByGroupIndex(lowerLineHalf);
        }

        public override GeneratorResult<BigInteger> Generate(int numLevels)
        {
            var result = new GeneratorResult<BigInteger>();
            int lower = GetLowerByGroupIndex(numLevels);
            var list = (List<BigInteger>)this.Sequence;

            if (numLevels > this.GeneratedHigh)
            {
                this.GenerateLines(this.GeneratedHigh, numLevels);
                this.GeneratedHigh = numLevels;
            }

            result.GeneratedHigh = numLevels;
            result.Sequence = list.GetRange(0, lower);

            return result;
        }

        public override void GenerateLines(int beginLine, int endLine)
        {
            // convert line group number to index
            int ind = GetLowerByGroupIndex(beginLine);
            int indEnd = GetLowerByGroupIndex(endLine);

            // convert index to 2d position
            Pos pos = GetPos(ind);
            int line = pos.Line;

            if (ind <= 1)
            {
                this.Sequence.Add(1);
                this.Sequence.Add(1);

                line = 2;
                ind = 2;
            }

            int dec = (line + 1) / 2;

            while (ind != indEnd)
            {
                // even line
                this.Sequence.Add(1);
                ++ind;

                int col = 1;

                for (; col < line / 2; ++col)
                {
                    this.Sequence.Add(this.Sequence[ind - dec - 1] + this.Sequence[ind - dec]);
                    ++ind;
                }

                this.Sequence.Add(2 * this.Sequence[ind - dec - 1]);
                ++ind;

                // entering next line
                ++line;
                ++dec;

                // odd line
                this.Sequence.Add(1);
                ++ind;
                col = 1;

                for (; col <= line / 2; ++col)
                {
                    this.Sequence.Add(this.Sequence[ind - dec - 1] + this.Sequence[ind - dec]);
                    ++ind;
                }

                // entering next line
                ++line;
            }
        }

        #endregion

        #region Methods

        private static int GetLowerByGroupIndex(int ind)
        {
            return ind * (ind + 1);
        }

        private static Pos GetPos(int ind)
        {
            var lowerLineHalf = (int)Math.Floor(Math.Sqrt(ind + 0.25) - 0.5);
            int lowerLine = lowerLineHalf * 2;
            int lowerLineHalfInc = lowerLineHalf + 1;

            int lower = GetLowerByGroupIndex(lowerLineHalf);
            int ofsInd = ind - lower;
            int ofsGroup = ofsInd / lowerLineHalfInc;
            int col = ofsInd - lowerLineHalfInc * ofsGroup;
            int line = lowerLine + ofsGroup;

            return new Pos(col, line);
        }

        #endregion
    }

    public class Pos
    {
        #region Constructors and Destructors

        public Pos(int col, int line)
        {
            this.Col = col;
            this.Line = line;
        }

        #endregion

        #region Public Properties

        public int Col { get; set; }

        public int Line { get; set; }

        #endregion
    }
}