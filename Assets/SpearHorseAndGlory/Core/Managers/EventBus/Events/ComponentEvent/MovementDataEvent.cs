namespace SpearHorseAndGlory.EventBusSystem
{
    public readonly struct MovementDataEvent
    {
        public readonly bool isMove;

        public MovementDataEvent(bool isMove)
        {
            this.isMove = isMove;
        }
    }
}
