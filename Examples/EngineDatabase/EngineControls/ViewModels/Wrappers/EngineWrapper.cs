using EngineModels.EngineModels;
using EngineUIComponents.ViewModels.LookupItems;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EngineUIComponents.ViewModels.Wrappers
{
    public class EngineWrapper : ModelWrapper<Engine>
    {
        private UserLookupItem _selectedUser;
        private EngineTypeLookupItem _selectedEngineType;

        public EngineWrapper(Engine engine) : base(engine) 
        {
        }

        public int UserId
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int UserIdOriginalValue => GetOriginalValue<int>(nameof(Id));

        public bool UserIdIsChanged => GetIsChanged(nameof(UserId));


        public string FabricNumber
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public int FabricNumberOriginalValue => GetOriginalValue<int>(nameof(Id));

        public bool FabricNumberIsChanged => GetIsChanged(nameof(FabricNumber));


        public int EngineTypeId
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int EngineTypeIdOriginalValue => GetOriginalValue<int>(nameof(Id));

        public bool EngineTypeIdIsChanged => GetIsChanged(nameof(EngineTypeId));

        public UserLookupItem SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                UserId = value?.Id ?? 0;
                OnPropertyChanged();
            }
        }

        public EngineTypeLookupItem SelectedEngineType
        {
            get => _selectedEngineType;
            set
            {
                _selectedEngineType = value;
                EngineTypeId = value?.Id ?? 0;
                OnPropertyChanged();
            }
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(FabricNumber))
            {
                yield return new ValidationResult("FabricNumber is required",
                  new[] { nameof(FabricNumber) });
            }

            if (SelectedUser == null)
            {
                yield return new ValidationResult("User is required",
                  new[] { nameof(SelectedUser) });
            }

            if (SelectedEngineType == null)
            {
                yield return new ValidationResult("Engine type is required",
                  new[] { nameof(SelectedEngineType) });
            }

        }
    }
}
