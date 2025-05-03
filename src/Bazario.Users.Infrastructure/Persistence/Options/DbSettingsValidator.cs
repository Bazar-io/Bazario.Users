using Microsoft.Extensions.Options;

namespace Bazario.Users.Infrastructure.Persistence.Options
{
    [OptionsValidator]
    public partial class DbSettingsValidator
        : IValidateOptions<DbSettings>
    {
    }
}
