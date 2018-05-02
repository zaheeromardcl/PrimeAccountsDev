using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Domain.ViewModels
{
    public enum RoleNamesEnum
    {
        Admin = 1,
        InvoiceAdmin = 2,
        Accounts = 3,
        Warehouse = 4,
        Sales = 5
    }

    public enum PermissionControllerEnum
    {
        Consignment,
        Ticket,
        PurchaseInvoice,
        InvoiceAdmin,
        UsersAdmin,
        RolesAdmin,
        Permissions
    }

    public enum PermissionActionEnum
    {
        Create,
        Index,
        IndexTab,
        UpdatePurchaseInvoice,
        CreateTab,
        CreateCreditTicket,
        CreateCashTicket,
        CreateReceipt,
        CreateConsignmentTab
    }
}
