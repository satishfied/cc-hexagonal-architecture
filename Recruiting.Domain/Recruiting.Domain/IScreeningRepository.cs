namespace Recruiting.Domain
{
    using System;

    public interface IScreeningRepository
    {
        void CreateScreening(DateTime date, string candidate);
    }
}