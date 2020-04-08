namespace WeChart
{
    public readonly struct Clip
    {
        public int Left { get; }
        public int Top { get; }
        public int Right { get; }
        public int Bottom { get; }

        public Clip(int left, int right, int top, int bottom) => (Left, Right, Top, Bottom) = (left, right, top, bottom);


    }
}
