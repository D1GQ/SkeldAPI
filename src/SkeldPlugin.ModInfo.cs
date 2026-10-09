using Semver;
using System.Reflection;

namespace SkeldApi;

public partial class SkeldPlugin
{
    /// <summary>
    /// Contains metadata and constants for the BetterAmongUs mod.
    /// </summary>
    public static class ModInfo
    {
        /// <summary>
        /// The base version number of the mod in MAJOR.MINOR.PATCH format.
        /// </summary>
        public const string VERSION_NUMBER = "1.0.0";

        /// <summary>
        /// The beta release number. Increment this for each beta release.
        /// </summary>
        public const string BETA_NUMBER = "0";

        /// <summary>
        /// Gets the full version string for the current build configuration.
        /// </summary>
#if RELEASE
        public const string VERSION = VERSION_NUMBER;
#elif BETA
        public const string VERSION = $"{VERSION_NUMBER}-beta-{BETA_NUMBER}";
#endif

        /// <summary>
        /// Gets the full version string with a v prefix for the current build configuration.
        /// </summary>
        public const string VERSION_STRING = "v" + VERSION;

        /// <summary>
        /// Gets the parsed semantic version of the mod.
        /// </summary>
        public static readonly SemVersion SemVersion = SemVersion.Parse(VERSION);

        /// <summary>
        /// Gets the Git commit hash from assembly metadata.
        /// </summary>
        public const string CommitHash = ThisAssembly.Git.Commit;

        /// <summary>
        /// Gets the build date from assembly metadata.
        /// </summary>
        public const string BuildDate = ThisAssembly.Metadata.BuildDate;

        /// <summary>
        /// The name of SkeldAPI.
        /// </summary>
        public const string PLUGIN_NAME = "SkeldAPI";

        /// <summary>
        /// The GUID (Globally Unique Identifier) of SkeldAPI.
        /// </summary>
        public const string PLUGIN_GUID = "com.d1gq.skeldapi";

        /// <summary>
        /// The GitHub repository URL for SkeldAPI.
        /// </summary>
        public const string GITHUB = ThisAssembly.Git.RepositoryUrl;

        /// <summary>
        /// The SkeldAPI assembly.
        /// </summary>
        public static Assembly Assembly
        {
            get
            {
                if (field == null)
                {
                    field = Assembly.GetExecutingAssembly();
                }
                return field;
            }
        }

        /// <summary>
        /// Contains constants for Among Us.
        /// </summary>
        internal static class AmongUs
        {
            /// <summary>
            /// The process name of the Among Us executable.
            /// </summary>
            internal const string PROCESS_NAME = "Among Us.exe";
        }
    }
}