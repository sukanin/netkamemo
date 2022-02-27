using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotBambooDAL.Framework;

namespace DotBambooDAL
{
    public partial class UserAccount : IBaseEntity {}
    public partial class MenuItem : IBaseEntity { }
    public partial class Role : IBaseEntity { }
    public partial class Capability : IBaseEntity { }
    public partial class RoleCapability : IBaseEntity { }
    public partial class RoleUserAccount : IBaseEntity { }
    public partial class Product : IBaseEntity { }
    public partial class Purchase : IBaseEntity { }
    public partial class PurchaseItem : IBaseEntity { }
    public partial class UserPurchase : IBaseEntity { }
    public partial class UserReviewer : IBaseEntity { }
    public partial class UserApprover : IBaseEntity { }
    public partial class UserApprover2 : IBaseEntity { }
    public partial class Email : IBaseEntity { }
    public partial class PurchaseTodo : IBaseEntity { }
    public partial class PurchaseOrder : IBaseEntity { }
    public partial class PurchaseOrderItem : IBaseEntity { }
    public partial class PurchaseAttachment : IBaseEntity { }
    public partial class MemoAttachment : IBaseEntity { }
    public partial class PurchaseOrderAttachment : IBaseEntity { }
    public partial class PmOrder : IBaseEntity { }
    public partial class AccountCode : IBaseEntity { }
    public partial class CostCenter : IBaseEntity { }
    public partial class Notification : IBaseEntity { }
    public partial class PurchaseOrderTodo : IBaseEntity { }
    public partial class Vendor : IBaseEntity { }
    public partial class PurchaseOrderCancel : IBaseEntity { }
    public partial class Unit : IBaseEntity { }
    public partial class AuditObject : IBaseEntity { }
    public partial class AuditObjectProperty : IBaseEntity { }
    public partial class Audit : IBaseEntity { }
    public partial class PurchaseForecastAttachment : IBaseEntity { }
    public partial class GoodsReceived : IBaseEntity { }
    
    public partial class ItemMaster : IBaseEntity { }
    public partial class SupplierMaster : IBaseEntity { }
    public partial class BomMaster : IBaseEntity { }
    public partial class RelationMaster : IBaseEntity { }
    public partial class GoodsRec : IBaseEntity { }

    public partial class SupplierEvaluation : IBaseEntity { }

    public partial class Memo : IBaseEntity { }

}
