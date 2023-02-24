namespace Arbeitszeitberechnung
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LectureCal
    {
        public int WorkDaysPerWeek { get; set; } = 5;
        public int FullTimeHoursPerWorkDay { get; set; } = 8;
        public int WeeklyHours => WorkDaysPerWeek * FullTimeHoursPerWorkDay;
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public List<(DateTime Start, DateTime End)> LectureFreeTimes = new();
        public TimeSpan Diff => End - Start;
        public int Days => Diff.Days;
        public int SemesterWeeks => Days / 7;
        public int SemesterWorkDays => Days * WorkDaysPerWeek / 7;
        public int FreeBusinessDays => LectureFreeTimes.Aggregate(0, (days, timewindow) => days += (timewindow.End - timewindow.Start).Days) * WorkDaysPerWeek / 7;
        /// <summary>
        /// Public holidays or vacation days
        /// </summary>
        public int HolidaysDuringLectureFreeTimes { get; set; } = 1 + 1; // 24.12. + 31.12.
        public int PartTimeDays => SemesterWorkDays - FreeBusinessDays;
        public int FullTimeDays => FreeBusinessDays - HolidaysDuringLectureFreeTimes;
        public Dictionary<string, int> LectureWeeklyHours = new();
        /// <summary>
        /// During lecture times
        /// </summary>
        public int WeeklyLearningHours => LectureWeeklyHours.Values.Sum();
        /// <summary>
        /// During lecture times
        /// </summary>
        public int WeeklyWorkingHours => WeeklyHours - WeeklyLearningHours;

        public int FullTimeHours => FullTimeDays * FullTimeHoursPerWorkDay;
        public int PartTimeHours => PartTimeDays * WeeklyWorkingHours / WorkDaysPerWeek;
        public int OverallHoursDuringSemester => FullTimeHours + PartTimeHours;
        public int OverallEquivalentFulltimeDaysDuringSemester => OverallHoursDuringSemester / FullTimeHoursPerWorkDay;
        public int AdditionalWorkHoursOutsideSemester { get; set; } = 130;
        public int OverallWorkHours => OverallHoursDuringSemester + AdditionalWorkHoursOutsideSemester;
        public int OverallEquivalentFulltimeDays => OverallWorkHours / FullTimeHoursPerWorkDay;

        public LectureCal(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
            LectureFreeTimes.Add((new DateTime(2021, 12, 20), new DateTime(2021, 12, 31)));
            LectureWeeklyHours.Add("Graphentheoretische Konzepte und Algorithmen", 6);
            LectureWeeklyHours.Add("Algorithmen und Datenstrukturen", 3 + 2);
            LectureWeeklyHours.Add("Software Engineering  I", 5);
            LectureWeeklyHours.Add("Betriebssysteme", 5);
            LectureWeeklyHours.Add("Betriebswirtschaftslehre  II", 4);
        }
    }
}
