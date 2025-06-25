public interface ICatchInput
{
    CatchLane GetLaneInput();
    void SetLane(CatchLane catchLane);
}
public interface IInput
{
    void SetInput(ICatchInput catchInput);
}