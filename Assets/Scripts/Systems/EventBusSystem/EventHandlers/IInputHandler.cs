using EventBusSystem;

public interface IInputHandler : IGlobalSubscriber
{
    void HandleInput(float horizontalAxis, float verticalAxis);
}
