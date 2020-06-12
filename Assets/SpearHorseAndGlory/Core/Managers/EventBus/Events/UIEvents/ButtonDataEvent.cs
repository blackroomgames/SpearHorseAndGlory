namespace SpearHorseAndGlory.EventBusSystem
{
    public readonly struct ButtonDataEvent
    {
        public readonly object button;
        public readonly UI.ButtonType buttonType;
        public readonly bool isPressed;

        public ButtonDataEvent(object button ,UI.ButtonType buttonType = UI.ButtonType.NullType, bool isPressed = false)
        {
            this.button = button;
            this.buttonType = buttonType;
            this.isPressed = isPressed;
        }
    }
}

