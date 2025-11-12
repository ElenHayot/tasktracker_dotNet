namespace tasktracker.Exceptions
{
    /// <summary>
    /// Exception thrown when updating a user with wrong task ID
    /// </summary>
    public class AssociatedTaskNotFound : Exception
    {
        /// <summary>
        /// Initial constructor
        /// </summary>
        public AssociatedTaskNotFound()
        {
        }

        /// <summary>
        /// AssociatedTaskNotFound constructor
        /// </summary>
        /// <param name="message">Message to send</param>
        public AssociatedTaskNotFound(string? message) : base(message)
        {
        }
    }
}
