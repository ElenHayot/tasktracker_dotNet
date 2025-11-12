namespace tasktracker.Exceptions
{
    /// <summary>
    /// Exception thrown when creating a task with wrong UserId
    /// </summary>
    public class AssociatedUserNotFound : Exception
    {
        /// <summary>
        /// Initial constructor
        /// </summary>
        public AssociatedUserNotFound()
        {
        }

        /// <summary>
        /// AssociatedUserNotFound constructor
        /// </summary>
        /// <param name="message">Message to send</param>
        public AssociatedUserNotFound(string? message) : base(message)
        {
        }
    }
}
