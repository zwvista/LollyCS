using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Components.Abstractions;
using ReactiveUI.Validation.Contexts;
using ReactiveUI.Validation.Extensions;

namespace LollyCloud
{
    // https://github.com/reactiveui/ReactiveUI.Validation/blob/master/src/ReactiveUI.Validation/Helpers/ReactiveValidationObject.cs
    // https://stackoverflow.com/questions/3047830/how-to-inherit-from-a-generic-parameter

    /// <summary>
    /// Base class for ReactiveObjects that support INotifyDataErrorInfo validation.
    /// </summary>
    /// <typeparam name="TViewModel">The parent view model.</typeparam>
    public abstract class ReactiveValidationObject2<TViewModel> : ReactiveObject, IValidatableViewModel, INotifyDataErrorInfo where TViewModel : new ()
    {
        private readonly ObservableAsPropertyHelper<bool> _hasErrors;
        public TViewModel VM { get; set; } = new TViewModel();

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveValidationObject{TViewModel}"/> class.
        /// </summary>
        /// <param name="scheduler">Scheduler for OAPHs and for the the ValidationContext.</param>
        protected ReactiveValidationObject2(IScheduler scheduler = null)
        {
            ValidationContext = new ValidationContext(scheduler);

            _hasErrors = this
                .IsValid()
                .Select(valid => !valid)
                .ToProperty(this, x => x.HasErrors, scheduler: scheduler);

            ValidationContext.ValidationStatusChange
                .Select(validity => new DataErrorsChangedEventArgs(string.Empty))
                .Subscribe(args => ErrorsChanged?.Invoke(this, args));
        }

        /// <inheritdoc />
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <inheritdoc />
        public bool HasErrors => _hasErrors.Value;

        /// <inheritdoc />
        public ValidationContext ValidationContext { get; }

        /// <summary>
        /// Returns a collection of error messages, required by the INotifyDataErrorInfo interface.
        /// </summary>
        /// <param name="propertyName">Property to search error notifications for.</param>
        /// <returns>A list of error messages, usually strings.</returns>
        /// <inheritdoc />
        public virtual IEnumerable GetErrors(string propertyName)
        {
            var memberInfoName = GetType()
                .GetMember(propertyName)
                .FirstOrDefault()?
                .ToString();

            if (memberInfoName == null)
            {
                return Enumerable.Empty<string>();
            }

            var relatedPropertyValidations = ValidationContext
                .Validations
                .OfType<IPropertyValidationComponent<TViewModel>>()
                .Where(validation => validation.ContainsPropertyName(memberInfoName));

            return relatedPropertyValidations
                .Where(validation => !validation.IsValid)
                .SelectMany(validation => validation.Text)
                .ToList();
        }
    }
}
