namespace tasktracker.Exceptions
{
    /// <summary>
    /// Exception thrown when deleting a projet with still uncompleted tasks associated
    /// </summary>
    public class UncompletedTasksAssociated : Exception
    {
        /// <summary>
        /// Initial constructor
        /// </summary>
        public UncompletedTasksAssociated()
        {
        }

        /// <summary>
        /// UncompletedTasksAssociated constructor
        /// </summary>
        /// <param name="message">Message to send</param>
        public UncompletedTasksAssociated(string? message) : base(message)
        {
        }
    }
}
