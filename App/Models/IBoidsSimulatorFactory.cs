public interface IBoidsSimulatorFactory
{
    IBoidsSimulator Create(IRenderer renderer, BoidsSimulatorOptions options);
}