using SPV3.Loader;

namespace Nova.UI
{
    /// <summary>
    ///     Main model used by the SPV3.2 Launcher.
    /// </summary>
    public class Main
    {
        /// <summary>
        ///     Invokes the SPV3.Loader's HCE executable loading routine.
        /// </summary>
        public void Load()
        {
            new SPV3.Loader.Loader(new LoaderConfiguration()).Start(ExecutableFactory.Detect());
        }
    }
}