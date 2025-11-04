namespace tasktracker.Exceptions
{
    /// <summary>
    /// NotFoundException class
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// NotFoundException constructor
        /// </summary>
        /// <param name="message"></param>
        public NotFoundException(string message) : base(message) { }
    }
}
