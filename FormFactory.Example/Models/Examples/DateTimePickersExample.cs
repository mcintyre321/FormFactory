using System;
using System.ComponentModel.DataAnnotations;

namespace FormFactory.Example.Models.Examples
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

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public DateTime LastAccessTime { get; set; }

        [DataType(DataType.Date)]
        public DateTimeOffset DateOfBirthAsDateTimeOffset { get; set; }

        public DateTimeOffset LastAccessTimeAsDateTimeOffset { get; set; }
    }
}