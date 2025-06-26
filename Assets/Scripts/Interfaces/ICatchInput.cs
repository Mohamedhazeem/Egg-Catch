public interface ICatchInput
{
    CatchLane GetLaneInput();
    void SetLane(CatchLane catchLane);
    void ResetLaneCommitment();
}
public interface IInput
{
    void SetInput(ICatchInput catchInput);
}