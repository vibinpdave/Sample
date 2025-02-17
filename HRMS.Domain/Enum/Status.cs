
namespace HRMS.Domain.Enum
{
    public enum Status
    {
        Active,      // Employee is currently working
        Inactive,    // Employee is temporarily not working
        Terminated,  // Employee has been removed from the company
        Resigned,    // Employee voluntarily left the company
        OnLeave      // Employee is on approved leave
    }
}
