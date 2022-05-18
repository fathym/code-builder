namespace Fathym.LCU.IDE.Controls
{
    public class IDEBarItemState
    {
        public virtual string? Icon { get; set; }

        public virtual List<IDEBarItemState>? Items { get; set; }

        public virtual string? Path { get; set; }

        public virtual IDEBarItemPositionTypes Position { get; set; }

        public virtual string? Text { get; set; }
    }
}