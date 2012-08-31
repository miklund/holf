namespace Holf.Tests
{
    using Xunit;

    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Scan
    {
        private void ExampleWithoutScan()
        {
            // create a time table for busses
            var departure = 609; // first departure 10:09, 609 minutes after midnight
            var timetable = new List<int> { departure };

            // nine times
            for (int i = 0; i < 9; i++)
            {
                // add 20 minutes, 10 minutes
                departure += i % 2 == 0 ? 20 : 10;
                timetable.Add(departure);
            }

            // print: 10:09, 10:29, 10:39, 10:59, 11:09, 11:29, 11:39, 11:59, 12:09, 12:29,
            foreach (var time in timetable)
            {
                Console.Write("{0:00}:{1:00}, ", time / 60, time % 60);
            }
        }

        [Fact]
        public void CreateBusTimeTable()
        {
            // arrange
            const int FirstDeparture = 609;
            var intervals = 0.Expand(i => i == 20 ? 10 : 20);

            // act
            var timetable = intervals
                .Scan((previous, interval) => previous + interval, FirstDeparture)
                .Map(time => string.Format("{0:00}:{1:00}", time / 60, time % 60))
                .Take(10);

            // assert
            Assert.Equal(
                "10:09, 10:29, 10:39, 10:59, 11:09, 11:29, 11:39, 11:59, 12:09, 12:29", 
                string.Join(", ", timetable.ToArray()));
        }
    }
}
