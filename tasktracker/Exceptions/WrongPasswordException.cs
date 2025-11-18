namespace tasktracker.Exceptions
{
    /// <summary>
    /// Exception thrown when login with bad password
    /// </summary>
    public class WrongPasswordException : Exception
    {
        /// <summary>
        /// Initial constructor
        /// </summary>
        public WrongPasswordException()
        {
        }

        /// <summary>
        /// WrongPasswordException constructor
        /// </summary>
        /// <param name="message"></param>
        public WrongPasswordException(string? message) : base(message)
        {
        }
    }
}
