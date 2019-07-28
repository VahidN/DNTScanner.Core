namespace DNTScanner.Core
{
    /// <summary>
    /// Describes the different types of DocumentSources available to scanners.
    /// </summary>
    public enum DocumentSource
    {

        /// <summary>
        /// Represents a one-sided sheet-fed scanner.
        /// </summary>
        SingleSided = 1,

        /// <summary>
        /// Represents a one-sided flatbed scanner.
        /// </summary>
        Flatbed = 2,

        /// <summary>
        /// Represents a duplex sheet-fed scanner.
        /// </summary>
        DoubleSided = 4
    }
}