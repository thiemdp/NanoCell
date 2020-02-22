using NanoCell.Application.Common.Interfaces;
using System;

namespace NanoCell.WebUI.IntegrationTests
{
    public class TestDateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
