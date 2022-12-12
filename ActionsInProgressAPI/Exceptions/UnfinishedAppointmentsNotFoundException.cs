namespace ActionsInProgressAPI.Exceptions
{
    public class UnfinishedAppointmentsNotFoundException : Exception
    {
        public UnfinishedAppointmentsNotFoundException(int customerUserId)
            : base($"Customer with user id: '{customerUserId}' doesn't have unfinished appointments.")
        {
        }
    }
}
