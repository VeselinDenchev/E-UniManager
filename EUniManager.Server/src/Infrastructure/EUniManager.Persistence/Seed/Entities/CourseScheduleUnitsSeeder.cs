using EUniManager.Application.Models.DbContexts;
using EUniManager.Domain.Entities;
using EUniManager.Domain.Enums;

using Microsoft.EntityFrameworkCore;

namespace EUniManager.Persistence.Seed.Entities;

public static class CourseScheduleUnitsSeeder
{
    public static async Task SeedAsync(IEUniManagerDbContext dbContext)
    {
        if (await dbContext.CourseScheduleUnits.AsNoTracking().AnyAsync()) return;

        Activity[] activities = await dbContext.Activities.Include(a => a.Students)
                                                          .ToArrayAsync();
        List<CourseScheduleUnit> scheduleUnits = new();

        for (int i = 0; i < activities.Length; i++)
        {
            CourseScheduleUnit scheduleUnit;
            if (activities[i].Students.Any())
            {
                bool isLecture = activities[i].Type is ActivityType.Lecture;
                scheduleUnit = new CourseScheduleUnit
                {
                    DayOfWeek = (DayOfWeek)Math.Max(1, Math.Min(6, (i + 2) % 6)),
                    StartTime = new TimeOnly(8, 15),
                    MonthlyFrequency = (ScheduleMonthlyFrequency)(i % 3),
                    GroupNumber = isLecture ? null : 1,
                    GroupType = isLecture ? null : (SubjectGroupType)(i % 2),
                    RoomNumber = (short)(100 + i),
                    Place = SchedulePlace.Corps4,
                    Activity = activities[i]
                };
            }
            else
            {
                scheduleUnit = new CourseScheduleUnit
                {
                    ExactDate = DateOnly.FromDateTime(DateTime.Now),
                    StartTime = new TimeOnly(10, 15),
                    MonthlyFrequency = null,
                    GroupNumber = 2,
                    GroupType = SubjectGroupType.Laboratory,
                    RoomNumber = 201,
                    Place = SchedulePlace.Corps4,
                    Activity = activities[i]
                };
            }

            scheduleUnits.Add(scheduleUnit);
        }
        
        await dbContext.CourseScheduleUnits.AddRangeAsync(scheduleUnits);
        await dbContext.SaveChangesAsync();
        
        Console.WriteLine("Schedule seeded successfully");
    }
}