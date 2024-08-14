﻿using DeveloperExam.Application.Abstractions.Messaging;
using DeveloperExam.Application.Dto;
using DeveloperExam.Domain.Abstractions;
using DeveloperExam.Domain.Exceptions;

namespace DeveloperExam.Application.UserProfile.Commands.UpdateUserProfile;

internal sealed class UpdateUserProfileCommandHandler : ICommandHandler<UpdateUserProfileCommand, ServiceResponse>
{
    private readonly IUserProfileRepository _userProfileRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserProfileCommandHandler(IUserProfileRepository userProfileRepository, IUnitOfWork unitOfWork)
    {
        _userProfileRepository = userProfileRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ServiceResponse> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
    {
        var userProfile = await _userProfileRepository.GetByIdAsync(request.Id);
        if (userProfile is null)
            throw new UserProfileNotFoundException(request.Id);

        userProfile.Name = request.Name;
        userProfile.Weight = request.Weight;
        userProfile.Height = request.Height;
        userProfile.BirthDate = request.BirthDate;

        _userProfileRepository.Update(userProfile);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ServiceResponse(true, "Customer updated");
    }
}