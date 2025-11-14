namespace tasktracker.Exceptions
{
    /// <summary>
    /// Exception thrown when creating a user whith an existing email
    /// </summary>
    public class EmailAlreadyExistsException : Exception
    {
        /// <summary>
        /// Initial constructor
        /// </summary>
        public EmailAlreadyExistsException()
        {
        }

        /// <summary>
        /// EmailAlreadyExists constructor
        /// </summary>
        /// <param name="message">Message to send</param>
        public EmailAlreadyExistsException(string? message) : base(message)
        {
        }
    }
}
