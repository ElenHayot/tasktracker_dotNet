namespace tasktracker.Exceptions
{
    /// <summary>
    /// Exception thrown when dupplicating project or task title
    /// </summary>
    public class TitleAlreadyExistsException : Exception
    {
        /// <summary>
        /// Initial constructor
        /// </summary>
        public TitleAlreadyExistsException()
        {
        }

        /// <summary>
        /// TitleAlreadyExistsException constructor
        /// </summary>
        /// <param name="message"></param>
        public TitleAlreadyExistsException(string? message) : base(message)
        {
        }
    }
}
