    public class BoidsSimulatorOptions
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Count { get; set; }
        public float MaxSpeed { get; set; } = 2f;
        public float PerceptionRadius { get; set; } = 50f;
        public float SeparationDistance { get; set; } = 20f;
        public float DampingFactor { get; set; } = 0.95f;
    }