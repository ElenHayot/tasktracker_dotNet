namespace tasktracker.Exceptions
{
    /// <summary>
    /// Exception thrown when creating a task with wrong project ID
    /// </summary>
    public class AssociatedProjectNotFound : Exception
    {
        /// <summary>
        /// Initial constructor
        /// </summary>
        public AssociatedProjectNotFound()
        {
        }

        /// <summary>
        /// AssociatedProjectNotFound constructor
        /// </summary>
        /// <param name="message">Message to send</param>
        public AssociatedProjectNotFound(string? message) : base(message)
        {
        }
    }
}
