using FluentResults;
using EstiPro.Application.Abstractions;
using EstiPro.Application.Abstractions.Messaging;
using EstiPro.Domain.Abstractions;
using EstiPro.Domain.Entities;

namespace EstiPro.Application.Users.Commands.VerifyUser;

public class CreateUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateUserCommand, Result>
{
    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.User.UserId, cancellationToken);

        if (user != null)
        {
            return Result.Ok();
        }

        var newUser = new User(request.User.UserId, request.User.Email, request.User.Name);

        userRepository.Add(newUser);
        var result = await unitOfWork.SaveChangesAsync(cancellationToken);
        return result == 1
            ? Result.Ok()
            : Result.Fail("User creation failed");
    }
}