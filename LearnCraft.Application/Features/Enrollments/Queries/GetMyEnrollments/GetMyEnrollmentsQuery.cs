using LearnCraft.Application.Features.Courses.Queries.GetCourses;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Enrollments.Queries.GetMyEnrollments;

public record GetMyEnrollmentsQuery(Guid UserId) 
    : IRequest<Result<List<CourseResponse>>>;
