using NanoCell.Application.Common.Interfaces;
using System;

namespace NanoCell.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
