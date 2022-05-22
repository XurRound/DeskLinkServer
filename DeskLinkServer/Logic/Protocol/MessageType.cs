namespace DeskLinkServer.Logic.Protocol
{
    public enum MessageType
    {
        Register =          0xF7,
        Auth =              0x07,
        LeftClick =         0xB1,
        RightClick =        0xB2,
        LeftDown =          0xB3,
        LeftUp =            0xB4,
        CursorMove =        0xF0,
        TypeText =          0xA0,
        TypeSpecialSymbol = 0xA1
    }
}
