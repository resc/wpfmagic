using System.Runtime.CompilerServices;

namespace GenerateAllTheThings.Utils
{
    /// <summary> Source contains methods to find the source file and directory at the time of compilation.  </summary>
    public static class Source
    {
        /// <summary> returns the source file path of the method that calls FilePath, do not supply any arguments, the compiler will do that for you </summary>
        public static string FilePath([CallerFilePath] string callerFilePath = null)
        {
            return callerFilePath;
        }

        /// <summary> returns the directory of the sourcefile of the method that calls FilePath </summary>
        public static string Directory([CallerFilePath] string callerFilePath = null)
        {
            return System.IO.Path.GetDirectoryName(callerFilePath);
        }
    }
}
