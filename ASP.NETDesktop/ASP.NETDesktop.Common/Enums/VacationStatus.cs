using System.ComponentModel;

namespace ASP.NETDesktop.Common.Enums {
    public enum VacationStatus {
        [Description("None")]
        None,
        [Description("Can move")]
        CanMove,
        [Description("Cannot move")]
        CannotMove
    }
}
