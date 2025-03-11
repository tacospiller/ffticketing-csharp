namespace ffTicketingCsharp
{
    internal interface IActor
    {
        Task<bool> Loop();
    }
}