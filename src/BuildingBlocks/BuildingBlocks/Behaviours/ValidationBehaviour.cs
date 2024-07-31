
using BuildingBlocks.Abstractions.CQRS;
using FluentValidation;
using MediatR;


namespace BuildingBlocks.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponce> 
        (IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior <TRequest, TResponce> 
        where TRequest : ICommand<TResponce>  // Here means that i will validate only on Command Requests (Update , Create , Delete)
    {

        // All Pre PipeLineBehaviour Before Delegateing to RequestHandler 
        public async Task<TResponce> Handle(TRequest command, 
                                      RequestHandlerDelegate<TResponce> next, 
                                      CancellationToken cancellationToken)
        {
            // From Fluent Validation library make object from Validation Context
            var context = new ValidationContext<TRequest>(command);
            // Run All Validations 
            var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            // if Failures happened in Validation  get these errors 
            var Failures = validationResults
                .Where(r=>r.Errors.Any())
                .SelectMany(r=>r.Errors)
                .ToList();

            if (Failures.Any())
                throw new ValidationException(Failures);

            // to Invoke Next PipeLine Behaviour (Actual Command Handle  Method in CommandHandler)
            return await next();

        }
    }
}
