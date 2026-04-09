using LearnCraft.Application.Common.Models;
using LearnCraft.Application.Features.Enrollments.Queries.GetEnrollmentById;
using LearnCraft.Domain.Primitives;
using MediatR;

namespace LearnCraft.Application.Features.Enrollments.Queries.GetEnrollments;

public record GetEnrollmentsQuery(int PageNumber = 1, int PageSize = 10) 
    : IRequest<Result<PagedResult<EnrollmentResponse>>>;
