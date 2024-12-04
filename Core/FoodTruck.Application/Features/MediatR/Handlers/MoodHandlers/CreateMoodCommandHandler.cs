using FoodTruck.Application.Features.MediatR.Commands.MoodCommands;
using FoodTruck.Application.Interfaces;
using FoodTruck.Domain.Entities;
using MediatR;

namespace FoodTruck.Application.Features.MediatR.Handlers.MoodHandlers
{
    public class CreateMoodCommandHandler : IRequestHandler<CreateMoodCommand, bool>
    {
        private readonly IRepository<Mood> _repository;

        public CreateMoodCommandHandler(IRepository<Mood> repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(CreateMoodCommand request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new Mood
            {
                Name = request.Name
            });

            var data = await _repository.GetByFilterAsync(x => x.Name == request.Name);

            if (data != null)
            {
                return true;
            }

            return false;
        }
    }
}