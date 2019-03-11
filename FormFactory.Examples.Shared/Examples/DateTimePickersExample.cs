using System;
using FormFactory.Attributes;

namespace FormFactory.Examples.Shared.Examples
{
    public class DateTimePickersExample
    {
        public DateTimePickersExample()
        {
            DateOfBirth = DateTime.Parse("15 Jan 1980");
            LastAccessTime = DateTime.UtcNow;
            DateOfBirthAsDateTimeOffset = DateTime.Parse("15 Jan 1980");
            LastAccessTimeAsDateTimeOffset = DateTime.UtcNow;

        }

        [Date]
        public DateTime DateOfBirth { get; set; }

        public DateTime LastAccessTime { get; set; }

        [Date]
        public DateTimeOffset DateOfBirthAsDateTimeOffset { get; set; }

        public DateTimeOffset LastAccessTimeAsDateTimeOffset { get; set; }
    }
}