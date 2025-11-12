namespace tasktracker.Exceptions
{
    /// <summary>
    /// NotFoundException class
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Initial constructor
        /// </summary>
        public NotFoundException()
        {
        }

        /// <summary>
        /// NotFoundException constructor
        /// </summary>
        /// <param name="message">Message to send</param>
        public NotFoundException(string message) : base(message) { }
    }
}
