    public class BoidsSimulatorOptions
    {
        public int Fps { get; set; } = 30;
        public double FrameThresholdMilliseconds { get; set; } = 33.33;
        public int Width { get; set; }
        public int Height { get; set; }
        public int Count { get; set; } = 100;
        public float MaxSpeed { get; set; } = 9f;
        public float PerceptionRadius { get; set; } = 50f;
        public float SeparationDistance { get; set; } = 20f;
        public float DampingFactor { get; set; } = 0.95f;
        public float DirectionChangeFactor { get; set; } = 0.5f;
        public int AvoidWallsDistance = 50;

        public BoidsSimulatorOptions Clone() => (BoidsSimulatorOptions)MemberwiseClone();
    }