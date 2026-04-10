using LearnCraft.Application.Common.Models;
using LearnCraft.Application.Features.Courses.Queries.GetCourseById;
using LearnCraft.Application.Features.Courses.Queries.GetCourseLessons;
using LearnCraft.Application.Features.Courses.Queries.GetCourses;
using LearnCraft.Application.Features.Lessons.Queries.GetLessonById;
using MediatR;

namespace LearnCraft.API.GraphQL.Courses;

[ExtendObjectType("Query")]
public sealed class CourseQueries
{
    public async Task<PagedResult<CourseResponse>> GetCourses(
        [Service] ISender sender,
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetCoursesQuery(pageNumber, pageSize), cancellationToken);

        if (result.IsFailure)
        {
            throw new Exception(result.Error.Message);
        }

        return result.Value;
    }

    public async Task<CourseDetailsResponse> GetCourseById(
        [Service] ISender sender,
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetCourseByIdQuery(id), cancellationToken);

        if (result.IsFailure)
        {
            throw new Exception(result.Error.Message);
        }

        return result.Value;
    }

    public async Task<List<LessonResponse>> GetCourseLessons(
        [Service] ISender sender,
        Guid courseId,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetCourseLessonsQuery(courseId), cancellationToken);

        if (result.IsFailure)
        {
            throw new Exception(result.Error.Message);
        }

        return result.Value;
    }
}
