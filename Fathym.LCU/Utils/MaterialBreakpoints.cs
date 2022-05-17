namespace Fathym.LCU.Utils
{
    public static class LCUBreakpoints
    {
        public static Func<int, int, string> Between = (start, end) => $"(min-width: {start}px) and (max-width: {end}px)";

        public static Func<int, string> Down => (end) => $"(max-width: {end}px)";

        public static Func<int, string> Up => (start) => $"(min-width: {start}px)";

        public static class Material
        {
            public static string ExtraSmall => "(max-width: 480px)";

            public static string Small => "(min-width: 481px) and (max-width: 720px)";

            public static string SmallDown => "(max-width: 720px)";

            public static string SmallUp => "(min-width: 481px)";

            public static string Medium => "(min-width: 721px) and (max-width: 1023px)";

            public static string MediumDown => "(max-width: 1023px)";

            public static string MediumUp => "(min-width: 721px)";

            public static string Large => "(min-width: 1024px) and (max-width: 1199px)";

            public static string LargeDown => "(max-width: 1199px)";

            public static string LargeUp => "(min-width: 1024px)";

            public static string ExtraLarge => "(min-width: 1200px)";
        }
    }


}