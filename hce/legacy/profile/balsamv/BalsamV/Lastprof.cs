namespace BalsamV
{
    public class Lastprof
    {
        /// <summary>
        ///     Separation character which is guaranteed to be present.
        /// </summary>
        public const char Delimiter = '\\';

        /// <summary>
        ///     Position of the profile name relative to the end of the split string.
        /// </summary>
        public const int NameOffset = 0x2;

        /// <summary>
        ///     Known string that is present in the lastprof.txt.
        /// </summary>
        public const string Signature = "lam.sav";

        public string Name { get; set; }
    }
}